INSERT INTO [datahub].[Organization] ([LocalOrganizationID],[OrganizationName],[FederalOrganizationID],[EducationOrganizationID],[SIS],[DominantDataSystem],[AnalyticsSystem],[InterimAssessments]) VALUES ('37679670000000','District 1 Union Elementary','0602100',3767967,'Synergy','Windows - SQL Server','Power BI','SBAC');
INSERT INTO [datahub].[Organization] ([LocalOrganizationID],[OrganizationName],[FederalOrganizationID],[EducationOrganizationID],[SIS],[DominantDataSystem],[AnalyticsSystem],[InterimAssessments]) VALUES ('37768510000000','District 2 Unified','0601426',3776851,'Synergy','Windows - SQL Server','Power BI','iReady');
INSERT INTO [datahub].[Organization] ([LocalOrganizationID],[OrganizationName],[FederalOrganizationID],[EducationOrganizationID],[SIS],[DominantDataSystem],[AnalyticsSystem],[InterimAssessments]) VALUES ('37679830000000','District 3 Unified','0605700',3767983,'Synergy','Windows - SQL Server','Power BI','NWEA');
INSERT INTO [datahub].[Organization] ([LocalOrganizationID],[OrganizationName],[FederalOrganizationID],[EducationOrganizationID],[SIS],[DominantDataSystem],[AnalyticsSystem],[InterimAssessments]) VALUES ('37679910000000','District 4 Union','0606810',3767991,'Synergy','Windows - SQL Server','Forecast5','SBAC');
INSERT INTO [datahub].[Organization] ([LocalOrganizationID],[OrganizationName],[FederalOrganizationID],[EducationOrganizationID],[SIS],[DominantDataSystem],[AnalyticsSystem],[InterimAssessments]) VALUES ('37680070000000','District 6 Elementary','0607470',3768007,'Synergy','Windows - SQL Server','Forecast5','iReady');
INSERT INTO [datahub].[Organization] ([LocalOrganizationID],[OrganizationName],[FederalOrganizationID],[EducationOrganizationID],[SIS],[DominantDataSystem],[AnalyticsSystem],[InterimAssessments]) VALUES ('37735510000000','District 7 Unified','0607500',3773551,'Synergy','Windows - SQL Server','Forecast5','NWEA');
INSERT INTO [datahub].[Organization] ([LocalOrganizationID],[OrganizationName],[FederalOrganizationID],[EducationOrganizationID],[SIS],[DominantDataSystem],[AnalyticsSystem],[InterimAssessments]) VALUES ('37680230000000','District 9 Elementary','0608610',3768023,'Synergy','Windows - SQL Server','Core Districts','SBAC');
INSERT INTO [datahub].[Organization] ([LocalOrganizationID],[OrganizationName],[FederalOrganizationID],[EducationOrganizationID],[SIS],[DominantDataSystem],[AnalyticsSystem],[InterimAssessments]) VALUES ('37680310000000','District 10 Unified','0609870',3768031,'Synergy','Windows - SQL Server','Core Districts','iReady');
INSERT INTO [datahub].[Organization] ([LocalOrganizationID],[OrganizationName],[FederalOrganizationID],[EducationOrganizationID],[SIS],[DominantDataSystem],[AnalyticsSystem],[InterimAssessments]) VALUES ('37680490000000','District 11 Elementary','0610710',3768049,'Synergy','Windows - SQL Server','Core Districts','NWEA');
INSERT INTO [datahub].[Organization] ([LocalOrganizationID],[OrganizationName],[FederalOrganizationID],[EducationOrganizationID],[SIS],[DominantDataSystem],[AnalyticsSystem],[InterimAssessments]) VALUES ('37680560000000','District 12 Union Elementary','0610740',3768056,'Synergy','Windows - Oracle DB','Power BI','SBAC');
INSERT INTO [datahub].[Organization] ([LocalOrganizationID],[OrganizationName],[FederalOrganizationID],[EducationOrganizationID],[SIS],[DominantDataSystem],[AnalyticsSystem],[InterimAssessments]) VALUES ('37680800000000','District 13 Elementary','0612750',3768080,'Synergy','Windows - Oracle DB','Power BI','iReady');
INSERT INTO [datahub].[Organization] ([LocalOrganizationID],[OrganizationName],[FederalOrganizationID],[EducationOrganizationID],[SIS],[DominantDataSystem],[AnalyticsSystem],[InterimAssessments]) VALUES ('37680980000000','District 14 Union','0612880',3768098,'Synergy','Windows - Oracle DB','Power BI','NWEA');
INSERT INTO [datahub].[Organization] ([LocalOrganizationID],[OrganizationName],[FederalOrganizationID],[EducationOrganizationID],[SIS],[DominantDataSystem],[AnalyticsSystem],[InterimAssessments]) VALUES ('37681060000000','District 15 Union High','0612910',3768106,'Synergy','Windows - Oracle DB','Forecast5','SBAC');
INSERT INTO [datahub].[Organization] ([LocalOrganizationID],[OrganizationName],[FederalOrganizationID],[EducationOrganizationID],[SIS],[DominantDataSystem],[AnalyticsSystem],[InterimAssessments]) VALUES ('37103710000000','County Office of Education','0691030',3710371,NULL,'Windows - SQL Server','Power BI',NULL);
INSERT INTO [datahub].[Organization] ([LocalOrganizationID],[OrganizationName],[OrganizationAbbreviation],[FederalOrganizationID],[EducationOrganizationID],[SIS],[DominantDataSystem],[AnalyticsSystem],[InterimAssessments]) VALUES ('37754160000000','District 16 Unified','SuperiorUSD','0600042',3775416,'PowerSchool','Windows - SQL Server','Power BI','NWEA');
GO

