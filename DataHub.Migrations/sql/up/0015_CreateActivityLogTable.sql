-- ActivityLog
-- NOTE: ChangeType 1 - Service, 2 - Product, 3 - Ed-Fi
CREATE TABLE [datahub].[ActivityLog](
    [ActivityLogId] [int] IDENTITY(1,1) PRIMARY KEY,
    [UserId] [int] NOT NULL,
    [OrganizationId] [int] NOT NULL,
    [ChangeType] [tinyint] NULL,
    [Description] [nvarchar](80) NULL,
    [AsOfDate] [date] NOT NULL,
    CONSTRAINT FK_ActivityOrganizationId FOREIGN KEY ([OrganizationId]) REFERENCES datahub.Organization([OrganizationId]),
    CONSTRAINT FK_ActivityUserId FOREIGN KEY (UserId) REFERENCES datahub.[User](UserId)
) ON [PRIMARY]
GO