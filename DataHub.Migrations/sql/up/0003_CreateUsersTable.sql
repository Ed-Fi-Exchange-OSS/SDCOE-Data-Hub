-- Users - Data Hub users, one of 4 roles as follows.  Users would be associated with an organization.  
-- Staff and Administrator users would be SDCOE employees, so these would be associated with 37103710000000 (SDCOE) for now
-- NOTE: Status: 0 - inactive, 1 - Active 
-- NOTE: Role: 1 - InternalAdministrator, 2 - DistrictSuperUser, 3 - DistrictViewer
-- NOTE: Role values are currently aligned with wireframe requirements, which differed slightly from the schema originally sent from SDCOE.
--       These original values were: Role: 1 - External User (reserved), 2 - DistrictUser, 3 - Staff User (Internal), 4 - Administrator (Internal)
CREATE TABLE [datahub].[User](
    [UserId] [int] IDENTITY(1,1) PRIMARY KEY,
    [OrganizationId] [int] NOT NULL,
    [FirstName] [nvarchar](75) NOT NULL,
    [LastName] [nvarchar](75) NOT NULL,
    [Role] [tinyint] NOT NULL,
    [EmailAddress] [nvarchar](128) NOT NULL,
    [Status] [tinyint] NOT NULL,
    CONSTRAINT FK_OrganizationId FOREIGN KEY ([OrganizationId]) REFERENCES datahub.Organization([OrganizationId])
) ON [PRIMARY]
GO
ALTER TABLE [datahub].[User] ADD CONSTRAINT [DF_datahub_Users_Status]  DEFAULT ((1)) FOR [Status]
GO

CREATE UNIQUE INDEX [IX_datahub_User_EmailAddress] ON [datahub].[User] ([EmailAddress])
GO
