using DataHub.Api.Enums;

namespace DataHub.Api.Data.Models
{
    public class SupportDto
    {
        public int SupportId { get; set; }
        public string SystemId { get; set; }
        public string TicketId { get; set; }
        public string Description { get; set; }
        public SupportStatus Status { get; set; }
    }
}