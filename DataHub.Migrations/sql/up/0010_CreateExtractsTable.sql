-- Extracts
-- Note:
-- Jobfrequency varchar(128) [maybe enum or flag: 1 daily, 2 weekly, 3 monthly, 4 on demand]
-- Jobstatus    varchar(128) [maybe enum or flag: 1 Active extract - regularly scheduled, 2 disabled - temporary, 3 inactive]
CREATE TABLE [datahub].[Extract](
    [ExtractId] [int] IDENTITY(1,1) PRIMARY KEY,
    [OrganizationId] [int] NOT NULL,
    [ExtractJobName] [nvarchar](128) NOT NULL,
    [ExtractFrequency] [nvarchar](128) NULL,
    [ExtractLastStatus] [nvarchar](128) NULL,
    [ExtractLastDate] [date] NULL,
    CONSTRAINT FK_ExtractsOrganizationID FOREIGN KEY ([OrganizationId]) REFERENCES datahub.Organization([OrganizationId])
) ON [PRIMARY] 
GO