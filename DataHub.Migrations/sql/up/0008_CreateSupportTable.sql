-- Support
-- USE: Simulated API interface for ServiceNow ticketing, PeopleSoft notifications
-- NOTE: Status 0 - New, 1 - Active, 2 - Closed
CREATE TABLE [datahub].[Support] (
    [SupportId] [int] IDENTITY(1,1) PRIMARY KEY,
    [OrganizationId] [int] NULL,
    [SystemID] [nvarchar](20) NULL,
    [TicketID] [nvarchar](20) NULL,
    [Description] [nvarchar](255) NULL,
    [Status] [tinyint] NOT NULL,
    CONSTRAINT FK_SupportOrganizationID FOREIGN KEY ([OrganizationId]) REFERENCES datahub.Organization([OrganizationId])
) ON [PRIMARY] 
GO