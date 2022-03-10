using System;
using System.Linq;
using System.Threading.Tasks;
using DataHub.Api.Data.Entities;
using DataHub.Api.Enums;
using DataHub.Api.Services;
using Xunit;

namespace DataHub.Tests.Services
{
    public class ExtractServiceTests : DatabaseFixtureBaseTest
    {
        private readonly IExtractService _extractService;

        public ExtractServiceTests(DatabaseFixture databaseFixture) : base(databaseFixture)
        {
            _extractService = new ExtractService(DatabaseFixture.Mapper, DatabaseFixture.Context);
        }

        private Extract AddExtract(int organizationId, string extractJobName = "Test Extract", string extractFrequency = "Test Frequency", string extractLastStatus = "Test Last Status", DateTime? extractLastDate = null)
        {
            var extract = new Extract
            {
                OrganizationId = organizationId,
                ExtractJobName = extractJobName,
                ExtractFrequency = extractFrequency,
                ExtractLastStatus = extractLastStatus,
                ExtractLastDate = extractLastDate ?? DateTime.Now
            };
            DatabaseFixture.Context.Extract.Add(extract);
            DatabaseFixture.Context.SaveChanges();

            return extract;
        }

        [Fact]
        public async Task When_GetExtractsByOrganization_ForOrgWithNoExtracts()
        {
            AddExtract(DatabaseFixture.TestOrganizationWarnerUnified.OrganizationId, "Test Warner");

            var actualExtracts = await _extractService.GetExtractsByOrganization(DatabaseFixture
                .TestOrganizationGrandBend
                .OrganizationId);
            Assert.Empty(actualExtracts);
        }

        [Fact]
        public async Task When_GetExtractsByOrganization_ForOrgExtracts()
        {
            var expectedExtract =
                AddExtract(DatabaseFixture.TestOrganizationGrandBend.OrganizationId, "Test GrandBend");
            var expectedAdditionalExtract =
                AddExtract(DatabaseFixture.TestOrganizationGrandBend.OrganizationId, "Test GrandBend 2");
            AddExtract(DatabaseFixture.TestOrganizationWarnerUnified.OrganizationId, "Test Warner");
            var actualExtracts = (await _extractService.GetExtractsByOrganization(DatabaseFixture
                .TestOrganizationGrandBend
                .OrganizationId)).ToList();
            Assert.Equal(2, actualExtracts.Count);
            var actualExtract = actualExtracts.SingleOrDefault(acc => acc.ExtractId == expectedExtract.ExtractId);
            Assert.NotNull(actualExtract);
            Assert.Equal(expectedExtract.Organization.OrganizationAbbreviation, actualExtract.OrganizationAbbreviation);
            Assert.Equal(expectedExtract.ExtractJobName, actualExtract.ExtractJobName);
            Assert.Equal(expectedExtract.ExtractFrequency, actualExtract.ExtractFrequency);
            Assert.Equal(expectedExtract.ExtractLastStatus, actualExtract.ExtractLastStatus);
            Assert.NotNull(expectedExtract.ExtractLastDate);
            Assert.Equal(expectedExtract.ExtractLastDate.Value.Date, actualExtract.ExtractLastDate);
            var actualAdditionalExtract =
                actualExtracts.SingleOrDefault(acc => acc.ExtractId == expectedAdditionalExtract.ExtractId);
            Assert.NotNull(actualAdditionalExtract);
            Assert.Equal(expectedAdditionalExtract.Organization.OrganizationAbbreviation, actualAdditionalExtract.OrganizationAbbreviation);
            Assert.Equal(expectedAdditionalExtract.ExtractJobName, actualAdditionalExtract.ExtractJobName);
            Assert.Equal(expectedAdditionalExtract.ExtractFrequency, actualAdditionalExtract.ExtractFrequency);
            Assert.Equal(expectedAdditionalExtract.ExtractLastStatus, actualAdditionalExtract.ExtractLastStatus);
            Assert.NotNull(expectedAdditionalExtract.ExtractLastDate);
            Assert.Equal(expectedAdditionalExtract.ExtractLastDate.Value.Date, actualAdditionalExtract.ExtractLastDate);
        }
   }
}