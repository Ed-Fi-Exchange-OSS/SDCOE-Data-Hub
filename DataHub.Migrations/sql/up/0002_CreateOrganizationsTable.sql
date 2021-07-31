-- Organizations - These are districts within San Diego County, and this table is used as a lookup
CREATE TABLE [datahub].[Organization](
    [OrganizationId] [int] IDENTITY(1,1) PRIMARY KEY,
    [OrganizationName] [nvarchar](80) NOT NULL,
    [OrganizationAbbreviation] [nvarchar](60) NULL,
    [LocalOrganizationID] [nvarchar](20) NOT NULL,
    [FederalOrganizationID] [nvarchar](10) NULL,
    [EducationOrganizationID] [int] NULL,
    [SIS] [nvarchar](80) NULL,
    [DominantDataSystem] [nvarchar](80) NULL,
    [AnalyticsSystem] [nvarchar](80) NULL,
    [InterimAssessments] [nvarchar](80) NULL
) ON [PRIMARY]
GO
CREATE UNIQUE INDEX [UX_datahub_Organization_LocalOrganizationID] ON [datahub].[Organization] ([LocalOrganizationID]);
GO