DECLARE @OrganizationId int = (select [OrganizationId] from [datahub].[Organization] where [LocalOrganizationID]='37103710000000')
DECLARE @OrganizationId_2 int = (select [OrganizationId] from [datahub].[Organization] where [LocalOrganizationID]='37754160000000')
DECLARE @OrganizationId_3 int = (select [OrganizationId] from [datahub].[Organization] where [LocalOrganizationID]='37679670000000')

INSERT INTO [datahub].[User] ([Status],[OrganizationId],[FirstName],[LastName],[EmailAddress],[Role])
VALUES
    -- External accounts invited into AAD tenant
    (1,@OrganizationId,'J','Test','jtest#EXT#@[ACCOUNT].onmicrosoft.com',1)
    ,(1,@OrganizationId,'M','Test','mtes#EXT#@[ACCOUNT].onmicrosoft.com',1)

    -- Test accounts directly in AAD tenant
    ,(1,@OrganizationId,'Test','SuperUser 1','District_1_SuperUser@[ACCOUNT].onmicrosoft.com',1)
    ,(1,@OrganizationId,'Test','SuperUser 2','District_2_SuperUser@[ACCOUNT].onmicrosoft.com',1)
    ,(1,@OrganizationId_2,'Test','District 1 User','District_1_User@[ACCOUNT].onmicrosoft.com',2)
    ,(1,@OrganizationId_2,'Test','District 1 Viewer','District_1_Viewer@[ACCOUNT].onmicrosoft.com',3)
    ,(1,@OrganizationId_3,'Test','District 2 User','District_2_User@[ACCOUNT].onmicrosoft.com',2)
    ,(1,@OrganizationId_3,'Test','District 2 Viewer','District_2_Viewer@[ACCOUNT].onmicrosoft.com',3)
GO 

DECLARE @OrganizationId int = (select [OrganizationId] from [datahub].[Organization] where [LocalOrganizationID]='37754160000000')
DECLARE @OrganizationId_2 int = (select [OrganizationId] from [datahub].[Organization] where [LocalOrganizationID]='37679670000000')

