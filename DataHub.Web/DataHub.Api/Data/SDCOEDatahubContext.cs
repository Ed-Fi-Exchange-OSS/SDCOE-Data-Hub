﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DataHub.Api.Data.Entities;

namespace DataHub.Api.Data
{
    public partial class SDCOEDatahubContext : DbContext
    {
        public SDCOEDatahubContext()
        {
        }

        public SDCOEDatahubContext(DbContextOptions<SDCOEDatahubContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActivityLog> ActivityLog { get; set; }
        public virtual DbSet<Announcement> Announcement { get; set; }
        public virtual DbSet<CRMContact> CRMContact { get; set; }
        public virtual DbSet<EdFiODS> EdFiODS { get; set; }
        public virtual DbSet<EdFiODSClient> EdFiODSClient { get; set; }
        public virtual DbSet<EdFiRequest> EdFiRequest { get; set; }
        public virtual DbSet<Extract> Extract { get; set; }
        public virtual DbSet<ODSStatus> ODSStatus { get; set; }
        public virtual DbSet<ODSStatusJob> ODSStatusJob { get; set; }
        public virtual DbSet<Offering> Offering { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<Participation> Participation { get; set; }
        public virtual DbSet<Support> Support { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivityLog>(entity =>
            {
                entity.ToTable("ActivityLog", "datahub");

                entity.Property(e => e.AsOfDate).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.ActivityLogs)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityOrganizationId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ActivityLogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityUserId");
            });

            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.ToTable("Announcement", "datahub");

                entity.Property(e => e.DisplayUntilDate).HasColumnType("date");

                entity.Property(e => e.Message).IsRequired();

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Announcements)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AnnouncementOrganizationId");
            });

