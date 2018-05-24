/* Check if database already exists and delete it if it does exist*/
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'kirkwoodLibrary') 
BEGIN
	DROP DATABASE kirkwoodLibrary
	print '' print '*** dropping database kirkwoodLibrary'
END
GO

print '' print '*** creating database kirkwoodLibrary'
GO
CREATE DATABASE kirkwoodLibrary
GO

print '' print '*** using database kirkwoodLibrary'
GO
USE [kirkwoodLibrary]
GO

/* *********** CREATION OF TABLES *************** */

print '' print '*** Creating Adminn Table'
GO
CREATE TABLE [dbo].[Adminn](
    [AdminnID] 	    [nvarchar](20) 			NOT NULL,
	[FirstName]		[nvarchar](50)			NOT NULL,
	[LastName]		[nvarchar](100)			NOT NULL,
	[PhoneNumber]	[nvarchar](15)			NOT NULL,
	[AdminnEmail]	[nvarchar](100)			NOT NULL,
	[PasswordHash]	[nvarchar](100)			NOT NULL DEFAULT '9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	[Active]		[bit]					NOT NULL DEFAULT 1,
	CONSTRAINT [pk_AdminnID] PRIMARY KEY([AdminnID] ASC),
	CONSTRAINT [ak_EmailAdminn] UNIQUE ([AdminnEmail] ASC)
)
GO

print '' print '*** Creating Student Table'
GO
CREATE TABLE [dbo].[Student](
    [StudentID] 	[nvarchar](20) 			NOT NULL,
    [FirstName]		[nvarchar](50)			NOT NULL,
	[LastName]		[nvarchar](100)			NOT NULL,
	[PhoneNumber]	[nvarchar](15)			NOT NULL,
	[StudentEmail]	[nvarchar](100)			NOT NULL,
	[PasswordHash]	[nvarchar](100)			NOT NULL DEFAULT '9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	[Active]		[bit]					NOT NULL DEFAULT 1,
	CONSTRAINT [pk_StudentID] PRIMARY KEY([StudentID] ASC),
	CONSTRAINT [ak_EmailStudent] UNIQUE ([StudentEmail] ASC)
)
GO

print '' print '*** Creating Book Table'
GO
CREATE TABLE [dbo].[Book](
    [BookID]            [int] IDENTITY (100000,1) 	NOT NULL,
    [ISBN]              [nvarchar](20) 		    NOT NULL,
    [Edition]		    [nvarchar](20)			NOT NULL,
    [Title]		        [nvarchar](100)			NOT NULL,
    [EditionYear]       [int]			    NOT NULL,
    [Description]       [nvarchar](255)			NOT NULL,
	[CategoryID]        [nvarchar](50)			NOT NULL,
	[AuthorID]	        [int]       			NOT NULL,
    [LibraryID]	        [nvarchar](50)  			NOT NULL,
	[StatusID]	    	[nvarchar](50)			NOT NULL DEFAULT 'Available',
    [StudentEmail]         [nvarchar](100)	            NULL,
    [DateOfCheckout]    dateTime	            NULL,
    [DateToReturn]	    dateTime			    NULL,
	[Active]		[bit]					NOT NULL DEFAULT 1,
    
	CONSTRAINT [pk_BookID] PRIMARY KEY([BookID] ASC)
)
GO

print '' print '*** Creating Status Table'
GO
CREATE TABLE [dbo].[Status](
	[StatusID]	[nvarchar](50)	NOT NULL,
	
	CONSTRAINT [pk_StatusID] PRIMARY KEY([StatusID] ASC)
)
GO

print '' print '*** Creating Author Table'
GO
CREATE TABLE [dbo].[Author](
    [AuthorID] 	    [int] 			IDENTITY(100000,1)	NOT NULL,
    [FirstName]		[nvarchar](50)			NOT NULL,
	[LastName]		[nvarchar](100)			NOT NULL,
    [Description]   [nvarchar](255)			NOT NULL,
	CONSTRAINT [pk_AuthorID] PRIMARY KEY([AuthorID] ASC)
)
GO

print '' print '*** Creating Category Table'
GO
CREATE TABLE [dbo].[Category](
    [CategoryID]        [nvarchar](50)			NOT NULL,
	CONSTRAINT [pk_CategoryID] PRIMARY KEY([CategoryID] ASC)
)
GO

