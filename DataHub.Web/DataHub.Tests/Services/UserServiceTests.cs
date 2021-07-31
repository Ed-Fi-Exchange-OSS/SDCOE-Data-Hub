using System.Threading.Tasks;
using DataHub.Api.Data.Entities;
using DataHub.Api.Enums;
using DataHub.Api.Services;
using Xunit;

namespace DataHub.Tests.Services
{
    public class UserServiceTests : DatabaseFixtureBaseTest
    {
        private readonly IUserService _userService;

        public UserServiceTests(DatabaseFixture databaseFixture) : base(databaseFixture)
        {
            _userService = new UserService(DatabaseFixture.Mapper, DatabaseFixture.Context);
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
                Status = (byte) status
            };
            DatabaseFixture.Context.User.Add(user);
            DatabaseFixture.Context.SaveChanges();

            return user;
        }

        [Fact]
        public async Task When_GetUser_NoExists()
        {
            var actualUser = await _userService.GetUser(0);
            Assert.Null(actualUser);
        }

        [Fact]
        public async Task When_GetUser_Exists()
        {
            var expectedUser = AddUser(DatabaseFixture.TestOrganizationGrandBend.OrganizationId);
            var actualUser = await _userService.GetUser(expectedUser.UserId);
            Assert.Equal(expectedUser.UserId, actualUser.UserId);
            Assert.Equal(expectedUser.FirstName, actualUser.FirstName);
            Assert.Equal(expectedUser.LastName, actualUser.LastName);
            Assert.Equal(expectedUser.EmailAddress, actualUser.EmailAddress);
            Assert.Equal((UserRole) expectedUser.Role, actualUser.Role);
        }
    }
}