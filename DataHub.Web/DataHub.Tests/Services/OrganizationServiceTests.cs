using System.Linq;
using System.Threading.Tasks;
using DataHub.Api.Data.Entities;
using DataHub.Api.Services;
using Xunit;

namespace DataHub.Tests.Services
{
    public class OrganizationServiceTests : DatabaseFixtureBaseTest
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationServiceTests(DatabaseFixture databaseFixture) : base(databaseFixture)
        {
            _organizationService = new OrganizationService(DatabaseFixture.Mapper, DatabaseFixture.Context);
        }

        private Organization AddOrganization(string localOrganizationId, string organizationName = "Test Organization",
            string federalOrganizationId = "123456", int educationOrganizationId = 123456)
        {
            var organization = new Organization
            {
                LocalOrganizationID = localOrganizationId,
                OrganizationName = organizationName,
                FederalOrganizationID = federalOrganizationId,
                EducationOrganizationID = educationOrganizationId
            };
            DatabaseFixture.Context.Organization.Add(organization);
            DatabaseFixture.Context.SaveChanges();

            return organization;
        }

        [Fact]
        public async Task When_GetOrganizationByLocalOrganizationId_NoExists()
        {
            var actualOrganization = await _organizationService.GetOrganization(999);
            Assert.Null(actualOrganization);
        }

        [Fact]
        public async Task When_GetOrganizationByLocalOrganizationId_Exists()
        {
            var expectedOrganization = AddOrganization("8385738");
            var actualOrganization =
                await _organizationService.GetOrganization(
                    expectedOrganization.OrganizationId);
            Assert.Equal(expectedOrganization.LocalOrganizationID, actualOrganization.LocalOrganizationID);
            Assert.Equal(expectedOrganization.OrganizationName, actualOrganization.OrganizationName);
        }

        [Fact]
        public async Task When_GetAllOrganizations()
        {
            AddOrganization("8385738");
            var expectedOrganizations = DatabaseFixture.Context.Organization.ToList();
            var actualOrganizations = (await _organizationService.GetAllOrganizations()).ToList();
            Assert.Equal(expectedOrganizations.Count, actualOrganizations.Count);
            var count = expectedOrganizations.Count;
            for (var i = 0; i < count; ++i)
            {
                var expectedOrganization = expectedOrganizations[i];
                var actualOrganization = actualOrganizations.SingleOrDefault(o =>
                    o.LocalOrganizationID == expectedOrganization.LocalOrganizationID);
                Assert.NotNull(actualOrganization);
                Assert.Equal(expectedOrganization.OrganizationName, actualOrganization.OrganizationName);
            }
        }
    }
}