using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataHub.Api.Data;
using DataHub.Api.Data.Models;
using DataHub.Api.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DataHub.Api.Services
{
    public interface IOrganizationContextProvider
    {
        Task<int> GetCurrentOrganizationId();
        Task<UserDto> GetUser();
    }

    public class OrganizationContextProvider : IOrganizationContextProvider
    {
        private const string OrganizationHeaderName = "x-local-organization-id";
        private const string EmailClaimType = "preferred_username";

        private readonly SDCOEDatahubContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public OrganizationContextProvider(SDCOEDatahubContext dbContext, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<int> GetCurrentOrganizationId()
        {
            var user = await GetUser().ConfigureAwait(false);

            if (user == null)
                throw new ApplicationException("Unable to locate user");

            // If user is admin, check for org context header
            if (user.Role == UserRole.InternalAdministrator)
            {
                if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue(OrganizationHeaderName,
                    out var headerValues))
                {
                    var firstOrgId = headerValues.First();

                    // Find internal ID from local organization ID
                    var orgId = await _dbContext.Organization
                        .Where(o => o.LocalOrganizationID == firstOrgId)
                        .Select(o => (int?) o.OrganizationId)
                        .SingleOrDefaultAsync().ConfigureAwait(false);
                    if (orgId != null)
                    {
                        return orgId.Value;
                    }
                }
            }

            // Otherwise get org from user
            var userOrgId = await _dbContext.Organization
                .Where(o => o.LocalOrganizationID == user.LocalOrganizationId)
                .Select(o => o.OrganizationId)
                .SingleAsync().ConfigureAwait(false);
            return userOrgId;
        }

        public async Task<UserDto> GetUser()
        {
            var user = _httpContextAccessor.HttpContext.User;

            Claim emailClaim;
            if (!user.Identity.IsAuthenticated
                || (emailClaim = user.Claims.SingleOrDefault(c => c.Type.Equals(EmailClaimType, StringComparison.InvariantCultureIgnoreCase))) == null)
                return null;

            var dbUser = await _dbContext.User
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(u => u.EmailAddress == emailClaim.Value).ConfigureAwait(false);
            return dbUser;
        }
    }
}
