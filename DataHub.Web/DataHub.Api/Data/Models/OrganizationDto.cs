namespace DataHub.Api.Data.Models
{
    public class OrganizationDto
    {
        public string OrganizationName { get; set; }
        public string LocalOrganizationID { get; set; }
        public string SIS { get; set; }
        public string DominantDataSystem { get; set; }
        public string AnalyticsSystem { get; set; }
        public string InterimAssessments { get; set; }
    }
}