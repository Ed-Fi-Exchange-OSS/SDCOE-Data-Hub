-- Announcements
-- NOTE: Status 0 - Inactive, 1 - Active
CREATE TABLE [datahub].[Announcement](
    [AnnouncementId] [int] IDENTITY(1,1) PRIMARY KEY,
    [OrganizationId] [int] NOT NULL,
    [Message] nvarchar(max) NOT NULL,
    [DisplayUntilDate] [date] NULL,
    [Status] [tinyint] NOT NULL,
    CONSTRAINT FK_AnnouncementOrganizationId FOREIGN KEY ([OrganizationId]) REFERENCES datahub.Organization([OrganizationId])
) ON [PRIMARY]
GO