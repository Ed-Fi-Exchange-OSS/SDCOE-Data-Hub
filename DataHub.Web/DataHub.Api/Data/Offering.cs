﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace DataHub.Api.Data.Entities
{
    public partial class Offering
    {
        public Offering()
        {
            Participations = new HashSet<Participation>();
        }

        public int OfferingId { get; set; }
        public int ItemNo { get; set; }
        public byte ItemCategoryType { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public byte ItemType { get; set; }
        public string AssociatedCost { get; set; }
        public string ProductURL { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }

        public virtual ICollection<Participation> Participations { get; set; }
    }
}