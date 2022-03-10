-- ED-FI RELATED DATA AND OPERATIONS
-- Note that SDCOE will be supporting s single district, multi-year strategy for ODSs
--      Thus, a district will most likely have just one ODS.
--      But, it is possible for a district to have 3 ODSs (a live 2.6 ODS, a live 3.4 ODS and 3.4 ODS with sample data)
--
-- EdFiRequests
-- USE: District requests for Ed-Fi setup or change
-- Requests might be a fixed drop down list: 
--      "Set up a 3.4 Ed-Fi ODS with sample data for my district"
--      "Set up a new 2.6 Ed-Fi ODS for my district (to connect our SIS to)"
--      "Set up a new 3.4 Ed-Fi ODS for my district (to connect our SIS to)"
--      "Remove my 2.6 Ed-Fi ODS"
--      "Remove my 3.4 Ed-Fi ODS"
--      "Remove my 3.4 Ed-Fi ODS with sample data"
-- For the last two options, maybe these options wouldn't appear if there is no matching ODS for the district
--
-- RequestStatus    (1-Request, 2-InProcess, 3-Completed, 4-CompletedHide)
-- Operational note: There will likely be no automated create/delete Ed-Fi operations in this prototype, and 
--      thus, RequestStatus may need to be updated manually behind the scenes
-- Interface note: It would be ideal to 1) display a list of the Requests and 2) the form to make new requests
CREATE TABLE [datahub].[EdFiRequest](
    [EdFiRequestId] [int] IDENTITY(1,1) PRIMARY KEY,
    [OrganizationId] [int] NOT NULL,
    [Description] [nvarchar](255) NULL,
    [RequestDate] [date] NULL,
    [RequestStatus] [tinyint] NOT NULL,
    [IsArchived] [bit] NOT NULL,
    [CreatedOn] [datetime] NULL,
    [CreatedBy] [int] NULL,
    [LastModifiedOn] [datetime] NULL,
    [LastModifiedBy] [int] NULL,
    CONSTRAINT FK_EdFiRequestOrganizationID FOREIGN KEY ([OrganizationId]) REFERENCES datahub.Organization([OrganizationId]),
    CONSTRAINT FK_EdFiRequestCreatedBy FOREIGN KEY ([CreatedBy]) REFERENCES datahub.[User]([UserId]),
    CONSTRAINT FK_EdFiRequestLastModifiedBy FOREIGN KEY ([LastModifiedBy]) REFERENCES datahub.[User]([UserId])
) ON [PRIMARY] 
GO
ALTER TABLE [datahub].[EdFiRequest] ADD CONSTRAINT [DF_datahub_EdFiRequest_CreatedOn] DEFAULT (GETDATE()) FOR [CreatedOn]
ALTER TABLE [datahub].[EdFiRequest] ADD CONSTRAINT [DF_datahub_EdFiRequest_LastModifiedOn] DEFAULT (GETDATE()) FOR [LastModifiedOn]
ALTER TABLE [datahub].[EdFiRequest] ADD CONSTRAINT [DF_datahub_EdFiRequest_IsArchived] DEFAULT 0 FOR [IsArchived]
GO