print '' print '*** Creating Library Table'
GO
CREATE TABLE [dbo].[Library](
    
    [LibraryID]		        [nvarchar](50)			NOT NULL,
    [Address]		    [nvarchar](100)			NOT NULL,
	[City]		        [nvarchar](100)			NOT NULL,
    [State]		        [nvarchar](100)			NOT NULL,
    [Zip]		        [nvarchar](10)			NOT NULL,
    
	CONSTRAINT [pk_Library] PRIMARY KEY([LibraryID] ASC)
)
GO

print '' print '*** Creating Conversation Table'
GO
CREATE TABLE [dbo].[Conversation](
    [ConversationID] 	[int] 			IDENTITY(100000,1)	NOT NULL,
    [Subject]		    [nvarchar](100)			NOT NULL,
    [StudentID]		    [nvarchar](20)			NOT NULL,
    [AdminnID]		    [nvarchar](20)			NOT NULL,
    
	CONSTRAINT [pk_ConversationID] PRIMARY KEY([ConversationID] ASC)
)
GO

print '' print '*** Creating Message Table'
GO
CREATE TABLE [dbo].[Message](
    [MessageID] 	[int] 			IDENTITY(100000,1)	NOT NULL,
    [Text]		    [nvarchar](100)			NOT NULL,
    [ConversationID] 	[int]                   NOT NULL,
    [DateOfMessage] 	[dateTime]              NOT NULL,
    [Sender]		    [nvarchar](100)			NOT NULL,
    
    CONSTRAINT [pk_MessageID] PRIMARY KEY([MessageID] ASC)    
)
GO


/* ************** Create Indexes ******************** */

print '' print '*** Creating Index for Adminn.Email'
GO
CREATE NONCLUSTERED INDEX [ix_Adminn_Email] ON [dbo].[Adminn]([AdminnEmail]);
GO

print '' print '*** Creating Index for adminn.Email'
GO
CREATE NONCLUSTERED INDEX [ix_Student_Email] ON [dbo].[Student]([StudentEmail]);
GO

/* ************** Foreign Key Constraints ******************** */

