-- EdFiODS
-- USE: ODS summary for district, including version, status and keys associated with the district
--  Regarding KEY, May not need a database if these can be read from, say the Ed-Fi ADMIN DB
--  Also, refs: 
--      https://techdocs.ed-fi.org/display/ODSAPI23/_How+To%3A+Manage+Keys+and+Secrets
--      https://techdocs.ed-fi.org/pages/viewpage.action?pageId=75106590
-- IMPORTANT NOTE: We are looking for assistance on this table design, as we are unsure which ODS-identifying artifacts are 
--      available and make sense to store, thus we are unsure about:
--          [ODSName], [ODSPath], [ODSURL] <<< These were included to identify the ODS and access to it
-- LOGIC: this table can be populated manually (UNDESIRED), or optimally refreshed via reading ed-fi Admin data (DESIRED)
--      Might be a regular SQL Agent job, Scheduled Task, CRON
--      The table can be UPDATED (lastcheckdate, and maybe ODSKey, ODSSecret)
--      
-- WIDGET NOTES: EdFiODS, ODSStatus and ODSStatusJobs work in concert 
--   EdFiODS contains a summary of ODS's for a given LocalOrganizationID
-- NOTE: We dont have a primary key defined for this table, but it could be the EdFiODSNo OR LocalOrganizationID, EdFiODSNo 
--  In the example below - the primay deky is EdFiODSNo and is not autoincrementing or defaulted in this example.
CREATE TABLE [datahub].[EdFiODS](
    [EdFiODSId] [int] IDENTITY(1,1) PRIMARY KEY,
    [EdFiODSNo] [int] NOT NULL,
    [OrganizationId] [int] NULL,
    [ODSName] [nvarchar](255) NOT NULL,
    [ODSPath] [nvarchar](255) NULL,
    [ODSURL] [nvarchar](255) NULL,
    [ODSVersion] [nvarchar](255) NULL,
    [ODSKey] [nvarchar](50) NULL,
    [ODSSecret] [nvarchar](100) NULL,
    [LastModifiedOn] [date] NOT NULL,
    CONSTRAINT FK_EdFiODSOrganizationID FOREIGN KEY ([OrganizationId]) REFERENCES datahub.Organization([OrganizationId])
) ON [PRIMARY] 
GO
ALTER TABLE [datahub].[EdFiODS] ADD CONSTRAINT [DF_datahub_EdFiODS_LastModifiedOn] DEFAULT (GETDATE()) FOR [LastModifiedOn]
CREATE UNIQUE INDEX [UX_datahub_EdFiODS_EdFiODSNo] ON [datahub].[EdFiODS] ([EdFiODSNo])
GO