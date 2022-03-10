using System;

namespace DataHub.Api.Data.Models
{
    public class ExtractDto
    {
        public int ExtractId { get; set; }
        public string OrganizationAbbreviation { get; set; }
        public string ExtractJobName { get; set; }
        public string ExtractFrequency { get; set; }
        public string ExtractLastStatus { get; set; }
        public DateTime ExtractLastDate { get; set; }
    }
}