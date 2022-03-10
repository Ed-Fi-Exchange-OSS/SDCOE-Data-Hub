using System;
using System.Linq;
using System.Threading.Tasks;
using DataHub.Api.Data.Entities;
using DataHub.Api.Enums;
using DataHub.Api.Services;
using Xunit;

namespace DataHub.Tests.Services
{
    public class CrmContactServiceTests : DatabaseFixtureBaseTest
    {
        private readonly ICrmContactService _crmContactService;

        public CrmContactServiceTests(DatabaseFixture databaseFixture) : base(databaseFixture)
        {
            _crmContactService = new CrmContactService(DatabaseFixture.Mapper, DatabaseFixture.Context);
        }

        private CRMContact AddCrmContact(int organizationId, string contactName = "Test Contact", string contactTitle = "Contact Title", string contactEmail = "email@test.com", string contactPhone = "949-999-9999")
        {
            var crmContact = new CRMContact
            {
                OrganizationId = organizationId,
                ContactName = contactName,
                ContactTitle = contactTitle,
                ContactEmail = contactEmail,
                ContactPhone = contactPhone
            };
            DatabaseFixture.Context.CRMContact.Add(crmContact);
            DatabaseFixture.Context.SaveChanges();

            return crmContact;
        }

        [Fact]
        public async Task When_GetCrmContactsByOrganization_ForOrgWithNoCrmContacts()
        {
            var actualCrmContacts = await _crmContactService.GetCrmContactsByOrganization(DatabaseFixture
                .TestOrganizationGrandBend
                .OrganizationId);
            Assert.Empty(actualCrmContacts);
        }

        [Fact]
        public async Task When_GetCrmContactsByOrganization_ForOrgCrmContacts()
        {
            var expectedCrmContact =
                AddCrmContact(DatabaseFixture.TestOrganizationGrandBend.OrganizationId, "Test GrandBend");
            var expectedAdditionalCrmContact =
                AddCrmContact(DatabaseFixture.TestOrganizationGrandBend.OrganizationId, "Test GrandBend 2");
            AddCrmContact(DatabaseFixture.TestOrganizationWarnerUnified.OrganizationId, "Test Warner");
            var actualCrmContacts = (await _crmContactService.GetCrmContactsByOrganization(DatabaseFixture
                .TestOrganizationGrandBend
                .OrganizationId)).ToList();
            Assert.Equal(2, actualCrmContacts.Count);
            var actualCrmContact = actualCrmContacts.SingleOrDefault(acc => acc.CrmContactId == expectedCrmContact.CRMContactId);
            Assert.NotNull(actualCrmContact);
            Assert.Equal(expectedCrmContact.Organization.LocalOrganizationID, actualCrmContact.LocalOrganizationId);
            Assert.Equal(expectedCrmContact.ContactName, actualCrmContact.ContactName);
            Assert.Equal(expectedCrmContact.ContactTitle, actualCrmContact.ContactTitle);
            Assert.Equal(expectedCrmContact.ContactEmail, actualCrmContact.ContactEmail);
            Assert.Equal(expectedCrmContact.ContactPhone, actualCrmContact.ContactPhone);
            var actualAdditionalCrmContact =
                actualCrmContacts.SingleOrDefault(acc => acc.CrmContactId == expectedAdditionalCrmContact.CRMContactId);
            Assert.NotNull(actualAdditionalCrmContact);
            Assert.Equal(expectedAdditionalCrmContact.Organization.LocalOrganizationID, actualAdditionalCrmContact.LocalOrganizationId);
            Assert.Equal(expectedAdditionalCrmContact.ContactName, actualAdditionalCrmContact.ContactName);
            Assert.Equal(expectedAdditionalCrmContact.ContactTitle, actualAdditionalCrmContact.ContactTitle);
            Assert.Equal(expectedAdditionalCrmContact.ContactEmail, actualAdditionalCrmContact.ContactEmail);
            Assert.Equal(expectedAdditionalCrmContact.ContactPhone, actualAdditionalCrmContact.ContactPhone);
        }
   }
}