DECLARE @currentDate DATE = CAST(GETUTCDATE() as DATE)
DECLARE @futureDate DATE = DATEADD(day, 30, @currentDate)
DECLARE @pastDate DATE = DATEADD(day, -30, @currentDate)

INSERT INTO [datahub].[Announcement]([OrganizationId],[Message],[DisplayUntilDate],[Status])
VALUES (@OrganizationId,'This is a test message for District 1<br> No need to respond',@futureDate,1)
INSERT INTO [datahub].[Announcement]([OrganizationId],[Message],[DisplayUntilDate],[Status])
VALUES (@OrganizationId,'Please check your Ed-Fi settings',@pastDate,1)
INSERT INTO [datahub].[Announcement]([OrganizationId],[Message],[DisplayUntilDate],[Status])
VALUES (@OrganizationId_2,'District 2 : This is a test message<br> No need to respond',@futureDate,1)
INSERT INTO [datahub].[Announcement]([OrganizationId],[Message],[DisplayUntilDate],[Status])
VALUES (@OrganizationId_2,'District 2 : This is a second test message<br> No need to respond',@futureDate,1)
GO

INSERT INTO [datahub].[Offering] ([ItemNo],[ItemCategoryType],[ItemName],[ItemDescription],[ItemType],[AssociatedCost],[ProductURL],[ContactName],[ContactPhone],[ContactEmail])
VALUES
    (1,1,'District Financial Services','','1','See pricing sheet','https://www.[YOUR ORGANIZATION WEB SITE]','Delia Obrien','999-222-3333','DeliaObrien@example.com')
   ,(2,1,'Business Advisory Services','','1','See pricing sheet','https://www.[YOUR ORGANIZATION WEB SITE]','Delia Obrien','999-222-3333','DeliaObrien@example.com')
   ,(3,1,'Charter School Services','','1','See pricing sheet','https://www.[YOUR ORGANIZATION WEB SITE]','Delia Obrien','999-222-3333','DeliaObrien@example.com')
   ,(4,1,'District Financial Services','','1','See pricing sheet','https://www.[YOUR ORGANIZATION WEB SITE]','Delia Obrien','999-222-3333','DeliaObrien@example.com')
   ,(5,1,'Maintenance and Operations','','1','See pricing sheet','https://www.[YOUR ORGANIZATION WEB SITE]','Delia Obrien','999-222-3333','DeliaObrien@example.com')
   ,(6,1,'Legal Services','','1','See pricing sheet','https://www.[YOUR ORGANIZATION WEB SITE]','Delia Obrien','999-222-3333','DeliaObrien@example.com')
   ,(7,1,'Payroll Services','Payroll Services provides support and assistance to schools','1','See pricing sheet','https://www.[YOUR ORGANIZATION WEB SITE]','Terrell Simpson','999-222-3333','TerrellSimpson@example.com')
   ,(25,1,'SIS','Our hosted SIS solution liberates you from the expense of infrastructure, maintenance and technical support freeing you to focus on student&#39;s needs and instruction. ','1','See pricing sheet','https://www.[YOUR ORGANIZATION WEB SITE]','Lucia Larson','999-222-3333','LuciaLarson@example.com')
   ,(47,1,'District and School Improvement','','1','See pricing sheet','https://www.[YOUR ORGANIZATION WEB SITE]','Jan Barnes','999-222-3333','JanBarnes@example.com')
GO

INSERT INTO [datahub].[Offering] ([ItemNo],[ItemCategoryType],[ItemName],[ItemDescription],[ItemType],[ProductURL],[ContactName],[ContactPhone],[ContactEmail]) 
VALUES
    (1002,2,'Forecaster','Forecaster consolidates disparate data sources into a single analytics environment, giving you in-depth, actionable insights to navigate our evolving education landscape and improve student achievement outcomes.',2,'https://www.[YOUR ORGANIZATION WEB SITE]','Samuel Obrien','999-222-3333','SamuelObrien@example.com')
   ,(1003,2,'PowerBI Dashboard','This dashboard displays lists of business and it areas.',2,'https://www.[YOUR ORGANIZATION WEB SITE]','Samuel Lopes','999-222-3333','SamuelLopes@example.com')
   ,(1004,2,'Universal Transcript Service','For active users of Ed-Fi, this application provides a method of pulling student transcript records in multiple formats, from multiple Ed-Fi ODSs.',2,'https://www.[YOUR ORGANIZATION WEB SITE]','Samuel Lopes','999-222-3333','SamuelLopes@example.com')
