using System;
using DataHub.Api.Data.Models;
using Semver;

namespace DataHub.Api.Services
{
    public class EdFiOdsApiUrlProvider
    {
        private readonly SemVersion _apiVersion;
        private readonly string _baseUrl;

        public EdFiOdsApiUrlProvider(string baseUrl, SemVersion apiVersion) : this(baseUrl, "", apiVersion)
        {
            if (_apiVersion < new SemVersion(3))
            {
                throw new NotImplementedException("Support for Ed-Fi ODS API v2.X is not currently implemented");
            }
        }

        public EdFiOdsApiUrlProvider(string baseUrl, string virtualPath, SemVersion apiVersion)
        {
            _apiVersion = apiVersion;
            _baseUrl = baseUrl.Trim('/');

            if (!string.IsNullOrWhiteSpace(virtualPath))
            {
                _baseUrl = $"{_baseUrl}/{virtualPath.Trim('/')}";
            }
        }

        public string AuthTokenRequestUri => $"{_baseUrl}/api/oauth/token";

        public string ResourceCountRequestUri(EdFiResource edFiResource)
        {
            return $"{_baseUrl}/api/data/v3/{edFiResource.ResourceNamespace}/{edFiResource.ResourceName}?totalCount=true&limit={(_apiVersion >= new SemVersion(5, 2) ? "0" : "1")}";
        }
    }
}