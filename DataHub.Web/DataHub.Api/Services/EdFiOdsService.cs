using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using DataHub.Api.Data;
using DataHub.Api.Data.Entities;
using DataHub.Api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Semver;

namespace DataHub.Api.Services
{
    public interface IEdFiOdsService
    {
        Task<EdFiOdsStatusDto> GetOdsStatus(int edFiOdsNo);
        Task<IEnumerable<EdFiOdsStatusDto>> GetAllOdsStatusesByOrganization(int organizationId);
    }

    public class EdFiOdsService : IEdFiOdsService
    {
        private const string TotalCountHeaderName = "Total-Count";
        private static readonly List<EdFiResource> Resources = new List<EdFiResource>
        {
            new EdFiResource("courses"),
            new EdFiResource("schools"),
            new EdFiResource("staffs"),
            new EdFiResource("students")
        };

        private readonly SDCOEDatahubContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;

        public EdFiOdsService(IHttpClientFactory httpClientFactory, SDCOEDatahubContext context, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _context = context;
            _mapper = mapper;
        }

        private async Task<string> GetBearerToken(EdFiOdsApiUrlProvider urlProvider, string key, string secret)
        {
            var authClient = _httpClientFactory.CreateClient();
            var response = await authClient.PostAsync(urlProvider.AuthTokenRequestUri, new FormUrlEncodedContent(
                new Dictionary<string, string>()
                {
                    { "grant_type", "client_credentials" },
                    { "client_id", key },
                    { "client_secret", secret }
                })).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var jToken = JToken.Parse(json);
                return jToken["access_token"].Value<string>();
            }
            else
            {
                throw new ApplicationException("Unable to acquire bearer token.");
            }
        }

        private async Task<HttpClient> BuildClientForOds(EdFiOdsApiUrlProvider urlProvider, string key, string secret)
        {
            var client = _httpClientFactory.CreateClient();
            var token = await GetBearerToken(urlProvider, key, secret).ConfigureAwait(false);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }

        private async Task<int?> GetCountForResource(HttpClient odsClient, EdFiOdsApiUrlProvider urlProvider, EdFiResource resource)
        {
            var response = await odsClient.GetAsync(urlProvider.ResourceCountRequestUri(resource)).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                if (response.Headers.TryGetValues(TotalCountHeaderName, out var headerValues))
                {
                    return int.Parse(headerValues.First());
                }
            }

            return null;
        }

        public async Task<EdFiOdsStatusDto> GetOdsStatus(int edFiOdsNo)
        {
            var ods = await _context.EdFiODS.Include(e => e.EdFiODSClients).Where(e => e.EdFiODSNo == edFiOdsNo)
                .SingleAsync().ConfigureAwait(false);

            var status = _mapper.Map<EdFiODS, EdFiOdsStatusDto>(ods);
            var apiVersion = SemVersion.Parse(ods.ODSVersion);
            var urlProvider = new EdFiOdsApiUrlProvider(ods.ODSURL, ods.ODSPath, apiVersion);

            HttpClient client;

            try
            {
                client = await BuildClientForOds(urlProvider, ods.ODSKey, ods.ODSSecret).ConfigureAwait(false);
                status.Status = OdsStatus.Available;
                status.LastCheckedDate = DateTime.Now;
            }
            catch
            {
                // If unable to build client, token acquisition likely failed
                // TODO: May want to use explicit exception type
                status.Status = OdsStatus.Offline;
                status.LastCheckedDate = DateTime.Now;

                // TODO: If status is being pulled from DB, we may want to use last status values instead of returning early here
                return status;
            }

            foreach (var resource in Resources)
            {
                var count = await GetCountForResource(client, urlProvider, resource).ConfigureAwait(false);
                if (count != null)
                {
                    status.ResourceCounts.Add(new EdFiOdsResourceCount()
                    {
                        ResourceName = resource.ResourceName,
                        ResourceCount = count.Value,
                        LastCheckedDate = DateTime.Now
                    });
                }
            }

            return status;
        }

        public async Task<IEnumerable<EdFiOdsStatusDto>> GetAllOdsStatusesByOrganization(int organizationId)
        {
            var edFiNos = await _context.EdFiODS
                .Where(e => e.OrganizationId == organizationId)
                .Select(e => e.EdFiODSNo)
                .ToListAsync().ConfigureAwait(false);

            var statuses = new List<EdFiOdsStatusDto>();

            foreach (var ods in edFiNos)
            {
                statuses.Add(await GetOdsStatus(ods).ConfigureAwait(false));
            }

            return statuses;
        }
    }
}
