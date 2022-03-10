-- Active District Participation in Service Offerings and SaaS
-- Participation
-- USE: This table holds which services and SaaS products each district is actively using
CREATE TABLE [datahub].[Participation](
    [OrganizationId] [int] NOT NULL,
    [OfferingId] [int] NOT NULL,
    [AsOfDate] [date] NOT NULL,
    CONSTRAINT [PK_Participation] PRIMARY KEY CLUSTERED (
        [OrganizationId] ASC,
        [OfferingId] ASC
    ),
    CONSTRAINT FK_ParticipationOrganizationId FOREIGN KEY ([OrganizationId]) REFERENCES datahub.Organization([OrganizationId]),
    CONSTRAINT FK_ParticipationOfferingId FOREIGN KEY ([OfferingId]) REFERENCES datahub.Offering([OfferingId])
) ON [PRIMARY] 
GO