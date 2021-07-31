using DataHub.Api.Enums;

namespace DataHub.Api.Data.Models
{
    public class OfferingDto
    {
        public int OfferingId { get; set; }
        public int ItemNo { get; set; }
        public ItemCategoryType ItemCategoryType { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public ItemType ItemType { get; set; }
        public string AssociatedCost { get; set; }
        public string ProductUrl { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
    }
}