-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/12/2017 10:46:54
-- Generated from EDMX file: C:\Users\student\Desktop\PersonalFinancesMiscellaneus\PersonalFinances2017\PersonalFinances2017\Models\PersonalFinances.edmx
-- --------------------------------------------------

begin transaction

SET QUOTED_IDENTIFIER OFF;
--GO
--USE [PersonalFinancesDB];
--GO
--IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');

--GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

--IF OBJECT_ID(N'[dbo].[FK_asset_dossier]', 'F') IS NOT NULL
--    ALTER TABLE [dbo].[asset] DROP CONSTRAINT [FK_asset_dossier];
--GO
--IF OBJECT_ID(N'[dbo].[FK_dossier_user]', 'F') IS NOT NULL
--    ALTER TABLE [dbo].[dossier] DROP CONSTRAINT [FK_dossier_user];
--GO
--IF OBJECT_ID(N'[dbo].[FK_record_dossier]', 'F') IS NOT NULL
--    ALTER TABLE [dbo].[record] DROP CONSTRAINT [FK_record_dossier];
--GO
--IF OBJECT_ID(N'[dbo].[FK_record_recordSubcategory]', 'F') IS NOT NULL
--    ALTER TABLE [dbo].[record] DROP CONSTRAINT [FK_record_recordSubcategory];
--GO
--IF OBJECT_ID(N'[dbo].[FK_recordCategory_dossier]', 'F') IS NOT NULL
--    ALTER TABLE [dbo].[recordCategory] DROP CONSTRAINT [FK_recordCategory_dossier];
--GO
--IF OBJECT_ID(N'[dbo].[FK_recordSubcategory_recordCategory]', 'F') IS NOT NULL
--    ALTER TABLE [dbo].[recordSubcategory] DROP CONSTRAINT [FK_recordSubcategory_recordCategory];
--GO
--IF OBJECT_ID(N'[dbo].[FK_importRecord_dossier]', 'F') IS NOT NULL
--    ALTER TABLE [dbo].[importRecordTmp] DROP CONSTRAINT [FK_importRecord_dossier];
--GO
--IF OBJECT_ID(N'[dbo].[FK_liability_dossier]', 'F') IS NOT NULL
--    ALTER TABLE [dbo].[liability] DROP CONSTRAINT [FK_liability_dossier];
--GO
--IF OBJECT_ID(N'[dbo].[FK_recurrentRecord_dossier]', 'F') IS NOT NULL
--    ALTER TABLE [dbo].[recurrentRecord] DROP CONSTRAINT [FK_recurrentRecord_dossier];
--GO
--IF OBJECT_ID(N'[dbo].[FK_recurrentRecord_recordSubcategory]', 'F') IS NOT NULL
--    ALTER TABLE [dbo].[recurrentRecord] DROP CONSTRAINT [FK_recurrentRecord_recordSubcategory];
--GO
--IF OBJECT_ID(N'[dbo].[FK_recurrentRevenue_dossier]', 'F') IS NOT NULL
--    ALTER TABLE [dbo].[recurrentRevenue] DROP CONSTRAINT [FK_recurrentRevenue_dossier];
--GO
--IF OBJECT_ID(N'[dbo].[FK_recurrentRevenue_revenueSubcategory]', 'F') IS NOT NULL
--    ALTER TABLE [dbo].[recurrentRevenue] DROP CONSTRAINT [FK_recurrentRevenue_revenueSubcategory];
--GO
--IF OBJECT_ID(N'[dbo].[FK_revenue_dossier]', 'F') IS NOT NULL
--    ALTER TABLE [dbo].[revenue] DROP CONSTRAINT [FK_revenue_dossier];
--GO
--IF OBJECT_ID(N'[dbo].[FK_revenue_revenueSubcategory]', 'F') IS NOT NULL
--    ALTER TABLE [dbo].[revenue] DROP CONSTRAINT [FK_revenue_revenueSubcategory];
--GO
--IF OBJECT_ID(N'[dbo].[FK_revenueCategory_dossier]', 'F') IS NOT NULL
--    ALTER TABLE [dbo].[revenueCategory] DROP CONSTRAINT [FK_revenueCategory_dossier];
--GO
--IF OBJECT_ID(N'[dbo].[FK_revenueSubcategory_recordCategory]', 'F') IS NOT NULL
--    ALTER TABLE [dbo].[revenueSubcategory] DROP CONSTRAINT [FK_revenueSubcategory_recordCategory];
--GO
--IF OBJECT_ID(N'[dbo].[fk_RoleId]', 'F') IS NOT NULL
--    ALTER TABLE [dbo].[webpages_UsersInRoles] DROP CONSTRAINT [fk_RoleId];
--GO
--IF OBJECT_ID(N'[dbo].[FK_user_userProfile]', 'F') IS NOT NULL
--    ALTER TABLE [dbo].[user] DROP CONSTRAINT [FK_user_userProfile];
--GO
--IF OBJECT_ID(N'[dbo].[fk_UserId]', 'F') IS NOT NULL
--    ALTER TABLE [dbo].[webpages_UsersInRoles] DROP CONSTRAINT [fk_UserId];
--GO

