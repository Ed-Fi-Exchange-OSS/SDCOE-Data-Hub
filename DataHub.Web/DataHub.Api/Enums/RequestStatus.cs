namespace DataHub.Api.Enums
{
    public enum RequestStatus : byte
    {
        Unknown = 0,
        Requested = 1,
        InProgress = 2,
        Completed = 3,
        Denied = 4
    }
}