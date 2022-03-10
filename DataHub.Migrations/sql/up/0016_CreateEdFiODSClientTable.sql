-- EdFiODSClient
-- USE: Contains client information for all clients for a given ODS
--  Would source from the Admin database in most situations
--  Duplicated here in DataHub to work around concerns for encryption and the lack of an Admin Api
-- WIDGET NOTES: Provided to the user as additional information off the EdFiODS widget
CREATE TABLE [datahub].[EdFiODSClient](
    [EdFiODSClientId] [int] IDENTITY(1,1) PRIMARY KEY,
    [EdFiODSId] [int] NOT NULL,
    [VendorName] [nvarchar](255) NULL,
    [ApplicationName] [nvarchar](255) NULL,
    [ClaimSetName] [nvarchar](255) NULL,
    [ClientName] [nvarchar](100) NOT NULL,
    [ODSKey] [nvarchar](50) NOT NULL,
    [ODSSecret] [nvarchar](100) NOT NULL,
    [LastModifiedOn] [date] NOT NULL,
    CONSTRAINT FK_EdFiODSClientEdFiODSId FOREIGN KEY ([EdFiODSId]) REFERENCES datahub.EdFiODS([EdFiODSId])
) ON [PRIMARY]
GO
ALTER TABLE [datahub].[EdFiODSClient] ADD CONSTRAINT [DF_datahub_EdFiODSClient_LastModifiedOn] DEFAULT (GETDATE()) FOR [LastModifiedOn]
CREATE UNIQUE INDEX [UX_datahub_EdFiODSClient_EdFiODSOdsKey] ON [datahub].[EdFiODSClient] ([EdFiODSId], [ODSKey])
GO