---- --------------------------------------------------
---- Dropping existing tables
---- --------------------------------------------------

--IF OBJECT_ID(N'[dbo].[asset]', 'U') IS NOT NULL
--    DROP TABLE [dbo].[asset];
--GO
--IF OBJECT_ID(N'[dbo].[dossier]', 'U') IS NOT NULL
--    DROP TABLE [dbo].[dossier];
--GO
--IF OBJECT_ID(N'[dbo].[record]', 'U') IS NOT NULL
--    DROP TABLE [dbo].[record];
--GO
--IF OBJECT_ID(N'[dbo].[recordCategory]', 'U') IS NOT NULL
--    DROP TABLE [dbo].[recordCategory];
--GO
--IF OBJECT_ID(N'[dbo].[recordSubcategory]', 'U') IS NOT NULL
--    DROP TABLE [dbo].[recordSubcategory];
--GO
--IF OBJECT_ID(N'[dbo].[importRecordTmp]', 'U') IS NOT NULL
--    DROP TABLE [dbo].[importRecordTmp];
--GO
--IF OBJECT_ID(N'[dbo].[liability]', 'U') IS NOT NULL
--    DROP TABLE [dbo].[liability];
--GO
--IF OBJECT_ID(N'[dbo].[recurrentRecord]', 'U') IS NOT NULL
--    DROP TABLE [dbo].[recurrentRecord];
--GO
--IF OBJECT_ID(N'[dbo].[recurrentRevenue]', 'U') IS NOT NULL
--    DROP TABLE [dbo].[recurrentRevenue];
--GO
--IF OBJECT_ID(N'[dbo].[revenue]', 'U') IS NOT NULL
--    DROP TABLE [dbo].[revenue];
--GO
--IF OBJECT_ID(N'[dbo].[revenueCategory]', 'U') IS NOT NULL
--    DROP TABLE [dbo].[revenueCategory];
--GO
--IF OBJECT_ID(N'[dbo].[revenueSubcategory]', 'U') IS NOT NULL
--    DROP TABLE [dbo].[revenueSubcategory];
--GO
--IF OBJECT_ID(N'[dbo].[user]', 'U') IS NOT NULL
--    DROP TABLE [dbo].[user];
--GO
--IF OBJECT_ID(N'[dbo].[UserProfile]', 'U') IS NOT NULL
--    DROP TABLE [dbo].[UserProfile];
--GO
--IF OBJECT_ID(N'[dbo].[webpages_Membership]', 'U') IS NOT NULL
--    DROP TABLE [dbo].[webpages_Membership];
--GO
--IF OBJECT_ID(N'[dbo].[webpages_OAuthMembership]', 'U') IS NOT NULL
--    DROP TABLE [dbo].[webpages_OAuthMembership];
--GO
--IF OBJECT_ID(N'[dbo].[webpages_Roles]', 'U') IS NOT NULL
--    DROP TABLE [dbo].[webpages_Roles];
--GO
--IF OBJECT_ID(N'[dbo].[webpages_UsersInRoles]', 'U') IS NOT NULL
--    DROP TABLE [dbo].[webpages_UsersInRoles];

GO
CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                   NVARCHAR (128) NOT NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (256) NOT NULL,
    [Name]                 NVARCHAR (256) NULL,
    [Surname]              NVARCHAR (256) NULL,
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[AspNetUsers]([UserName] ASC);
	
	
CREATE TABLE [dbo].[AspNetRoles] (
    [Id]   NVARCHAR (128) NOT NULL,
    [Name] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[AspNetRoles]([Name] ASC);
	
	
	CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [ProviderKey]   NVARCHAR (128) NOT NULL,
    [UserId]        NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserLogins]([UserId] ASC);


CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] NVARCHAR (128) NOT NULL,
    [RoleId] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserRoles]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_RoleId]
    ON [dbo].[AspNetUserRoles]([RoleId] ASC);
	

-- Creating table 'dossier'
GO
CREATE TABLE [dbo].[dossier] (
    [dossierId]    int IDENTITY(1,1) NOT NULL,
    [dossierName]  nvarchar(250)  NOT NULL,
    [creationDate] datetime  NULL,
	[userId]       NVARCHAR (128) NOT NULL,
	CONSTRAINT [FK_dossier_AspNetUsers] FOREIGN KEY ([userId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [PK_dossier] PRIMARY KEY CLUSTERED ([dossierId] ASC)

);
	
GO
-- Creating table 'asset'
CREATE TABLE [dbo].[asset] (
    [assetId] int IDENTITY(1,1) NOT NULL,
    [dossierId] int  NOT NULL,
    [amount] decimal(19,4)  NOT NULL,
    [description] nvarchar(200)  NULL,
	CONSTRAINT [PK_asset] PRIMARY KEY CLUSTERED ([assetId] ASC),
);
GO



-- Creating table 'record'
-- Creating table 'records'
CREATE TABLE [dbo].[record] (
    [recordId] int IDENTITY(1,1) NOT NULL,
    [dossierId] int  NOT NULL,
    [recordSubcategoryId] int  NULL,
    [date] datetime  NOT NULL,
    [revenue] decimal(19,4)  NULL,
    [expense] decimal(19,4)  NULL,
    [description] nvarchar(200)  NOT NULL,
    [comment] nvarchar(300)  NULL,
	CONSTRAINT [PK_record] PRIMARY KEY CLUSTERED ([recordId] ASC),

);
GO


-- Creating table 'recordCategories'
CREATE TABLE [dbo].[recordCategory] (
    [recordCategoryId] int IDENTITY(1,1) NOT NULL,
    [dossierId] int  NOT NULL,
	[isExpense] bit  NOT NULL,
    [description] nvarchar(200)  NULL,
	CONSTRAINT [PK_recordCategory] PRIMARY KEY CLUSTERED ([recordCategoryId] ASC)

);
GO

-- Creating table 'recordSubcategory'
CREATE TABLE [dbo].[recordSubcategory] (
    [recordSubcategoryId] int IDENTITY(1,1) NOT NULL,
    [recordCategoryId] int  NOT NULL,
    [description] nvarchar(200)  NULL,
	CONSTRAINT [PK_recordSubcategory] PRIMARY KEY CLUSTERED ([recordSubcategoryId] ASC)

);

GO
-- Creating table 'importRecordTmp'
CREATE TABLE [dbo].[importRecordTmp] (
    [importRecordTmpId] int IDENTITY(1,1) NOT NULL,
    [dossierId] int  NOT NULL,
    [date] datetime  NOT NULL,
    [revenue] decimal(19,4)  NULL,
    [expense] decimal(19,4)  NULL,
    [numOfDuplicates] int  NULL,
    [category] nvarchar(100)  NOT NULL,
    [subcategory] nvarchar(100)  NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [comment] nvarchar(max)  NULL,
	CONSTRAINT [PK_importRecordTmp] PRIMARY KEY CLUSTERED ([importRecordTmpId] ASC)
	
);

GO
-- Creating table 'liability'
CREATE TABLE [dbo].[liability] (
    [liabilityId] int IDENTITY(1,1) NOT NULL,
    [dossierId] int  NOT NULL,
    [value] decimal(19,4)  NOT NULL,
    [description] nvarchar(200)  NULL,
	CONSTRAINT [PK_liability] PRIMARY KEY CLUSTERED ([liabilityId] ASC)
);

GO
-- Creating table 'recurrentRecord'
CREATE TABLE [dbo].[recurrentRecord] (
    [recurrentRecordId] int IDENTITY(1,1) NOT NULL,
    [dossierId] int  NOT NULL,
    [recordSubcategoryId] int  NULL,
    [beginDate] datetime  NOT NULL,
    [endDate] datetime  NOT NULL,
    [peridiocity] tinyint  NOT NULL,
    [dayPeriod] tinyint  NOT NULL,
    [value] decimal(19,4)  NULL
);

GO
-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [dossierId] in table 'asset'
ALTER TABLE [dbo].[asset]
ADD CONSTRAINT [FK_asset_dossier]
    FOREIGN KEY ([dossierId])
    REFERENCES [dbo].[dossier]
        ([dossierId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_asset_dossier'
CREATE INDEX [IX_FK_asset_dossier]
ON [dbo].[asset]
    ([dossierId]);
GO



-- Creating foreign key on [dossierId] in table 'record'
ALTER TABLE [dbo].[record]
ADD CONSTRAINT [FK_record_dossier]
    FOREIGN KEY ([dossierId])
    REFERENCES [dbo].[dossier]
        ([dossierId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_record_dossier'
CREATE INDEX [IX_FK_record_dossier]
ON [dbo].[record]
    ([dossierId]);
GO

-- Creating foreign key on [dossierId] in table 'recordCategory'
ALTER TABLE [dbo].[recordCategory]
ADD CONSTRAINT [FK_recordCategory_dossier]
    FOREIGN KEY ([dossierId])
    REFERENCES [dbo].[dossier]
        ([dossierId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_recordCategory_dossier'
CREATE INDEX [IX_FK_recordCategory_dossier]
ON [dbo].[recordCategory]
    ([dossierId]);
GO

-- Creating foreign key on [dossierId] in table 'importRecordTmp'
ALTER TABLE [dbo].[importRecordTmp]
ADD CONSTRAINT [FK_importRecord_dossier]
    FOREIGN KEY ([dossierId])
    REFERENCES [dbo].[dossier]
        ([dossierId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_importRecord_dossier'
CREATE INDEX [IX_FK_importRecord_dossier]
ON [dbo].[importRecordTmp]
    ([dossierId]);
GO

-- Creating foreign key on [dossierId] in table 'liability'
ALTER TABLE [dbo].[liability]
ADD CONSTRAINT [FK_liability_dossier]
    FOREIGN KEY ([dossierId])
    REFERENCES [dbo].[dossier]
        ([dossierId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_liability_dossier'
CREATE INDEX [IX_FK_liability_dossier]
ON [dbo].[liability]
    ([dossierId]);
GO

-- Creating foreign key on [dossierId] in table 'recurrentRecord'
ALTER TABLE [dbo].[recurrentRecord]
ADD CONSTRAINT [FK_recurrentRecord_dossier]
    FOREIGN KEY ([dossierId])
    REFERENCES [dbo].[dossier]
        ([dossierId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_recurrentRecord_dossier'
CREATE INDEX [IX_FK_recurrentRecord_dossier]
ON [dbo].[recurrentRecord]
    ([dossierId]);
GO



-- Creating foreign key on [recordSubcategoryId] in table 'record'
ALTER TABLE [dbo].[record]
ADD CONSTRAINT [FK_record_recordSubcategory]
    FOREIGN KEY ([recordSubcategoryId])
    REFERENCES [dbo].[recordSubcategory]
        ([recordSubcategoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_record_recordSubcategory'
CREATE INDEX [IX_FK_record_recordSubcategory]
ON [dbo].[record]
    ([recordSubcategoryId]);
GO

-- Creating foreign key on [recordCategoryId] in table 'recordSubcategory'
ALTER TABLE [dbo].[recordSubcategory]
ADD CONSTRAINT [FK_recordSubcategory_recordCategory]
    FOREIGN KEY ([recordCategoryId])
    REFERENCES [dbo].[recordCategory]
        ([recordCategoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_recordSubcategory_recordCategory'
CREATE INDEX [IX_FK_recordSubcategory_recordCategory]
ON [dbo].[recordSubcategory]
    ([recordCategoryId]);
GO

-- Creating foreign key on [recordSubcategoryId] in table 'recurrentRecord'
ALTER TABLE [dbo].[recurrentRecord]
ADD CONSTRAINT [FK_recurrentRecord_recordSubcategory]
    FOREIGN KEY ([recordSubcategoryId])
    REFERENCES [dbo].[recordSubcategory]
        ([recordSubcategoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_recurrentRecord_recordSubcategory'
CREATE INDEX [IX_FK_recurrentRecord_recordSubcategory]
ON [dbo].[recurrentRecord]
    ([recordSubcategoryId]);




-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
GO
CREATE FUNCTION [dbo].[GetCategoryId]
    -- Add the parameters for the stored procedure here
    (@dossierId int,
	 @description nvarchar(200))


RETURNS INT

AS

BEGIN	
	DECLARE @recordCategoryId int = 0;

    select 
		@recordCategoryId=ec.recordCategoryId
	from 
		recordCategory eC
	where
		dossierId=@dossierId
		and [description]= @description;

	if @recordCategoryId=null or @recordCategoryId=0  
		set  @recordCategoryId=-1

	RETURN @recordCategoryId
	
END;


GO
CREATE FUNCTION [dbo].[GetSubcategoryId]
    -- Add the parameters for the stored procedure here
    (@dossierId int,
	 @descriptionCat nvarchar(200),
	 @descriptionSubcat nvarchar(200))


RETURNS INT

AS

BEGIN	
	DECLARE @recordSubcategoryId int = 0;

   select 
		@recordSubcategoryId=eSC.recordSubcategoryId 
	from 
		recordCategory eC
		join recordSubcategory eSC on eSC.recordCategoryId=ec.recordCategoryId 
	where
		ec.dossierId=@dossierId
		and ec.[description]= @descriptionCat
		and eSC.[description]=  @descriptionSubcat


	if @recordSubcategoryId=null or @recordSubcategoryId=0  
		set  @recordSubcategoryId=-1

	RETURN @recordSubcategoryId
	
END;

GO
CREATE PROCEDURE [dbo].[CreateCategoriesSubcategoriesFromImport]
    -- Add the parameters for the stored procedure here
    @dossierId int = null
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- INSERT NEW CATEGORIES
	WITH EXISTING_CATS AS
	(
		select distinct 
			[description] as category 
		from 
			[dbo].recordCategory 
		where 
			dossierId=@dossierId
	)  
	,NEWLY_CREATED_CATS AS
	(
		select distinct 
			category 
		from 
			[dbo].importRecordTmp 
		where 
			dossierId=@dossierId
	) 

	insert into 
		recordCategory (dossierId, [description])
			select 
				@dossierId,category 
			from  
				NEWLY_CREATED_CATS
		except
			select 
				@dossierId,category 
			from  
				EXISTING_CATS;


	--INSERT SUBCATEGORIES;
	WITH EXISTING_SUBCATS AS
	(
		select 
			exC.[description] as Category,
			exSC.[description] as Subcategory  
		from
			recordSubcategory exSC 
			join recordCategory exC on exSC.recordCategoryId=exSC.recordCategoryId
		where
			exC.dossierId=@dossierId
	)
	, NEWLY_CREATED_SUBCATS AS
	(
		select 
			category, subcategory 
		from 
			importRecordTmp impET
		where
			impET.dossierId=@dossierId
		group by
			category, subcategory 
	)

	insert into recordSubcategory  (recordCategoryId, [description])
	
		select 
				 dbo.GetCategoryId(@dossierId, NCS.category),
				 NCS.subcategory as [description] 
			from  
				NEWLY_CREATED_SUBCATS NCS
		except
			select 
				 dbo.GetCategoryId(@dossierId, ES.category),
				 ES.subcategory as [description]  
			from  
				EXISTING_SUBCATS ES;


END;

GO
CREATE PROCEDURE [dbo].[CreaterecordsFromImport]
    -- Add the parameters for the stored procedure here
    @dossierId int
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- INSERT NEW recordS
	insert into
		record (dossierId,
				 recordSubcategoryId,
				 [date],
				 expense, 
				 [description])
		select
			dossierId,
			(
				select 
					eSC.recordSubcategoryId  
				from
					recordSubcategory eSC
					join recordCategory eC on eSC.recordCategoryId=eC.recordCategoryId
				where
					eSC.[description]=subcategory
					and eC.[description]=category
					and eC.dossierId=dossierId
			
			) recordSubcategoryId,
			[date],
			expense,
			[description]
		from 
			importRecordTmp
		where
			dossierId=@dossierId 
	

END;

GO
CREATE PROCEDURE [dbo].[CreateReport_BalanceSheet]
    -- Add the parameters for the stored procedure here
    (@dossierId int,
	@beginDate nvarchar(10),
	@endDate nvarchar(10))
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

	With EXP_CAT AS
	(
		select 
			expense,
			eC.[description] category,
			eSC.[description] subcategory
		from record e
			join recordSubcategory eSC on e.recordSubcategoryId= eSC.recordSubcategoryId
			join recordCategory eC on eC.recordCategoryId=eSC.recordCategoryId
			and e.dossierId=@dossierId
			and	e.[date] between CONVERT(datetime,@beginDate, 103) and CONVERT(datetime,@endDate, 103)
	)
	select
		GROUPING_ID(category, subcategory) as 'bitmap', 
		category,
		subcategory,
		SUM (expense) total
	from
		EXP_CAT
	Group by 
		category, 
		subcategory
	with rollup
	order by
		category,
		total desc,
		subcategory


END;

GO
CREATE PROCEDURE [dbo].[DeleteDossier]
    -- Add the parameters for the stored procedure here
    @dossierId int
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

	begin tran;
	
	delete from record where dossierId=@dossierId;
	delete from recurrentRecord where dossierId=@dossierId;
	delete from 
		recordSubcategory 
	where 
		recordCategoryId in 
		(
			Select 
				recordCategoryId
			from
				recordCategory
			where
				dossierId=@dossierId
		)
	
	
	
	delete from recordCategory where dossierId=@dossierId;
	delete from importRecordTmp where dossierId=@dossierId;
	delete from liability where dossierId=@dossierId;
	delete from asset where dossierId=@dossierId;
	delete from revenue where dossierId=@dossierId;
	delete from recurrentRevenue where dossierId=@dossierId;
	delete from revenueCategory where dossierId=@dossierId;

	delete from 
		revenueSubcategory 
	where 
		revenueCategoryId in 
		(
			Select 
				revenueCategoryId
			from
				revenueCategory
			where
				dossierId=@dossierId
		)
	
	
	delete from dossier where dossierId=@dossierId;

	delete from record where dossierId=@dossierId;
	delete from recurrentRecord where dossierId=@dossierId;
	
	delete from 
		recordSubcategory 
	where 
		recordCategoryId in 
		(
			Select 
				recordCategoryId
			from
				recordCategory
			where
				dossierId=@dossierId
		)
	
	
	
	delete from recordCategory where dossierId=@dossierId;
	delete from importRecordTmp where dossierId=@dossierId;
	delete from liability where dossierId=@dossierId;
	delete from asset where dossierId=@dossierId;
	delete from revenue where dossierId=@dossierId;
	delete from recurrentRevenue where dossierId=@dossierId;
	delete from revenueCategory where dossierId=@dossierId;

	delete from 
		revenueSubcategory 
	where 
		revenueCategoryId in 
		(
			Select 
				revenueCategoryId
			from
				revenueCategory
			where
				dossierId=@dossierId
		)
	
	
	delete from dossier where dossierId=@dossierId;

	commit;


END;

GO
CREATE PROCEDURE [dbo].[GetrecordSubcategoriesByDossierId]
    -- Add the parameters for the stored procedure here
    @dossierId int = null
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here
	select subcat.recordSubcategoryId, 
		   subcat.[description], 
		   subcat.recordCategoryId
	from recordCategory cat 
		 join recordSubcategory subcat on subcat.recordCategoryId = cat.recordCategoryId 
	where cat.dossierId = @dossierId;

END;

GO
CREATE FUNCTION [dbo].[GetMaxYearDossier]
    -- Add the parameters for the stored procedure here
    (@dossierId int)

RETURNS INT

AS

BEGIN	
	DECLARE @Year int = 0;

  
    -- Insert statements for procedure here
	select @Year=max(year(date)) 
	from record
	group by dossierId
	having dossierId=@dossierId;

	if @Year=null or @Year=0  
		set @Year=YEAR(GETDATE())	

	RETURN @Year
	
END;

GO
CREATE FUNCTION [dbo].[GetMinYearDossier]
    -- Add the parameters for the stored procedure here
    (@dossierId int)

RETURNS INT
AS


BEGIN	
	DECLARE @Year int = 0;

  
    -- Insert statements for procedure here
	select @Year=min(year(date)) 
	from record
	group by dossierId
	having dossierId=@dossierId;
 
	if @Year=null or @Year=0 
		set @Year=YEAR(GETDATE())	

	RETURN @Year


END;


COMMIT transaction;