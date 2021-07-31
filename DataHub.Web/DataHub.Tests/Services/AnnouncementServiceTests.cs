using System;
using System.Linq;
using System.Threading.Tasks;
using DataHub.Api.Data.Entities;
using DataHub.Api.Enums;
using DataHub.Api.Services;
using Xunit;

namespace DataHub.Tests.Services
{
    public class AnnouncementServiceTests : DatabaseFixtureBaseTest
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementServiceTests(DatabaseFixture databaseFixture) : base(databaseFixture)
        {
            _announcementService = new AnnouncementService(DatabaseFixture.Mapper, DatabaseFixture.Context);
        }

        private Announcement AddAnnouncement(int organizationId, string message, DateTime? displayUntilDate = null,
            RecordStatus status = RecordStatus.Active)
        {
            var announcement = new Announcement
            {
                OrganizationId = organizationId,
                Message = message,
                DisplayUntilDate = displayUntilDate,
                Status = (byte) status
            };
            DatabaseFixture.Context.Announcement.Add(announcement);
            DatabaseFixture.Context.SaveChanges();

            return announcement;
        }

        [Fact]
        public async Task When_GetActiveAnnouncementsByOrganization_ForOrgWithNoAnnouncements()
        {
            var actualAnnouncements = await _announcementService.GetActiveAnnouncementsByOrganization(DatabaseFixture
                .TestOrganizationGrandBend
                .OrganizationId);
            Assert.Empty(actualAnnouncements);
        }

        [Fact]
        public async Task When_GetActiveAnnouncementsByOrganization_ForOrgWithActiveAnnouncements_ExcludesExpiredAnnouncements()
        {
            AddAnnouncement(DatabaseFixture.TestOrganizationGrandBend.OrganizationId, "Test GrandBend Expired",
                DateTime.Now.AddDays(-3));
            var actualAnnouncements = (await _announcementService.GetActiveAnnouncementsByOrganization(DatabaseFixture
                .TestOrganizationGrandBend
                .OrganizationId)).ToList();
            Assert.Empty(actualAnnouncements);
        }

        [Fact]
        public async Task When_GetActiveAnnouncementsByOrganization_ForOrgWithActiveAnnouncements_ExcludesInactiveAnnouncements()
        {
            AddAnnouncement(DatabaseFixture.TestOrganizationGrandBend.OrganizationId, "Test GrandBend Expired",
                status: RecordStatus.Inactive);
            var actualAnnouncements = (await _announcementService.GetActiveAnnouncementsByOrganization(DatabaseFixture
                .TestOrganizationGrandBend
                .OrganizationId)).ToList();
            Assert.Empty(actualAnnouncements);
        }

        [Fact]
        public async Task When_GetActiveAnnouncementsByOrganization_ForOrgWithActiveAnnouncements()
        {
            var expectedAnnouncement =
                AddAnnouncement(DatabaseFixture.TestOrganizationGrandBend.OrganizationId, "Test GrandBend");
            AddAnnouncement(DatabaseFixture.TestOrganizationWarnerUnified.OrganizationId, "Test Warner");
            var actualAnnouncements = (await _announcementService.GetActiveAnnouncementsByOrganization(DatabaseFixture
                .TestOrganizationGrandBend
                .OrganizationId)).ToList();
            var actualAnnouncement = Assert.Single(actualAnnouncements);
            Assert.NotNull(actualAnnouncement);
            Assert.Equal(expectedAnnouncement.AnnouncementId, actualAnnouncement.AnnouncementId);
            Assert.Equal(expectedAnnouncement.Message, actualAnnouncement.Message);
        }

        [Fact]
        public async Task When_GetActiveAnnouncementsByOrganization_ForOrgWithActiveAnnouncements_IncludesFutureDateAnnouncements()
        {
            var expectedAnnouncement = AddAnnouncement(DatabaseFixture.TestOrganizationGrandBend.OrganizationId,
                "Test GrandBend", DateTime.Now.AddDays(3));
            AddAnnouncement(DatabaseFixture.TestOrganizationWarnerUnified.OrganizationId, "Test Warner");
            var actualAnnouncements = (await _announcementService.GetActiveAnnouncementsByOrganization(DatabaseFixture
                .TestOrganizationGrandBend
                .OrganizationId)).ToList();
            var actualAnnouncement = Assert.Single(actualAnnouncements);
            Assert.NotNull(actualAnnouncement);
            Assert.Equal(expectedAnnouncement.AnnouncementId, actualAnnouncement.AnnouncementId);
            Assert.Equal(expectedAnnouncement.Message, actualAnnouncement.Message);
        }
    }
}