            modelBuilder.Entity<CRMContact>(entity =>
            {
                entity.ToTable("CRMContact", "datahub");

                entity.Property(e => e.ContactEmail).HasMaxLength(128);

                entity.Property(e => e.ContactName).HasMaxLength(60);

                entity.Property(e => e.ContactPhone).HasMaxLength(60);

                entity.Property(e => e.ContactTitle).HasMaxLength(60);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.CRMContacts)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CRMContactsOrganizationID");
            });

            modelBuilder.Entity<EdFiODS>(entity =>
            {
                entity.ToTable("EdFiODS", "datahub");

                entity.HasIndex(e => e.EdFiODSNo)
                    .HasName("UX_datahub_EdFiODS_EdFiODSNo")
                    .IsUnique();

                entity.Property(e => e.LastModifiedOn)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ODSKey).HasMaxLength(50);

                entity.Property(e => e.ODSName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ODSPath).HasMaxLength(255);

                entity.Property(e => e.ODSSecret).HasMaxLength(100);

                entity.Property(e => e.ODSURL).HasMaxLength(255);

                entity.Property(e => e.ODSVersion).HasMaxLength(255);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.EdFiODs)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK_EdFiODSOrganizationID");
            });

            modelBuilder.Entity<EdFiODSClient>(entity =>
            {
                entity.ToTable("EdFiODSClient", "datahub");

                entity.HasIndex(e => new { e.EdFiODSId, e.ODSKey })
                    .HasName("UX_datahub_EdFiODSClient_EdFiODSOdsKey")
                    .IsUnique();

                entity.Property(e => e.ApplicationName).HasMaxLength(255);

                entity.Property(e => e.ClaimSetName).HasMaxLength(255);

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastModifiedOn)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ODSKey)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ODSSecret)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.VendorName).HasMaxLength(255);

                entity.HasOne(d => d.EdFiODS)
                    .WithMany(p => p.EdFiODSClients)
                    .HasForeignKey(d => d.EdFiODSId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EdFiODSClientEdFiODSId");
            });

            modelBuilder.Entity<EdFiRequest>(entity =>
            {
                entity.ToTable("EdFiRequest", "datahub");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.LastModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RequestDate).HasColumnType("date");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.EdFiRequestCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_EdFiRequestCreatedBy");

                entity.HasOne(d => d.LastModifiedByNavigation)
                    .WithMany(p => p.EdFiRequestLastModifiedByNavigations)
                    .HasForeignKey(d => d.LastModifiedBy)
                    .HasConstraintName("FK_EdFiRequestLastModifiedBy");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.EdFiRequests)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EdFiRequestOrganizationID");
            });

            modelBuilder.Entity<Extract>(entity =>
            {
                entity.ToTable("Extract", "datahub");

                entity.Property(e => e.ExtractFrequency).HasMaxLength(128);

                entity.Property(e => e.ExtractJobName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.ExtractLastDate).HasColumnType("date");

                entity.Property(e => e.ExtractLastStatus).HasMaxLength(128);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Extracts)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExtractsOrganizationID");
            });

            modelBuilder.Entity<ODSStatus>(entity =>
            {
                entity.ToTable("ODSStatus", "datahub");

                entity.HasIndex(e => new { e.EdFiODSId, e.ODSStatusJobId })
                    .HasName("UX_datahub_ODSStatus_EdFiODSId_ODSStatusJob")
                    .IsUnique();

                entity.Property(e => e.LastUpdateDate).HasColumnType("date");

                entity.Property(e => e.StatusReadout).HasMaxLength(255);

                entity.HasOne(d => d.EdFiODS)
                    .WithMany(p => p.ODSStatuses)
                    .HasForeignKey(d => d.EdFiODSId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ODSStatus_EdFiODSId");

                entity.HasOne(d => d.ODSStatusJob)
                    .WithMany(p => p.ODSStatuses)
                    .HasForeignKey(d => d.ODSStatusJobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ODSStatus_ODSStatusJobId");
            });

            modelBuilder.Entity<ODSStatusJob>(entity =>
            {
                entity.ToTable("ODSStatusJob", "datahub");

                entity.Property(e => e.ODSVersion)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.StatusJobName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Offering>(entity =>
            {
                entity.ToTable("Offering", "datahub");

                entity.HasIndex(e => e.ItemNo)
                    .HasName("UX_datahub_Offering_ItemNo")
                    .IsUnique();

                entity.Property(e => e.AssociatedCost).HasMaxLength(255);

                entity.Property(e => e.ContactEmail).HasMaxLength(128);

                entity.Property(e => e.ContactName).HasMaxLength(80);

                entity.Property(e => e.ContactPhone).HasMaxLength(40);

                entity.Property(e => e.ItemDescription).HasMaxLength(1024);

                entity.Property(e => e.ItemName).HasMaxLength(80);

                entity.Property(e => e.ProductURL).HasMaxLength(1024);
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.ToTable("Organization", "datahub");

                entity.HasIndex(e => e.LocalOrganizationID)
                    .HasName("UX_datahub_Organization_LocalOrganizationID")
                    .IsUnique();

                entity.Property(e => e.AnalyticsSystem).HasMaxLength(80);

                entity.Property(e => e.DominantDataSystem).HasMaxLength(80);

                entity.Property(e => e.FederalOrganizationID).HasMaxLength(10);

                entity.Property(e => e.InterimAssessments).HasMaxLength(80);

                entity.Property(e => e.LocalOrganizationID)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.OrganizationAbbreviation).HasMaxLength(60);

                entity.Property(e => e.OrganizationName)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.SIS).HasMaxLength(80);
            });

            modelBuilder.Entity<Participation>(entity =>
            {
                entity.HasKey(e => new { e.OrganizationId, e.OfferingId });

                entity.ToTable("Participation", "datahub");

                entity.Property(e => e.AsOfDate).HasColumnType("date");

                entity.HasOne(d => d.Offering)
                    .WithMany(p => p.Participations)
                    .HasForeignKey(d => d.OfferingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ParticipationOfferingId");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Participations)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ParticipationOrganizationId");
            });

            modelBuilder.Entity<Support>(entity =>
            {
                entity.ToTable("Support", "datahub");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.SystemID).HasMaxLength(20);

                entity.Property(e => e.TicketID).HasMaxLength(20);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Supports)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK_SupportOrganizationID");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "datahub");

                entity.HasIndex(e => e.EmailAddress)
                    .HasName("IX_datahub_User_EmailAddress")
                    .IsUnique();

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrganizationId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}