GO

DECLARE @OrganizationId int = (select [OrganizationId] from [datahub].[Organization] where [LocalOrganizationID]='37754160000000')
DECLARE @OrganizationId_2 int = (select [OrganizationId] from [datahub].[Organization] where [LocalOrganizationID]='37679670000000')

DECLARE @OfferingIdItemNo1 int = (select [OfferingId] from [datahub].[Offering] where [ItemNo] = 1)
DECLARE @OfferingIdItemNo7 int = (select [OfferingId] from [datahub].[Offering] where [ItemNo] = 7)
DECLARE @OfferingIdItemNo25 int = (select [OfferingId] from [datahub].[Offering] where [ItemNo] = 25)
DECLARE @OfferingIdItemNo47 int = (select [OfferingId] from [datahub].[Offering] where [ItemNo] = 47)
DECLARE @OfferingIdItemNo1001 int = (select [OfferingId] from [datahub].[Offering] where [ItemNo] = 1001)
DECLARE @OfferingIdItemNo1003 int = (select [OfferingId] from [datahub].[Offering] where [ItemNo] = 1003)
DECLARE @OfferingIdItemNo1004 int = (select [OfferingId] from [datahub].[Offering] where [ItemNo] = 1004)

INSERT INTO [datahub].[Participation] ([OrganizationId],[OfferingId],[AsOfDate])
VALUES
    (@OrganizationId, @OfferingIdItemNo7, '11-30-2020')
   ,(@OrganizationId, @OfferingIdItemNo25, '11-30-2020')
   ,(@OrganizationId, @OfferingIdItemNo47, '11-30-2020')
   ,(@OrganizationId_2, @OfferingIdItemNo7, '11-30-2020')
   ,(@OrganizationId, @OfferingIdItemNo1, '11-30-2020')
   ,(@OrganizationId, @OfferingIdItemNo1001, '11-30-2020')
   ,(@OrganizationId, @OfferingIdItemNo1003, '11-30-2020')
   ,(@OrganizationId, @OfferingIdItemNo1004, '11-30-2020')
GO

DECLARE @OrganizationId int = (select [OrganizationId] from [datahub].[Organization] where [LocalOrganizationID]='37754160000000')

INSERT INTO [datahub].[Support] ([OrganizationId], [SystemID], [TicketID], [Description], [Status])
VALUES (@OrganizationId,'PeopleSoft','PS1939829', 'Issue with February HR report run', 0);
INSERT INTO [datahub].[Support] ([OrganizationId], [SystemID], [TicketID], [Description], [Status])
VALUES (@OrganizationId,'ServiceNow','INC0049643', 'Server E104 requires patch', 1);
INSERT INTO [datahub].[Support] ([OrganizationId], [SystemID], [TicketID], [Description], [Status])
VALUES (@OrganizationId,'ServiceNow','INC0049613', 'Server E101 requires patch', 2);
GO

DECLARE @OrganizationId int = (select [OrganizationId] from [datahub].[Organization] where [LocalOrganizationID]='37754160000000')
DECLARE @OrganizationId_2 int = (select [OrganizationId] from [datahub].[Organization] where [LocalOrganizationID]='37679670000000')