print '' print '*** Creating Book CategoryID foreign key'
GO
ALTER TABLE [dbo].[Book]  WITH NOCHECK 
	ADD CONSTRAINT [FK_CategoryID] FOREIGN KEY([CategoryID])
	REFERENCES [dbo].[Category] ([CategoryID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating Book StatusID foreign key'
GO
ALTER TABLE [dbo].[Book]  WITH NOCHECK 
	ADD CONSTRAINT [FK_StatusID] FOREIGN KEY([StatusID])
	REFERENCES [dbo].[Status] ([StatusID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating Book AuthorID foreign key'
GO
ALTER TABLE [dbo].[Book]  WITH NOCHECK 
	ADD CONSTRAINT [FK_AuthorID] FOREIGN KEY([AuthorID])
	REFERENCES [dbo].[Author] ([AuthorID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating Book StudentEmail foreign key'
GO
ALTER TABLE [dbo].[Book]  WITH NOCHECK 
	ADD CONSTRAINT [FK_StudentEmail] FOREIGN KEY([StudentEmail])
	REFERENCES [dbo].[Student] ([StudentEmail])
	ON UPDATE CASCADE
GO

print '' print '*** Creating Book LibraryID foreign key'
GO
ALTER TABLE [dbo].[Book]  WITH NOCHECK 
	ADD CONSTRAINT [FK_LibraryID] FOREIGN KEY([LibraryID])
	REFERENCES [dbo].[Library] ([LibraryID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating Conversation StudentID foreign key'
GO
ALTER TABLE [dbo].[Conversation]  WITH NOCHECK 
	ADD CONSTRAINT [FK2_StudentID] FOREIGN KEY([StudentID])
	REFERENCES [dbo].[Student] ([StudentID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating Conversation AdminnID foreign key'
GO
ALTER TABLE [dbo].[Conversation]  WITH NOCHECK 
	ADD CONSTRAINT [FK_AdminnID] FOREIGN KEY([AdminnID])
	REFERENCES [dbo].[Adminn] ([AdminnID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating Message ConversationID foreign key'
GO
ALTER TABLE [dbo].[Message]  WITH NOCHECK 
	ADD CONSTRAINT [FK_ConversationID] FOREIGN KEY([ConversationID])
	REFERENCES [dbo].[Conversation] ([ConversationID])
	ON UPDATE CASCADE
GO


/* *****************   Sample Data ************************** */
print '' print '*** Creating Roles Table'
GO
CREATE TABLE [dbo].[Role](
	[RoleID]				[nvarchar](50)					NOT NULL,
	[RoleDescription]		[nvarchar](250)					NOT NULL,
	CONSTRAINT [pk_RoleID] PRIMARY KEY([RoleID] ASC)
)
GO

print '' print '*** Inserting Role Records'
GO
INSERT INTO [dbo].[Role]
		([RoleID], [RoleDescription])
	VALUES
		('Administator', 'Kirkwood LIBRARY Administator'),
		('Student', 'Kirkwood Student')
GO


print '' print '*** Creating StudentRole Table'
GO
CREATE TABLE [dbo].[StudentRole](
	[StudentID]	[nvarchar](20)			NOT NULL,
	[RoleID]		[nvarchar](50)			NOT NULL,
	[Active]		[bit]					NOT NULL DEFAULT 1,				
	CONSTRAINT [pk_StudentIDRoleID] PRIMARY KEY([StudentID] ASC, [RoleID] ASC)
)
GO

print '' print '*** Inserting StudentRole Records'
INSERT INTO [dbo].[StudentRole]
		([StudentID], [RoleID])
	VALUES
		('k000001', 'Student'),
		('k000002', 'Student'),
		('k000003', 'Student'),
		('k000004', 'Student'),
		('k000005', 'Student')
GO		
		

print '' print '*** Creating sp_retrieve_student_roles'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_student_roles]
	(
	@StudentID		[nvarchar](20)
	)
AS
	BEGIN
		SELECT 	[RoleID]
		FROM 	[StudentRole]
		WHERE 	[StudentRole].[StudentID] = @StudentID
		AND		[Active] = 1
	END
GO

print '' print '*** Inserting Adminn Test Records'
GO
INSERT INTO [dbo].[Adminn]
		([AdminnID], [FirstName], [LastName], [PhoneNumber], [AdminnEmail])
	VALUES
		('Adminn001', 'Badis', 'Saidani', '3195556666', 'badis@kirkwood.edu')
GO 

print '' print '*** Inserting Student Test Records'
GO
INSERT INTO [dbo].[Student]
		([StudentID], [FirstName], [LastName], [PhoneNumber], [StudentEmail])
	VALUES
		
		('k000001', 'Joanne', 'Smith', '3195556666', 'joanne@kirkwood.edu'),
		('k000002','Martin', 'Jones', '3195557777', 'martin@kirkwood.edu'),
		('k000003','Leo', 'Williams', '3195558888', 'leo@kirkwood.edu'),
		('k000004','Sally', 'Johnson', '3195559999', 'sally@kirkwood.edu'),
		('k000005','Larry', 'loser', '3195550000', 'Larry@kirkwood.edu')
GO 

print '' print '*** Inserting Category Test Records'
GO
INSERT INTO [dbo].[Category]
		([CategoryID])
	VALUES
		('Self-help'),
		('Novel'),
        ('Fiction'),
        ('Romance')
GO 


print '' print '*** Inserting Author Test Records'
GO
INSERT INTO [dbo].[Author]
		([FirstName], [LastName], [Description])
	VALUES
		('Napleon', 'Hill', 'Oliver Napoleon Hill was an American self-help author. He is known best for his book Think and Grow Rich which is among the 10 best selling self-help books of all time.'),
		('George', 'Orwell', 'Eric Arthur Blair, better known by his pen name George Orwell, was an English novelist, essayist, journalist, and critic.'),
		('Jean Jacques', 'Russau', 'Jean-Jacques Rousseau was a Francophone Genevan philosopher, writer, and composer of the 18th century. ')
GO 


print '' print '*** Inserting Library Test Records'
GO
INSERT INTO [dbo].[Library]
		([LibraryID], [Address], [City], [State], [Zip])
	VALUES
        ('Cedar Rapids Library', '6301 Kirkwood Blvd SW', 'Cedar Rapids', 'Iowa', 52404),
		('Iowa City Library', ' 1816 Lower Muscatine Rd', 'Iowa City', 'Iowa', 52240 )
		
GO 



print '' print '*** Inserting into Status Test Records'
GO
INSERT INTO [dbo].[Status]
		([StatusID])
	VALUES
		('Available'),
		('Out'),
		('In Held')
GO

print '' print '*** Inserting Book Test Records'
GO
INSERT INTO [dbo].[Book]
		([ISBN], [Edition], [Title], [EditionYear], [Description], [CategoryID], [AuthorID], [LibraryID], [StatusID])
	VALUES
		('9788545554', 'First', 'Think and grow rich', 1994, 'A book authored by Napleon Hill', 'Self-help', 100000, 'Cedar Rapids Library', 'Available'),
		('9665745578', 'First', '1984', 1949, 'A Novel authored by George Orwell', 'Novel', 100001, 'Cedar Rapids Library','Out'),
		('6458127777', 'Second', 'Miserables', 1986, 'A Novel authored by John J.Russau', 'Novel', 100002, 'Iowa City Library', 'In Held')
GO 



/* *****************  Stored Procedures   ******************* */

/* ----- Stored Procedures for Adminn ----- */

print '' print '*** Creating sp_authenticate_adminn'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_adminn]
	(
	@AdminnEmail			[nvarchar](100),
	@PasswordHash	[nvarchar](100)
	)
AS
	BEGIN
		SELECT COUNT([AdminnID])
		FROM 	[Adminn]
		WHERE 	[AdminnEmail] = @AdminnEmail
		AND 	[PasswordHash] = @PasswordHash
		AND		[Active] = 1
	END
GO

print '' print '*** Creating sp_retrieve_adminn_by_email'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_adminn_by_email]
	(
	@AdminnEmail		[nvarchar](100)
	)
AS
	BEGIN
		SELECT 	[AdminnID], [FirstName], [LastName], [PhoneNumber], [AdminnEmail], [Active]
		FROM 	[Adminn]
		WHERE 	[AdminnEmail] = @AdminnEmail
	END
GO	

print '' print '*** Adding sp_select_active_book'
GO
CREATE PROCEDURE [dbo].[sp_select_book_by_active]
	(
	@Active		[bit]
	)
AS
	BEGIN
		SELECT 	[BookID], [ISBN], [Edition], [Title], [EditionYear], [Description], [CategoryID], [AuthorID], [LibraryID],  [StatusID],
		[StudentEmail], [DateOfCheckout], [DateToReturn], [Active]
		FROM 	[Book]
		WHERE 	[Active] = 1
		ORDER BY [Title]
	END
GO	

print '' print '*** Adding sp_select_book_by_id'
GO
CREATE PROCEDURE [dbo].[sp_select_book_by_id]
	(
	@BookID 	[int] 
	)
AS
	BEGIN
		SELECT 	[BookID], [ISBN], [Edition], [Title], [EditionYear], [Description], [CategoryID], [AuthorID], [LibraryID],  [StatusID],
		[StudentEmail], [DateOfCheckout], [DateToReturn], [Active]
		FROM 	[Book]
		WHERE 	[BookID] = @BookID
	END
GO	

print '' print '*** Adding sp_select_book_by_isbn'
GO
CREATE PROCEDURE [dbo].[sp_select_book_by_isbn]
	(
	@ISBN	[nvarchar](20) 
	)
AS
	BEGIN
		SELECT 	[BookID], [ISBN], [Edition], [Title], [EditionYear], [Description], [CategoryID], [AuthorID], [LibraryID], [StatusID], [StudentEmail], [DateOfCheckout], [DateToReturn], [Active]
		FROM 	[Book]
		WHERE 	[ISBN] = @ISBN
	END
GO	

print '' print '*** Adding sp_select_book_by_words'
GO
CREATE PROCEDURE [dbo].[sp_select_book_by_words]
	(
	@words	[nvarchar](100) 
	)
AS
	BEGIN
		SELECT 	[BookID], [ISBN], [Edition], [Title], [EditionYear], [Book].[Description], [CategoryID], [Book].[AuthorID], [Book].[LibraryID],  [StatusID],
		[StudentEmail], [DateOfCheckout], [DateToReturn], [Active]
		FROM 	[Book], [Author]
		WHERE [Book].[AuthorID] = [Author].[AuthorID]	
		AND  (		[Title] LIKE CONCAT('%',@words,'%')
			OR      [Book].[ISBN] LIKE CONCAT('%',@words,'%')
			OR      [Author].[FirstName] LIKE CONCAT('%',@words,'%')
			OR      [Author].[LastName] LIKE CONCAT('%',@words,'%')
			OR      [Book].[Description] LIKE CONCAT('%',@words,'%')
		)
	END
GO	

print '' print '*** Adding sp_select_book_by_studentemail'
GO
CREATE PROCEDURE [dbo].[sp_select_book_by_studentemail]
	(
	@StudentEmail	[nvarchar](100) 
	)
AS
	BEGIN
		SELECT 	[BookID], [ISBN], [Edition], [Title], [EditionYear], [Description], [CategoryID], [AuthorID], [LibraryID], [StatusID], [StudentEmail], [DateOfCheckout], [DateToReturn]
		FROM 	[Book]
		WHERE 	[StudentEmail] LIKE @StudentEmail
	END
GO	

print '' print '*** Adding sp_add_book'
GO
CREATE PROCEDURE [dbo].[sp_add_book]
	(
    @ISBN              [nvarchar](20),
    @Edition		    [nvarchar](20),
    @Title		        [nvarchar](100),
    @EditionYear       [int],
    @Description       [nvarchar](255),
	@CategoryID        [nvarchar](50),
	@AuthorID        [int],
    @LibraryID	        [nvarchar](50)
	)
AS
	BEGIN
		INSERT INTO Book ([ISBN], [Edition], [Title], [EditionYear], [Description], [CategoryID], [AuthorID], [LibraryID])
		VALUES	(@ISBN, @Edition, @Title, @EditionYear, @Description, @CategoryID, @AuthorID, @LibraryID)
	END
GO	


print '' print '*** Adding sp_delete_book_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_book_by_id]
	(
    @BookID	        [int]
	)
AS
	BEGIN
		DELETE 
        FROM Book 
        WHERE [BookID] = @BookID
	END
GO	

print '' print '*** Adding sp_edit_book'
GO
CREATE PROCEDURE [dbo].[sp_edit_book]
	(
	@BookID             [int],
    @ISBN              [nvarchar](20),
    @Edition		   [nvarchar](20),
    @Title		       [nvarchar](100),
    @EditionYear       [int],
    @Description       [nvarchar](255),
	@CategoryID        [nvarchar](50),
	@AuthorID          [int],
    @LibraryID	       [nvarchar](50),
    @StatusID      [nvarchar](30),
    @StudentEmail         [nvarchar](20),
    @DateOfCheckout    dateTime ,
    @DateToReturn      dateTime
	)
AS
	BEGIN
		UPDATE Book 
        SET [ISBN] = @ISBN, [Edition] = @Edition, [Title] = @Title, [EditionYear] = @EditionYear, [Description] = @Description, 
        [CategoryID] = @CategoryID, [AuthorID] = @AuthorID, [LibraryID] = @LibraryID, [StatusID] = @StatusID,
        [StudentEmail] = @StudentEmail, [DateOfCheckout] = @DateOfCheckout, [DateToReturn] = @DateToReturn
		where BookID = @BookID
		return @@rowcount
	END
GO

print '' print '*** Adding sp_checkout_book'
GO
CREATE PROCEDURE [dbo].[sp_checkout_book]
	(
    @BookID            [int],
    @StudentEmail         [nvarchar](100),
    @DateOfCheckout    dateTime,
    @DateToReturn      dateTime
	)
AS
	BEGIN
		UPDATE Book 
        SET [StatusID] = 'Not Available',
        [StudentEmail] = @StudentEmail, [DateOfCheckout] = @DateOfCheckout, [DateToReturn] = @DateToReturn
        WHERE [BookID] = @BookID AND [StatusID] = 'Available'
	END
GO



/* ----- Stored Procedures for Student ----- */

print '' print '*** Creating sp_retrieve_student_by_email'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_student_by_email]
	(
	@StudentEmail		[nvarchar](100)
	)
AS
	BEGIN
		SELECT 	[StudentID], [FirstName], [LastName], [PhoneNumber], [StudentEmail], [Active]
		FROM 	[Student]
		WHERE 	[StudentEmail] = @StudentEmail
	END
GO	

print '' print '*** Creating sp_retrieve_student_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_student_by_active]
	(
	@Active		bit
	)
AS
	BEGIN
		SELECT 	[StudentID], [FirstName], [LastName], [PhoneNumber], [StudentEmail], [Active]
		FROM 	[Student]
		WHERE 	[Active] = 1
		ORDER BY [FirstName], [LastName]
	END
GO	

print '' print '*** Creating sp_retrieve_student_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_student_by_id]
	(
	@StudentID		[nvarchar](20)
	)
AS
	BEGIN
		SELECT 	[StudentID], [FirstName], [LastName], [PhoneNumber], [StudentEmail], [Active]
		FROM 	[Student]
		WHERE 	[StudentID] = @StudentID
	END
GO	

print '' print '*** Creating sp_retrieve_student_by_name'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_student_by_name]
	(
	@name		[nvarchar](50)
	)
AS
	BEGIN
		SELECT 	[StudentID], [FirstName], [LastName], [PhoneNumber], [StudentEmail], [Active]
		FROM 	[Student]
		WHERE 	[FirstName] LIKE CONCAT('%',@name,'%')
		OR		[LastName] LIKE CONCAT('%',@name,'%')
		OR		CONCAT([FirstName],' ',[LastName]) LIKE CONCAT('%',@name,'%')
		OR		CONCAT([LastName],' ',[FirstName]) LIKE CONCAT('%',@name,'%')
	END
GO	

print '' print '*** Creating sp_authenticate_student'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_student]
	(
	@StudentEmail			[nvarchar](100),
	@PasswordHash	[nvarchar](100)
	)
AS
	BEGIN
		SELECT COUNT([StudentID])
		FROM 	[Student]
		WHERE 	[StudentEmail] = @StudentEmail
		AND 	[PasswordHash] = @PasswordHash
		AND		[Active] = 1
	END
GO

print '' print '*** Adding sp_creation_student_account'
GO
CREATE PROCEDURE [dbo].[sp_creation_student_account]
	(
    @StudentID          [nvarchar](20), 
    @FirstName          [nvarchar](50), 
    @LastName           [nvarchar](100),
    @PhoneNumber        [nvarchar](15), 
    @StudentEmail       [nvarchar](100),
    @PasswordHash       [nvarchar](100)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Student]
		([StudentID], [FirstName], [LastName], [PhoneNumber], [StudentEmail], [PasswordHash])
	    VALUES
		(@StudentID, @FirstName, @LastName, @PhoneNumber, @StudentEmail, @PasswordHash)
    END
GO

print '' print '*** Adding sp_edit_student_account'
GO
CREATE PROCEDURE [dbo].[sp_edit_student_account]
	(
    @StudentID          [nvarchar](20), 
    @FirstName          [nvarchar](50), 
    @LastName           [nvarchar](100),
    @PhoneNumber        [nvarchar](15), 
    @StudentEmail       [nvarchar](100),
    @NewPasswordHash    [nvarchar](100),
    @OldPasswordHash    [nvarchar](100)
	)
AS
	BEGIN
		UPDATE Student 
        SET [FirstName] = @FirstName,
        [LastName] = @LastName, [PhoneNumber] = @PhoneNumber, [StudentEmail] = @StudentEmail
        WHERE [StudentID] = @StudentID AND [PasswordHash] = @OldPasswordHash
	END
GO


print '' print '*** Adding sp_hold_book'
GO
CREATE PROCEDURE [dbo].[sp_hold_book]
	(
    @BookID            [int],
    @StudentEmail         [nvarchar](100)
   
	)
AS
	BEGIN
		UPDATE Book 
        SET [StatusID] = 'In Held',
        [StudentEmail] = @StudentEmail
        WHERE [BookID] = @BookID 
	END
GO

print '' print '*** Adding sp_hold_book_by_student_email'
GO
CREATE PROCEDURE [dbo].[sp_hold_book_by_student_email]
	(
    @BookID            [int],
    @StudentEmail	   [nvarchar](100)
   
	)
AS
	BEGIN
		UPDATE Book 
        SET [StatusID] = 'In Held',
        [StudentEmail] = @StudentEmail
        WHERE [BookID] = @BookID 
	END
GO

print '' print '*** Adding sp_select_status_list'
GO
CREATE PROCEDURE [dbo].[sp_select_status_list]
	
AS
	BEGIN
		SELECT 	[StatusID]
		FROM 	[Status]
	END
GO	

print '' print '*** Adding sp_select_category_list'
GO
CREATE PROCEDURE [dbo].[sp_select_category_list]
	
AS
	BEGIN
		SELECT 	[CategoryID]
		FROM 	[Category]
	END
GO	


print '' print '*** Adding sp_select_library_list'
GO
CREATE PROCEDURE [dbo].[sp_select_library_list]
	
AS
	BEGIN
		SELECT 	[LibraryID]
		FROM 	[Library]
	END
GO	

print '' print '*** Creating sp_insert_book'
GO
CREATE PROCEDURE [dbo].[sp_insert_book]
	(
	 @ISBN              [nvarchar](20),
    @Edition		   [nvarchar](20),
    @Title		       [nvarchar](100),
    @EditionYear       [int],
    @Description       [nvarchar](255),
	@CategoryID        [nvarchar](50),
	@AuthorID          [int],
    @LibraryID	       [nvarchar](50),
    @StatusID      [nvarchar](30)
    
	)
AS
	BEGIN
		INSERT INTO [dbo].[Book]
			([ISBN], [Edition], [Title], [EditionYear], [Description], [CategoryID], [AuthorID], [LibraryID])
		VALUES
			(@ISBN, @Edition, @Title, @EditionYear, @Description, @CategoryID, @AuthorID, @LibraryID)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Inserting sp_update_book'
GO
CREATE PROCEDURE [dbo].[sp_update_book]
	(
	@BookID            [int],
	@ISBN              [nvarchar](20),
    @Edition		   [nvarchar](20),
    @Title		       [nvarchar](100),
    @EditionYear       [int],
    @Description       [nvarchar](255),
	@CategoryID        [nvarchar](50),
	@AuthorID          [int],
    @LibraryID	       [nvarchar](50),
    @StatusID      [nvarchar](30),
    @StudentEmail         [nvarchar](100),
    @DateOfCheckout    dateTime, 
    @DateToReturn      dateTime,
	
	@OldISBN              [nvarchar](20),
    @OldEdition		   [nvarchar](20),
    @OldTitle		       [nvarchar](100),
    @OldEditionYear       [int],
    @OldDescription       [nvarchar](255),
	@OldCategoryID        [nvarchar](50),
	@OldAuthorID          [int],
    @OldLibraryID	       [nvarchar](50),
    @OldStatusID      [nvarchar](30)
    
	)
AS
	BEGIN
	UPDATE Book 
        SET [ISBN] = @ISBN, 
			[Edition] = @Edition, 
			[Title] = @Title, 
			[EditionYear] = @EditionYear, 
			[Description] = @Description, 
			[CategoryID] = @CategoryID, 
			[AuthorID] = @AuthorID, 
			[LibraryID] = @LibraryID, 
			[StatusID] = @StatusID,
			[StudentEmail] = @StudentEmail, 
			[DateOfCheckout] = @DateOfCheckout, 
			[DateToReturn] = @DateToReturn
		WHERE [BookID] = @BookID
			AND [ISBN] = @OldISBN
			AND [Edition] = @OldEdition
			AND	[Title] = @OldTitle
			AND	[EditionYear] = @OldEditionYear
			AND	[Description] = @OldDescription
			AND [CategoryID] = @OldCategoryID
			AND	[AuthorID] = @OldAuthorID
			AND	[LibraryID] = @OldLibraryID
			AND	[StatusID] = @OldStatusID
			
	
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Inserting sp_update_out_book'
GO
CREATE PROCEDURE [dbo].[sp_update_out_book]
	(
	@BookID            [int],
	@ISBN              [nvarchar](20),
    @Edition		   [nvarchar](20),
    @Title		       [nvarchar](100),
    @EditionYear       [int],
    @Description       [nvarchar](255),
	@CategoryID        [nvarchar](50),
	@AuthorID          [int],
    @LibraryID	       [nvarchar](50),
    @StatusID      [nvarchar](30),
    @StudentEmail         [nvarchar](100),
    @DateOfCheckout    dateTime, 
    @DateToReturn      dateTime,
	
	@OldISBN              [nvarchar](20),
    @OldEdition		   [nvarchar](20),
    @OldTitle		       [nvarchar](100),
    @OldEditionYear       [int],
    @OldDescription       [nvarchar](255),
	@OldCategoryID        [nvarchar](50),
	@OldAuthorID          [int],
    @OldLibraryID	       [nvarchar](50),
    @OldStatusID      [nvarchar](30)
    
	)
AS
	BEGIN
	UPDATE Book 
        SET [ISBN] = @ISBN, 
			[Edition] = @Edition, 
			[Title] = @Title, 
			[EditionYear] = @EditionYear, 
			[Description] = @Description, 
			[CategoryID] = @CategoryID, 
			[AuthorID] = @AuthorID, 
			[LibraryID] = @LibraryID, 
			[StatusID] = @StatusID,
			[StudentEmail] = @StudentEmail, 
			[DateOfCheckout] = @DateOfCheckout, 
			[DateToReturn] = @DateToReturn
		WHERE [BookID] = @BookID
			AND [ISBN] = @ISBN
			AND [Edition] = @Edition
			AND	[Title] = @OldTitle
			AND	[EditionYear] = @OldEditionYear
			AND	[Description] = @OldDescription
			AND [CategoryID] = @OldCategoryID
			AND	[AuthorID] = @OldAuthorID
			AND	[LibraryID] = @OldLibraryID
			AND	[StatusID] = @OldStatusID
			
	
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Inserting sp_update_inheld_book'
GO
CREATE PROCEDURE [dbo].[sp_update_inheld_book]
	(
	@BookID            [int],
	@ISBN              [nvarchar](20),
    @Edition		   [nvarchar](20),
    @Title		       [nvarchar](100),
    @EditionYear       [int],
    @Description       [nvarchar](255),
	@CategoryID        [nvarchar](50),
	@AuthorID          [int],
    @LibraryID	       [nvarchar](50),
    @StatusID      [nvarchar](30),
    @StudentEmail         [nvarchar](100),
    @DateOfCheckout    dateTime, 
    @DateToReturn      dateTime,
	
	@OldISBN              [nvarchar](20),
    @OldEdition		   [nvarchar](20),
    @OldTitle		       [nvarchar](100),
    @OldEditionYear       [int],
    @OldDescription       [nvarchar](255),
	@OldCategoryID        [nvarchar](50),
	@OldAuthorID          [int],
    @OldLibraryID	       [nvarchar](50),
    @OldStatusID      [nvarchar](30)
    
	)
AS
	BEGIN
	UPDATE Book 
        SET [ISBN] = @ISBN, 
			[Edition] = @Edition, 
			[Title] = @Title, 
			[EditionYear] = @EditionYear, 
			[Description] = @Description, 
			[CategoryID] = @CategoryID, 
			[AuthorID] = @AuthorID, 
			[LibraryID] = @LibraryID, 
			[StatusID] = @StatusID,
			[StudentEmail] = @StudentEmail, 
			[DateOfCheckout] = @DateOfCheckout, 
			[DateToReturn] = @DateToReturn
		WHERE [BookID] = @BookID
			AND [ISBN] = @ISBN
			AND [Edition] = @Edition
			AND	[Title] = @OldTitle
			AND	[EditionYear] = @OldEditionYear
			AND	[Description] = @OldDescription
			AND [CategoryID] = @OldCategoryID
			AND	[AuthorID] = @OldAuthorID
			AND	[LibraryID] = @OldLibraryID
			AND	[StatusID] = @OldStatusID
			
	
		RETURN @@ROWCOUNT
	END
GO



print '' print '*** Inserting sp_update_available_book'
GO
CREATE PROCEDURE [dbo].[sp_update_available_book]
(
	@BookID            [int],
	@ISBN              [nvarchar](20),
    @Edition		   [nvarchar](20),
    @Title		       [nvarchar](100),
    @EditionYear       [int],
    @Description       [nvarchar](255),
	@CategoryID        [nvarchar](50),
	@AuthorID          [int],
    @LibraryID	       [nvarchar](50),
    @StatusID      [nvarchar](30),
    @StudentEmail         [nvarchar](100),
    @DateOfCheckout    dateTime, 
    @DateToReturn      dateTime,
	
	@OldISBN              [nvarchar](20),
    @OldEdition		   [nvarchar](20),
    @OldTitle		       [nvarchar](100),
    @OldEditionYear       [int],
    @OldDescription       [nvarchar](255),
	@OldCategoryID        [nvarchar](50),
	@OldAuthorID          [int],
    @OldLibraryID	       [nvarchar](50),
    @OldStatusID      [nvarchar](30)
    
	)
AS
	BEGIN
	UPDATE Book 
        SET [ISBN] = @ISBN, 
			[Edition] = @Edition, 
			[Title] = @Title, 
			[EditionYear] = @EditionYear, 
			[Description] = @Description, 
			[CategoryID] = @CategoryID, 
			[AuthorID] = @AuthorID, 
			[LibraryID] = @LibraryID, 
			[StatusID] = @StatusID,
			[StudentEmail] = @StudentEmail, 
			[DateOfCheckout] = @DateOfCheckout, 
			[DateToReturn] = @DateToReturn
		WHERE [BookID] = @BookID
			AND [ISBN] = @ISBN
			AND [Edition] = @Edition
			AND	[Title] = @OldTitle
			AND	[EditionYear] = @OldEditionYear
			AND	[Description] = @OldDescription
			AND [CategoryID] = @OldCategoryID
			AND	[AuthorID] = @OldAuthorID
			AND	[LibraryID] = @OldLibraryID
			AND	[StatusID] = @OldStatusID
			
	
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_deactivate_book'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_book]
	(
	@BookID			int
	)
AS
	BEGIN
		UPDATE [Book]
			SET [Active] = 0
			WHERE [BookID] = @BookID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_book'
GO
CREATE PROCEDURE [dbo].[sp_delete_book]
	(
	@BookID			int
	)
AS
	BEGIN
		DELETE 
		FROM [Book]
		WHERE [BookID] = @BookID
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_deactivate_student'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_student]
	(
	@StudentID			[nvarchar](20)
	)
AS
	BEGIN
		UPDATE [Student]
			SET [Active] = 0
			WHERE [StudentID] = @StudentID
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Adding sp_select_author_list'
GO
CREATE PROCEDURE [dbo].[sp_select_author_list]
	
AS
	BEGIN
		SELECT 	[AuthorID], [FirstName], [LastName], [Description]
		FROM 	[Author]
	END
GO	