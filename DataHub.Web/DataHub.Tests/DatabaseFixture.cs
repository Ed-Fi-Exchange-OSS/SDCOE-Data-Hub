using System;
using System.Linq;
using AutoMapper;
using DataHub.Api.Data;
using DataHub.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataHub.Tests
{
    public class DatabaseFixture : IDisposable
    {
        public DatabaseFixture()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SDCOEDatahubContext>().UseSqlServer("Server=(local);Database=SDCOE_DataHub;Trusted_Connection=True;");
            Context = new SDCOEDatahubContext(optionsBuilder.Options);
            SetupSharedSampleData();

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(typeof(EntityToModelProfile)));
            Mapper = configuration.CreateMapper();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public SDCOEDatahubContext Context { get; }
        public IMapper Mapper { get; }

        public Organization TestOrganizationGrandBend { get; private set; }
        public Organization TestOrganizationWarnerUnified { get; private set; }

        private void SetupSharedSampleData()
        {
            TestOrganizationGrandBend = Context.Organization.SingleOrDefault(o => o.OrganizationName == "Grand Bend ISD");
            if (TestOrganizationGrandBend == null)
            {
                TestOrganizationGrandBend = new Organization
                {
                    LocalOrganizationID = "255901000000", OrganizationName = "Grand Bend ISD",
                    FederalOrganizationID = "123456", EducationOrganizationID = 255901
                };
                Context.Organization.Add(TestOrganizationGrandBend);
            }
            TestOrganizationWarnerUnified = Context.Organization.SingleOrDefault(o => o.OrganizationName == "Warner Unified");
            if (TestOrganizationWarnerUnified == null)
            {
                TestOrganizationWarnerUnified = new Organization
                {
                    LocalOrganizationID = "37754160000000",
                    OrganizationName = "Warner Unified",
                    FederalOrganizationID = "0600042",
                    EducationOrganizationID = 3775416
                };
                Context.Organization.Add(TestOrganizationWarnerUnified);
            }

            Context.SaveChanges();
        }
    }
}
