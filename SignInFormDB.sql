IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'SignInFormDB')

BEGIN
  print '' print  '*** Dropping database SignInFormDB'
  DROP DATABASE SignInFormDB
END
GO

print '' print '*** Creating database SignInFormDB'
GO
CREATE DATABASE SignInFormDB
GO

print '' print '*** using database SignInFormDB'
GO
USE [SignInFormDB]
GO

print '' print '*** Creating Visitor Table'
GO
CREATE TABLE [dbo].[Visitor] (
	[VisitorID]			[int] IDENTITY(10000, 1) NOT NULL,		 
	[FirstName]			[varchar](50) 			 NOT NULL,		  
	[LastName]			[varchar](100)			 NOT NULL,		
	[Company]			[varchar](100)			 NOT NULL,
	[PersonVisiting]	[varchar](100) 		 	 NOT NULL,		  
	[Citizen]			[bit] 					 NOT NULL	  
	
	CONSTRAINT [pk_VisitorID] PRIMARY KEY ([VisitorID] ASC)
)
GO

print ''print'*** Inserting Visitor Records'
INSERT INTO [dbo].[Visitor]
	([FirstName],[LastName],[Company],[PersonVisiting],[Citizen])
	VALUES
	('DeeDee', 'Marz', 'CompanyA', 'asmith', 1),
	('Pickles', 'Campbell', 'CompanyB', 'ajacobs', 0),
	('George', 'Simmonds', 'CompanyC', 'ajacobs', 0),
	('Rover', 'Davis', 'CompanyD', 'asmith', 1)
GO

print '' print '*** Creating SignInRecord Table'
GO
CREATE TABLE [dbo].[SignInRecord] (
	[SignInID]			[int] IDENTITY(10000, 1) NOT NULL,		  
	[VisitorID]			[int]					 NOT NULL,	
	[SignedIn]			[bit] 					 NOT NULL DEFAULT 1,
	[SignedInTime]		[datetime]				 NOT NULL DEFAULT getdate() 

	CONSTRAINT [pk_SignInID] PRIMARY KEY ([SignInID] ASC)
)
GO

print ''print'*** Inserting SignInRecord Records'
INSERT INTO [dbo].[SignInRecord]
	([VisitorID],[SignedIn],[SignedInTime])
	VALUES
	(10000, 1, '20170628 10:34:09 AM'),
	(10001, 0, '20170628 08:45:00 AM'),
	(10002, 1, '20170628 11:20:11 AM'),
	(10003, 1, '20170628 09:05:01 AM')
GO

print '' print '*** Creating SignInRecord VisitorID foreign key'
GO
ALTER TABLE [dbo].[SignInRecord]  WITH NOCHECK 
	ADD CONSTRAINT [FK_VisitorID] FOREIGN KEY([VisitorID])
	REFERENCES [dbo].[Visitor] ([VisitorID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating sp_create_visitor'
GO
CREATE PROCEDURE [dbo].[sp_create_visitor]
	(
		@FirstName			[varchar](50), 
		@LastName			[varchar](100), 
		@Company			[varchar](100),
		@PersonVisiting		[varchar](100), 
		@Citizen			[bit],
		@SignedIn			[bit],
		@SignedInTime		[datetime] 
	)
AS
	BEGIN	
		DECLARE @ID INT
		INSERT INTO dbo.[Visitor]
			([FirstName],[LastName],[Company],[PersonVisiting],[Citizen])
		VALUES
			(@FirstName, @LastName, @Company, @PersonVisiting, @Citizen)
		SELECT @ID = SCOPE_IDENTITY()
		INSERT INTO dbo.[SignInRecord]
			([VisitorID],[SignedIn],[SignedInTime])
		VALUES
			(@ID, 1, getdate())
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_signed_in'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_signed_in]
	(
		@SignedIn		[bit]
	)
AS
	BEGIN
		SELECT	FirstName, LastName, Company, PersonVisiting, 
				Citizen, SignedIn, SignedInTime, Visitor.VisitorID
		FROM dbo.[Visitor],dbo.[SignInRecord]
		WHERE SignedIn = 1
		AND [Visitor].[VisitorID] = [SignInRecord].[VisitorID] 
	END
GO

print '' print '*** Creating sp_check_out_visitor'
GO
CREATE PROCEDURE [dbo].[sp_check_out_visitor]
	(
		@VisitorID		[int]
	)
AS
	BEGIN
		UPDATE dbo.[SignInRecord]
			SET SignedIn = 0
			WHERE VisitorID = @VisitorID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_signed_out'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_signed_out]
	(	
		@SignedIn		[bit]
	)
AS
	BEGIN
		SELECT	FirstName, LastName, Company, PersonVisiting, 
				Citizen, SignedIn, SignedInTime, Visitor.VisitorID
		FROM dbo.[Visitor],dbo.[SignInRecord]
		WHERE SignedIn = 0
		AND [Visitor].[VisitorID] = [SignInRecord].[VisitorID] 
	END
GO

print '' print '*** Creating sp_retrieve_visitor_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_visitor_by_id]
	(
		@VisitorID		[int]
	)
AS
	BEGIN
		SELECT	FirstName, LastName, Company, PersonVisiting, 
				Citizen, SignedIn, SignedInTime, Visitor.VisitorID
		FROM dbo.[Visitor],dbo.[SignInRecord]
		WHERE [Visitor].[VisitorID] = [SignInRecord].[VisitorID] 
	END
GO
