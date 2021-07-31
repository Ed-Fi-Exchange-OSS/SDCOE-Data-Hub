using System;
using System.Linq;
using System.Threading.Tasks;
using DataHub.Api.Data.Models;
using DataHub.Api.Services;
using Semver;
using Xunit;

namespace DataHub.Tests.Services
{
    public class EdFiOdsApiUrlProviderTests : DatabaseFixtureBaseTest
    {
        public EdFiOdsApiUrlProviderTests(DatabaseFixture databaseFixture) : base(databaseFixture)
        {
        }

        [Theory]
        [InlineData("3.4", "1")]
        [InlineData("5.1", "1")]
        [InlineData("5.1.9", "1")]
        [InlineData("5.2", "0")]
        [InlineData("5.2.0", "0")]
        [InlineData("5.3", "0")]
        [InlineData("6", "0")]
        public async Task CountRequestUri_WhenApiVersionUnder5_2_0_ShouldLimitTo1(string apiVersionString, string expectedLimitValue)
        {
            // Arrange
            var semVer = SemVersion.Parse(apiVersionString);
            var urlProvider = new EdFiOdsApiUrlProvider("https://www.example.com", semVer);

            // Act
            var result = urlProvider.ResourceCountRequestUri(new EdFiResource("ed-fi", "students"));

            // Assert
            var url = new Flurl.Url(result);
            var actualLimitValue = url.QueryParams.Single(p => p.Item1 == "limit");
            Assert.Equal(expectedLimitValue, actualLimitValue.Item2);
        }

        [Theory]
        [InlineData("https://api.ed-fi.org/", "v3.4.0","ed-fi", "courses", "https://api.ed-fi.org/v3.4.0/api/data/v3/ed-fi/courses")]
        [InlineData("https://api.ed-fi.org/", "v3.4.0", "custom-namespace", "extendedResources", "https://api.ed-fi.org/v3.4.0/api/data/v3/custom-namespace/extendedResources")]
        public void ResourceCountUrl_WhenNameAndNamespaceVary_ShouldReturnValidPath(string baseUrl, string virtualPath, string resourceNamespace,
            string resourceName, string expectedCountUrl)
        {
            // Arrange
            var resource = new EdFiResource(resourceName, resourceNamespace);
            var urlProvider = new EdFiOdsApiUrlProvider(baseUrl, virtualPath, "3.4.0");

            // Act
            var actualCountUrl = urlProvider.ResourceCountRequestUri(resource);

            // Assert
            var url = new Flurl.Url(actualCountUrl).RemoveQuery();
            Assert.Equal(expectedCountUrl, url.ToString());
        }

        [Theory]
        [InlineData("https://api.ed-fi.org/", "v3.4.0", "3.4.0", "ed-fi", "courses")]
        [InlineData("https://api.ed-fi.org/", "v5.1.0", "5.1.0", "ed-fi", "students")]
        [InlineData("https://api.ed-fi.org/", "v5.3.0", "5.3.0", "ed-fi", "staff")]
        public void ResourceCountUrl_ForAnyResource_ShouldIncludeRequiredParams(string baseUrl, string virtualPath, string apiVersion, string resourceNamespace,
            string resourceName)
        {
            // Arrange
            var resource = new EdFiResource(resourceName, resourceNamespace);
            var urlProvider = new EdFiOdsApiUrlProvider(baseUrl, virtualPath, apiVersion);

            // Act
            var actualCountUrl = urlProvider.ResourceCountRequestUri(resource);

            // Assert
            var queryParams = new Flurl.Url(actualCountUrl).QueryParams;
            Assert.Contains(queryParams, qp => qp.Item1 == "totalCount" && (string)qp.Item2 == "true");
            Assert.Contains(queryParams, qp => qp.Item1 == "limit");
        }
    }
}