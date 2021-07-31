-- CRMContacts
CREATE TABLE [datahub].[CRMContact](
    [CRMContactId] [int] IDENTITY(1,1) PRIMARY KEY,
    [OrganizationId] [int] NOT NULL,
    [ContactName] [nvarchar](60) NULL,
    [ContactTitle] [nvarchar](60) NULL,
    [ContactEmail] [nvarchar](128) NULL,
    [ContactPhone] [nvarchar](60) NULL,
    CONSTRAINT FK_CRMContactsOrganizationID FOREIGN KEY ([OrganizationId]) REFERENCES datahub.Organization([OrganizationId])
) ON [PRIMARY] 
GO