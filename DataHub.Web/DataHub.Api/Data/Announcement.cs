﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace DataHub.Api.Data.Entities
{
    public partial class Announcement
    {
        public int AnnouncementId { get; set; }
        public int OrganizationId { get; set; }
        public string Message { get; set; }
        public DateTime? DisplayUntilDate { get; set; }
        public byte Status { get; set; }

        public virtual Organization Organization { get; set; }
    }
}