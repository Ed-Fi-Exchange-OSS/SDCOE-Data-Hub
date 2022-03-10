-- Offerings (systems and services)
-- NOTE: ItemCategoryType 1 - SDCOE Item, 2 - External SaaS Item
-- NOTE: ItemType 1 - Service, 2 - Product
CREATE TABLE [datahub].[Offering](
    [OfferingId] [int] IDENTITY(1,1) PRIMARY KEY,
    [ItemNo] [int] NOT NULL,
    [ItemCategoryType] [tinyint] NOT NULL,
    [ItemName] [nvarchar](80) NULL,
    [ItemDescription] [nvarchar](1024) NULL,
    [ItemType] [tinyint] NOT NULL,
    [AssociatedCost] [nvarchar](255) NULL,
    [ProductURL] [nvarchar](1024) NULL,
    [ContactName] [nvarchar](80) NULL,
    [ContactPhone] [nvarchar](40) NULL,
    [ContactEmail] [nvarchar](128) NULL
) ON [PRIMARY]
GO
CREATE UNIQUE INDEX [UX_datahub_Offering_ItemNo] ON [datahub].[Offering] ([ItemNo]);
GO