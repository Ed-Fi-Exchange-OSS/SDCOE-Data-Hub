namespace DataHub.Api.Enums
{
    public enum UserPermission : byte
    {
        Unknown = 0,
        ManageAllOrganizations = 1,
        ViewAllOrganizations = 2,
        ManageMyOrganization = 3,
        ViewMyOrganization = 4,
        CanUpdateEdFiRequestStatus = 5,
        CanCreateEdFiRequest = 6,
        CanCreateParticipation = 7,
        CanDeleteParticipation = 8
    }
}