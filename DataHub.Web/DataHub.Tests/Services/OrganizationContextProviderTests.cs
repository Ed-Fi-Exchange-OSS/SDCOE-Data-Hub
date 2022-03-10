using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataHub.Api.Data.Entities;
using DataHub.Api.Enums;
using DataHub.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Identity.Client;
using Moq;
using Xunit;

namespace DataHub.Tests.Services
{
    public class OrganizationContextProviderTests : DatabaseFixtureBaseTest
    {
        public OrganizationContextProviderTests(DatabaseFixture databaseFixture) : base(databaseFixture)
        {
        }

        private User AddUser(int organizationId, string firstName = "Test", string lastName = "User",
            string emailAddress = "testuser@email.com", UserRole role = UserRole.DistrictSuperUser,
            RecordStatus status = RecordStatus.Active)
        {
            var user = new User
            {
                OrganizationId = organizationId,
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress,
                Role = role,
                Status = (byte)status
            };
            DatabaseFixture.Context.User.Add(user);
            DatabaseFixture.Context.SaveChanges();

            return user;
        }

        private Mock<IHttpContextAccessor> GetHttpContextAccessor(bool isAuthenticated, string email,
            string localOrganizationIdByHeader)
        {
            var mock = new Mock<IHttpContextAccessor>(MockBehavior.Strict);
            var context = new DefaultHttpContext();
            if (isAuthenticated)
            {
                context.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim("preferred_username", email)
                }, "FakeAuth"));
                if (localOrganizationIdByHeader != null)
                {
                    context.Request.Headers.Add("x-local-organization-id", new StringValues(localOrganizationIdByHeader));
                }
            }

            mock.Setup(m => m.HttpContext)
                .Returns(context);

            return mock;
        }

        [Fact]
        public async Task GetUser_WhenUnauthenticatedUser_ShouldReturnNull()
        {
            var httpContextAccessor = GetHttpContextAccessor(false, null, null);

            var organizationContextProvider = new OrganizationContextProvider(DatabaseFixture.Context,
                httpContextAccessor.Object, DatabaseFixture.Mapper);

            var user = await organizationContextProvider.GetUser().ConfigureAwait(false);
            Assert.Null(user);
        }

        [Fact]
        public async Task GetUser_WhenAuthenticatedUser_ShouldReturnUserDetailsMatchingAssignment()
        {
            var expectedOrgId = DatabaseFixture.TestOrganizationWarnerUnified.OrganizationId;
            var expectedUser = AddUser(expectedOrgId);
            var httpContextAccessor = GetHttpContextAccessor(true, expectedUser.EmailAddress, null);

            var organizationContextProvider = new OrganizationContextProvider(DatabaseFixture.Context,
                httpContextAccessor.Object, DatabaseFixture.Mapper);

            var user = await organizationContextProvider.GetUser().ConfigureAwait(false);
            Assert.Equal(DatabaseFixture.TestOrganizationWarnerUnified.LocalOrganizationID, user.LocalOrganizationId);
            Assert.Equal(expectedUser.Role, user.Role);
            Assert.Equal(expectedUser.EmailAddress, user.EmailAddress);
        }

        [Fact]
        public async Task GetCurrentOrganizationId_WhenAuthenticatedAdminUserWithHeader_ShouldReturnOrgIdMatchingHeader()
        {
            var userAssociatedOrgId = DatabaseFixture.TestOrganizationWarnerUnified.OrganizationId;
            var expectedOrgId = DatabaseFixture.TestOrganizationGrandBend.OrganizationId;
            var expectedUser = AddUser(userAssociatedOrgId, role: UserRole.InternalAdministrator);
            var httpContextAccessor = GetHttpContextAccessor(true, expectedUser.EmailAddress, DatabaseFixture.TestOrganizationGrandBend.LocalOrganizationID);

            var organizationContextProvider = new OrganizationContextProvider(DatabaseFixture.Context,
                httpContextAccessor.Object, DatabaseFixture.Mapper);

            var currentOrganizationId = await organizationContextProvider.GetCurrentOrganizationId().ConfigureAwait(false);
            Assert.Equal(expectedOrgId, currentOrganizationId);
        }

        [Fact]
        public async Task GetCurrentOrganizationId_WhenAuthenticatedNonAdminUserWithHeader_ShouldIgnoreHeader()
        {
            // Passing in header for user that is not admin should be ignored. User is associated with Warner
            // and GrandBend is being passed in. Result should still be Warner's org ID.
            var expectedOrgId = DatabaseFixture.TestOrganizationWarnerUnified.OrganizationId;
            var expectedUser = AddUser(expectedOrgId, role: UserRole.DistrictSuperUser);
            var httpContextAccessor = GetHttpContextAccessor(true, expectedUser.EmailAddress, DatabaseFixture.TestOrganizationGrandBend.LocalOrganizationID);

            var organizationContextProvider = new OrganizationContextProvider(DatabaseFixture.Context,
                httpContextAccessor.Object, DatabaseFixture.Mapper);

            var currentOrganizationId = await organizationContextProvider.GetCurrentOrganizationId().ConfigureAwait(false);
            Assert.Equal(expectedOrgId, currentOrganizationId);
        }
    }
}