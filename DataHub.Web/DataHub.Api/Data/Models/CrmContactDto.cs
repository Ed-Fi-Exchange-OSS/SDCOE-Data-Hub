namespace DataHub.Api.Data.Models
{
    public class CrmContactDto
    {
        public int CrmContactId { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string LocalOrganizationId { get; set; }
    }
}