INSERT INTO [datahub].[CRMContact] ([OrganizationId], [ContactName], [ContactTitle], [ContactEmail], [ContactPhone])
VALUES (@OrganizationId,'Priscilla Colon','Superintendent','PriscillaColon@example.com','949-999-9999');
INSERT INTO [datahub].[CRMContact] ([OrganizationId], [ContactName], [ContactTitle], [ContactEmail], [ContactPhone])
VALUES (@OrganizationId,'Blake Peterson','Asst. Superintendent, Academic Services','BlakePeterson@example.com','949-999-9999');
INSERT INTO [datahub].[CRMContact] ([OrganizationId], [ContactName], [ContactTitle], [ContactEmail], [ContactPhone])
VALUES (@OrganizationId,'Ollie Salazar','Data specialist','OllieSalazar@example.com','949-999-9999');
INSERT INTO [datahub].[CRMContact] ([OrganizationId], [ContactName], [ContactTitle], [ContactEmail], [ContactPhone])
VALUES (@OrganizationId_2,'Tami Barton','Superintendent','TamiBarton@example.com','949-999-9999');
INSERT INTO [datahub].[CRMContact] ([OrganizationId], [ContactName], [ContactTitle], [ContactEmail], [ContactPhone])
VALUES (@OrganizationId_2,'Lucille Lamb','Database Manager','LucilleLamb@example.com','949-999-9999');
INSERT INTO [datahub].[CRMContact] ([OrganizationId], [ContactName], [ContactTitle], [ContactEmail], [ContactPhone])
VALUES (@OrganizationId_2,'Antonio Blair','Assessment Coordinator','AntonioBlair@example.com','949-999-9999');
GO

DECLARE @OrganizationId int = (select [OrganizationId] from [datahub].[Organization] where [LocalOrganizationID]='37754160000000')

INSERT INTO [datahub].[Extract] ([OrganizationId],[ExtractJobName],[ExtractFrequency],[ExtractLastStatus],[ExtractLastDate])
VALUES (@OrganizationId,'OneRoster','Nightly','Lastrun: Success','1/30/2021');
INSERT INTO [datahub].[Extract] ([OrganizationId],[ExtractJobName],[ExtractFrequency],[ExtractLastStatus],[ExtractLastDate])
VALUES (@OrganizationId,'Clever Export','Weekly','Lastrun: Success','1/31/2021');
GO

DECLARE @OrganizationId int = (select [OrganizationId] from [datahub].[Organization] where [LocalOrganizationID]='37754160000000')

INSERT INTO [datahub].[EdFiRequest] ([OrganizationId], [Description], [RequestDate], [RequestStatus])
VALUES (@OrganizationId,'Set up a 3.4 Ed-Fi ODS with sample data for my district','1/30/2021',4);
INSERT INTO [datahub].[EdFiRequest] ([OrganizationId], [Description], [RequestDate], [RequestStatus])
VALUES (@OrganizationId,'Set up a new 2.6 Ed-Fi ODS for my district (to connect our SIS to)','2/15/2021',3);
INSERT INTO [datahub].[EdFiRequest] ([OrganizationId], [Description], [RequestDate], [RequestStatus])
VALUES (@OrganizationId,'Set up a new 3.4 Ed-Fi ODS for my district (to connect our SIS to)','2/18/2021',2);
INSERT INTO [datahub].[EdFiRequest] ([OrganizationId], [Description], [RequestDate], [RequestStatus])
VALUES (@OrganizationId,'Remove my 2.6 Ed-Fi ODS','2/25/2021',1);
GO

DECLARE @OrganizationId int = (select [OrganizationId] from [datahub].[Organization] where [LocalOrganizationID]='37754160000000')

INSERT INTO [datahub].[EdFiODS] 
([EdFiODSNo],[OrganizationId], [ODSName], [ODSPath], [ODSURL], [ODSVersion], [ODSKey], [ODSSecret])
VALUES 
    (1, @OrganizationId,'district_edfi_3.4_live', NULL, 'https://edfi3.[COLLABORATIVE DOMAIN]','3.4','testkey','testsecret')
    ,(2, @OrganizationId,'edfi_3.4_demo', '/v3.4.0', 'https://api.ed-fi.org:443','3.4','RvcohKz9zHI4','E1iEFusaNf81xzCxwHfbolkC')
    ,(3, @OrganizationId,'edfi_5.1_demo', '/v5.1.0', 'https://api.ed-fi.org:443','5.1','RvcohKz9zHI4','E1iEFusaNf81xzCxwHfbolkC')
