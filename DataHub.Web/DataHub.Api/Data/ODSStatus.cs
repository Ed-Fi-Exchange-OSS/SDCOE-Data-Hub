﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace DataHub.Api.Data.Entities
{
    public partial class ODSStatus
    {
        public int ODSStatusId { get; set; }
        public int EdFiODSId { get; set; }
        public int ODSStatusJobId { get; set; }
        public string StatusReadout { get; set; }
        public int? RecordCount { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public virtual EdFiODS EdFiODS { get; set; }
        public virtual ODSStatusJob ODSStatusJob { get; set; }
    }
}