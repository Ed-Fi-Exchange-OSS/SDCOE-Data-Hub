using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DataHub.Api.Data.Models
{
    public class EdFiOdsStatusDto
    {
        public int EdFiOdsNo { get; set; }
        public string OdsName { get; set; }
        public string OdsVersion { get; set; }
        public string OdsUrl { get; set; }
        public string OdsPath { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OdsStatus Status { get; set; }
        public DateTime LastCheckedDate { get; set; }
        public List<EdFiOdsClient> EdFiOdsClients { get; set; } = new List<EdFiOdsClient>();
        public List<EdFiOdsResourceCount> ResourceCounts { get; set; } = new List<EdFiOdsResourceCount>();
    }

    public class EdFiOdsClient
    {
        public string VendorName { get; set; }
        public string ApplicationName { get; set; }
        public string ClaimSetName { get; set; }
        public string ClientName { get; set; }
        public string OdsKey { get; set; }
        public string OdsSecret { get; set; }
    }

    public class EdFiOdsResourceCount
    {
        public string ResourceName { get; set; }
        public int ResourceCount { get; set; }
        public DateTime LastCheckedDate { get; set; }
    }

    public enum OdsStatus
    {
        Unknown = 0,
        Available = 1,
        Offline = 2
    }
}