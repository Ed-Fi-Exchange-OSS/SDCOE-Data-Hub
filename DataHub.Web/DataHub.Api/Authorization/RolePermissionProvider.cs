using System.Collections.Generic;
using DataHub.Api.Enums;

namespace DataHub.Api.Authorization
{
    public static class RolePermissionProvider
    {
        public static IEnumerable<UserPermission> GetPermissionsForRole(UserRole role)
        {
            switch (role)
            {
                case UserRole.InternalAdministrator:
                    yield return UserPermission.ManageAllOrganizations;
                    yield return UserPermission.ViewAllOrganizations;
                    yield return UserPermission.ManageMyOrganization;
                    yield return UserPermission.ViewMyOrganization;
                    yield return UserPermission.CanUpdateEdFiRequestStatus;
                    yield return UserPermission.CanCreateEdFiRequest;
                    yield return UserPermission.CanCreateParticipation;
                    yield return UserPermission.CanDeleteParticipation;
                    break;
                case UserRole.DistrictSuperUser:
                    yield return UserPermission.ManageMyOrganization;
                    yield return UserPermission.ViewMyOrganization;
                    yield return UserPermission.CanCreateEdFiRequest;
                    yield return UserPermission.CanCreateParticipation;
                    yield return UserPermission.CanDeleteParticipation;
                    break;
                case UserRole.DistrictViewer:
                    yield return UserPermission.ViewMyOrganization;
                    break;
            }
        }
    }
}