GO

DECLARE @EdFiOdsId int = (select [EdFiODSId] from [datahub].[EdFiODS] where [EdFiODSNo]=1)

INSERT INTO [datahub].[EdFiODSClient] 
([EdFiODSId],[VendorName],[ApplicationName],[ClaimSetName],[ClientName],[ODSKey],[ODSSecret])
VALUES
    (@EdFiOdsId, 'Test Vendor 1', 'Vendor 1 Application', 'SIS Vendor', 'SIS Vendor Client', 'ZWPRTOYHg3021mfmUpJl', 'ycf1PUygYqka0CFRyc9m')
   ,(@EdFiOdsId, 'Test Vendor 1', 'Vendor 1 Additional Application', 'AdditionalCustomClaimset', 'SIS Vendor Client 2', 'HvWF3dgZuQYLI6xee5UO', 'P91ljAgYQZ0a9IBYhPQK')
   ,(@EdFiOdsId, 'District Test', 'District Access', 'ReadOnlyClaimset', 'District Client', 'rnjiSfLeIVTszIceLBdc', 'VxjwSzNXBrMPTVCoeeUG')
GO

INSERT INTO [datahub].[ODSStatusJob] ([ODSStatusJobNo],[StatusJobName],[ODSversion],[ODSCheckType],[EntityTableToQuery],[FilterConditions])
VALUES (1,'General ODS wellness check (access)','3.4',1,'','');
INSERT INTO [datahub].[ODSStatusJob] ([ODSStatusJobNo],[StatusJobName],[ODSversion],[ODSCheckType],[EntityTableToQuery],[FilterConditions])
VALUES (2,'Row count - EducationOrganization','3.4',2,'EducationOrganization','');
INSERT INTO [datahub].[ODSStatusJob] ([ODSStatusJobNo],[StatusJobName],[ODSversion],[ODSCheckType],[EntityTableToQuery],[FilterConditions])
VALUES (3,'Row count - Student','3.4',2,'Student','');
INSERT INTO [datahub].[ODSStatusJob] ([ODSStatusJobNo],[StatusJobName],[ODSversion],[ODSCheckType],[EntityTableToQuery],[FilterConditions])
VALUES (4,'Row count - School','3.4',2,'School','');
GO

DECLARE @EdFiOdsId int = (select [EdFiOdsId] from [datahub].[EdFiODS] where [EdFiODSNo]=1)
DECLARE @ODSStatusJob1 int = (select [ODSStatusJobId] from [datahub].[ODSStatusJob] where [ODSStatusJobNo]=1)
DECLARE @ODSStatusJob2 int = (select [ODSStatusJobId] from [datahub].[ODSStatusJob] where [ODSStatusJobNo]=2)
DECLARE @ODSStatusJob3 int = (select [ODSStatusJobId] from [datahub].[ODSStatusJob] where [ODSStatusJobNo]=3)

INSERT INTO [datahub].[ODSStatus] ([EdFiODSId], [ODSStatusJobId], [StatusReadout], [RecordCount])
VALUES (@EdFiOdsId, @ODSStatusJob1, 'ODS Up and running',null);
INSERT INTO [datahub].[ODSStatus] ([EdFiODSId], [ODSStatusJobId], [StatusReadout], [RecordCount])
VALUES (@EdFiOdsId, @ODSStatusJob2, 'Row count - EducationOrganization',26);
INSERT INTO [datahub].[ODSStatus] ([EdFiODSId], [ODSStatusJobId], [StatusReadout], [RecordCount])
VALUES (@EdFiOdsId, @ODSStatusJob3, 'Row count - Student',12345);
GO
