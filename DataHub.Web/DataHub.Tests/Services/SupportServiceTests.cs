using System.Linq;
using System.Threading.Tasks;
using DataHub.Api.Data.Entities;
using DataHub.Api.Enums;
using DataHub.Api.Services;
using Xunit;

namespace DataHub.Tests.Services
{
    public class SupportServiceTests : DatabaseFixtureBaseTest
    {
        private readonly ISupportService _supportService;

        public SupportServiceTests(DatabaseFixture databaseFixture) : base(databaseFixture)
        {
            _supportService = new SupportService(DatabaseFixture.Mapper, DatabaseFixture.Context);
        }

        private Support AddSupportTicket(int organizationId, string systemId = "Test System", string ticketId = "TestTicket#", string description = "Test ticket description", SupportStatus status = SupportStatus.New)
        {
            var support = new Support
            {
                OrganizationId = organizationId,
                SystemID = systemId,
                TicketID = ticketId,
                Description = description,
                Status = (byte)status
            };
            DatabaseFixture.Context.Support.Add(support);
            DatabaseFixture.Context.SaveChanges();

            return support;
        }

        [Fact]
        public async Task When_GetSupportsByOrganization_ForOrgWithNoSupports()
        {
            AddSupportTicket(DatabaseFixture.TestOrganizationWarnerUnified.OrganizationId, "Test Warner");
            var actualSupports = await _supportService.GetSupportTicketsByOrganization(DatabaseFixture
                .TestOrganizationGrandBend
                .OrganizationId);
            Assert.Empty(actualSupports);
        }

        [Fact]
        public async Task When_GetSupportsByOrganization_ForOrgSupports()
        {
            var expectedSupport =
                AddSupportTicket(DatabaseFixture.TestOrganizationGrandBend.OrganizationId, "Test GrandBend");
            var expectedAdditionalSupport =
                AddSupportTicket(DatabaseFixture.TestOrganizationGrandBend.OrganizationId, "Test GrandBend 2");
            AddSupportTicket(DatabaseFixture.TestOrganizationWarnerUnified.OrganizationId, "Test Warner");
            var actualSupports = (await _supportService.GetSupportTicketsByOrganization(DatabaseFixture
                .TestOrganizationGrandBend
                .OrganizationId)).ToList();
            Assert.Equal(2, actualSupports.Count);
            var actualSupport = actualSupports.SingleOrDefault(acc => acc.SupportId == expectedSupport.SupportId);
            Assert.NotNull(actualSupport);
            Assert.Equal(expectedSupport.SystemID, actualSupport.SystemId);
            Assert.Equal(expectedSupport.TicketID, actualSupport.TicketId);
            Assert.Equal(expectedSupport.Description, actualSupport.Description);
            Assert.Equal(expectedSupport.Status, (byte)actualSupport.Status);
            var actualAdditionalSupport =
                actualSupports.SingleOrDefault(acc => acc.SupportId == expectedAdditionalSupport.SupportId);
            Assert.NotNull(actualAdditionalSupport);
            Assert.Equal(expectedAdditionalSupport.SystemID, actualAdditionalSupport.SystemId);
            Assert.Equal(expectedAdditionalSupport.TicketID, actualAdditionalSupport.TicketId);
            Assert.Equal(expectedAdditionalSupport.Description, actualAdditionalSupport.Description);
            Assert.Equal(expectedAdditionalSupport.Status, (byte)actualAdditionalSupport.Status);
        }
   }
}