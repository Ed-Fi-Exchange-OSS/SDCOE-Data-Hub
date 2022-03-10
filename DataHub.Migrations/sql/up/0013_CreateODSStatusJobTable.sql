-- ODSSTATUSJOBS    
--      Defines periodic status checks on ODS, which tables/entities will be queried for which versions of Ed-Fi        
--      These definitions are system-wide and set up as needed to define regular checks
--    
-- WIDGET NOTES: EdFiODS, ODSStatus and ODSStatusJobs work in concert 
--   ODSStatus contains status update job definitions - and these check-ups would be run regularly through some scheduled process
--
-- Question !!!! Does the ODS version matter on these count queries?  Might support 2.5 and 3.4
--
-- ODSCheckTypes 
--  Notes !!!! regarding operation included
--  1 - "ODS Up and Active Check" 
--      - This would test access to the ODS and the process would save the status to the associated ODSStatus record,
--          creating the row if it doesn't exist and updating the row if it already exists
--  2 - "Entity row count and last update"
--      - This would run a query against ODS and save both row count result and max(update date) to the associated ODSStatus record,
--          creating the row if it doesn't exist and updating the row if it already exists
--  3 - "Reserved"
CREATE TABLE [datahub].[ODSStatusJob](
    [ODSStatusJobId] [int] IDENTITY(1,1) PRIMARY KEY,
    [ODSStatusJobNo] [int] NOT NULL,
    [StatusJobName] [nvarchar] (255) NOT NULL,
    [ODSVersion] [nvarchar] (60) NOT NULL,
    [ODSCheckType] [tinyint] NOT NULL,
    [EntityTableToQuery] nvarchar(max) NULL,
    [FilterConditions] nvarchar(max) NULL
) ON [PRIMARY]
GO
