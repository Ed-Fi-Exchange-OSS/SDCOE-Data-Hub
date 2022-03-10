using System.Collections.Generic;
using System.Linq;
using DataHub.Api.Authorization;
using DataHub.Api.Enums;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DataHub.Api.Data.Models
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string LocalOrganizationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public UserRole Role { get; set; }
        [JsonProperty(ItemConverterType = typeof(StringEnumConverter))]
        public IEnumerable<UserPermission> Permissions => RolePermissionProvider.GetPermissionsForRole(Role);
    }
}