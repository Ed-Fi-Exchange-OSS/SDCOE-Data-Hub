-- ODSSTATUS    Status of counts and last updates by ODS - CAN BE DEFINED BY ODSStatusJobs
-- This data could be populated by a nightly/hourly process to query counts from specific ODS tables as well as last date table updated     
--
-- LOGIC: this table can be populated manually (UNDESIRED), or optimally refreshed via reading ed-fi ODS data (DESIRED)
-- WIDGET NOTES: EdFiODS, ODSStatus and ODSStatusJobs work in concert 
--   ODSStatus contains status job run results for specific ODSs
CREATE TABLE [datahub].[ODSStatus](
    [ODSStatusId] [int] IDENTITY(1,1) PRIMARY KEY,
    [EdFiODSId] [int] NOT NULL,
    [ODSStatusJobId] [int] NOT NULL,
    [StatusReadout] [nvarchar](255) NULL,
    [RecordCount] [int] NULL,
    [LastUpdateDate] [date] NULL,
    CONSTRAINT FK_ODSStatus_EdFiODSId FOREIGN KEY ([EdFiODSId]) REFERENCES datahub.EdFiODS([EdFiODSId]),
    CONSTRAINT FK_ODSStatus_ODSStatusJobId FOREIGN KEY ([ODSStatusJobId]) REFERENCES datahub.ODSStatusJob([ODSStatusJobId])
) ON [PRIMARY]
GO
CREATE UNIQUE INDEX [UX_datahub_ODSStatus_EdFiODSId_ODSStatusJob] ON [datahub].[ODSStatus] ([EdFiODSId], [ODSStatusJobId])
GO