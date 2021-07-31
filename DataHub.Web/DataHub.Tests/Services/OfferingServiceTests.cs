using System;
using System.Linq;
using System.Threading.Tasks;
using DataHub.Api.Data.Entities;
using DataHub.Api.Enums;
using DataHub.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace DataHub.Tests.Services
{
    public class OfferingServiceTests : DatabaseFixtureBaseTest
    {
        private readonly IOfferingService _offeringService;

        public OfferingServiceTests(DatabaseFixture databaseFixture) : base(databaseFixture)
        {
            _offeringService = new OfferingService(DatabaseFixture.Mapper, DatabaseFixture.Context, new Mock<IEmailService>().Object, new Mock<IConfiguration>().Object, NullLogger<OfferingService>.Instance);
        }

        private Offering AddOffering(int itemNo, string itemName = "Test Item",
            string itemDescription = "Test Item Description", ItemType itemType = ItemType.Service,
            string associatedCost = "See pricing sheet", string productUrl = "ProductUrl",
            string contactName = "Test Staff Contact", string contactPhone = "TestPhone",
            string contactEmail = "test@email.com")
        {
            var offering = new Offering
            {
                ItemNo = itemNo,
                ItemName = itemName,
                ItemDescription = itemDescription,
                ItemType = (byte) itemType,
                AssociatedCost = associatedCost,
                ProductURL = productUrl,
                ContactName = contactName,
                ContactPhone = contactPhone,
                ContactEmail = contactEmail
            };
            DatabaseFixture.Context.Offering.Add(offering);
            DatabaseFixture.Context.SaveChanges();

            return offering;
        }

        private Participation AddParticipation(int organizationId, int offeringId, DateTime? asOfDate = null)
        {
            var participation = new Participation
            {
                OrganizationId = organizationId,
                OfferingId = offeringId,
                AsOfDate = asOfDate ?? DateTime.Now
            };
            DatabaseFixture.Context.Participation.Add(participation);
            DatabaseFixture.Context.SaveChanges();

            return participation;
        }

        [Fact]
        public async Task When_GetAllOfferings()
        {
            AddOffering(8385738);
            var expectedOfferings = DatabaseFixture.Context.Offering.ToList();
            var actualOfferings = (await _offeringService.GetAllOfferings()).ToList();
            Assert.Equal(expectedOfferings.Count, actualOfferings.Count);
            var count = expectedOfferings.Count;
            for (var i = 0; i < count; ++i)
            {
                var expectedOffering = expectedOfferings[i];

                var actualOffering = Assert.Single(actualOfferings, o => o.ItemNo == expectedOffering.ItemNo);
                Assert.NotNull(actualOffering);
                Assert.Equal((ItemCategoryType) expectedOffering.ItemCategoryType, actualOffering.ItemCategoryType);
                Assert.Equal(expectedOffering.ItemName, actualOffering.ItemName);
                Assert.Equal(expectedOffering.ItemDescription, actualOffering.ItemDescription);
                Assert.Equal((ItemType) expectedOffering.ItemType, actualOffering.ItemType);
                Assert.Equal(expectedOffering.AssociatedCost, actualOffering.AssociatedCost);
                Assert.Equal(expectedOffering.ProductURL, actualOffering.ProductUrl);
                Assert.Equal(expectedOffering.ContactName, actualOffering.ContactName);
                Assert.Equal(expectedOffering.ContactPhone, actualOffering.ContactPhone);
                Assert.Equal(expectedOffering.ContactEmail, actualOffering.ContactEmail);
            }
        }

        [Fact]
        public async Task When_GetAvailableOfferingsOrSaaS_ForOrgWithNoActiveOfferings()
        {
            var offering = AddOffering(8385738);
            AddParticipation(DatabaseFixture.TestOrganizationWarnerUnified.OrganizationId,
                offering.OfferingId);
            var expectedOfferingCount = DatabaseFixture.Context.Offering.Count();
            var actualOfferings = (await _offeringService.GetAvailableOfferings(DatabaseFixture
                .TestOrganizationGrandBend
                .OrganizationId)).ToList();
            Assert.Equal(expectedOfferingCount, actualOfferings.Count);
            var actualOffering = Assert.Single(actualOfferings, o => o.OfferingId == offering.OfferingId);
            Assert.NotNull(actualOffering);

        }

        [Fact]
        public async Task When_GetParticipatingOfferingsByOrganization_ForOrgWithNoActiveOfferings()
        {
            var offering = AddOffering(8385738);
            AddParticipation(DatabaseFixture.TestOrganizationWarnerUnified.OrganizationId,
                offering.OfferingId);
            var actualOfferings = (await _offeringService.GetParticipatingOfferings(DatabaseFixture
                .TestOrganizationGrandBend
                .OrganizationId)).ToList();
            Assert.Empty(actualOfferings);
        }

        [Fact]
        public async Task When_GetAvailableOfferingsByOrganization_ForOrgWithOneOffering()
        {
            var participatingOffering = AddOffering(8385738);
            AddParticipation(DatabaseFixture.TestOrganizationWarnerUnified.OrganizationId,
                participatingOffering.OfferingId);
            AddParticipation(DatabaseFixture.TestOrganizationGrandBend.OrganizationId,
                participatingOffering.OfferingId);
            var expectedOfferingCount = DatabaseFixture.Context.Offering.Count() - 1;
            var actualOfferings = (await _offeringService.GetAvailableOfferings(DatabaseFixture
                .TestOrganizationGrandBend
                .OrganizationId)).ToList();
            Assert.Equal(expectedOfferingCount, actualOfferings.Count);
            Assert.DoesNotContain(actualOfferings, o => o.OfferingId == participatingOffering.OfferingId);
        }

        [Fact]
        public async Task When_GetParticipatingOfferingsByOrganization_ForOrgWithOneOffering()
        {
            var offering = AddOffering(8385738);
            AddParticipation(DatabaseFixture.TestOrganizationWarnerUnified.OrganizationId,
                offering.OfferingId);
            AddParticipation(DatabaseFixture.TestOrganizationGrandBend.OrganizationId,
                offering.OfferingId);
            var actualOfferings = (await _offeringService.GetParticipatingOfferings(DatabaseFixture
                .TestOrganizationGrandBend
                .OrganizationId)).ToList();
            var actualOffering = Assert.Single(actualOfferings, o => o.OfferingId == offering.OfferingId);
            Assert.NotNull(actualOffering);
        }

        //Task<bool> AddParticipation(int organizationId, int itemNo);
        //Task<bool> DeleteParticipation(int organizationId, int itemNo);

        [Fact]
        public async Task When_AddParticipation_ForUnknownItemNo()
        {
            var addParticipationResult = await _offeringService.AddParticipation(DatabaseFixture.TestOrganizationGrandBend.OrganizationId, 8385738);
            Assert.False(addParticipationResult);
        }

        [Fact]
        public async Task When_AddParticipation_ExistingParticipationItem()
        {
            var offering = AddOffering(8385738);
            AddParticipation(DatabaseFixture.TestOrganizationGrandBend.OrganizationId,
                offering.OfferingId);
            var addParticipationResult = await _offeringService.AddParticipation(DatabaseFixture.TestOrganizationGrandBend.OrganizationId, offering.ItemNo);
            Assert.False(addParticipationResult);
        }

        [Fact]
        public async Task When_AddParticipation_ForValidParticipation()
        {
            var offering = AddOffering(8385738);
            var addParticipationResult = await _offeringService.AddParticipation(DatabaseFixture.TestOrganizationGrandBend.OrganizationId, offering.ItemNo);
            Assert.True(addParticipationResult);
        }

        [Fact]
        public async Task When_DeleteParticipation_ForMissingParticipation()
        {
            var offering = AddOffering(8385738);
            AddParticipation(DatabaseFixture.TestOrganizationWarnerUnified.OrganizationId, offering.OfferingId);
            var offering2 = AddOffering(8385739);
            AddParticipation(DatabaseFixture.TestOrganizationGrandBend.OrganizationId, offering2.OfferingId);
            var deleteParticipationResult = await _offeringService.DeleteParticipation(DatabaseFixture.TestOrganizationGrandBend.OrganizationId, offering.ItemNo);
            Assert.False(deleteParticipationResult);
        }

        [Fact]
        public async Task When_DeleteParticipation_ForValidParticipation()
        {
            var offering = AddOffering(8385738);
            AddParticipation(DatabaseFixture.TestOrganizationGrandBend.OrganizationId, offering.OfferingId);
            var deleteParticipationResult = await _offeringService.DeleteParticipation(DatabaseFixture.TestOrganizationGrandBend.OrganizationId, offering.ItemNo);
            Assert.True(deleteParticipationResult);
            Assert.True(await DatabaseFixture.Context.Participation.AllAsync(p =>
                p.OrganizationId != DatabaseFixture.TestOrganizationGrandBend.OrganizationId &&
                p.OfferingId != offering.OfferingId));
        }
    }
}