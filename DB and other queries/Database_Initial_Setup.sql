--CREATE DATABASE SyncLib
--GO

USE [SyncLib]
GO

CREATE TABLE dbo.ApplicationUserRole	(	Id			INT PRIMARY KEY,
											RoleName	VARCHAR(50) NOT NULL)
GO


CREATE TABLE dbo.BookCategory		(	Id				INT IDENTITY PRIMARY KEY,
										Category		VARCHAR(50),
										CreatedBy		INT NOT NULL,
										CreatedOn		DATETIME NOT NULL,
										UpdatedBy		INT NOT NULL,
										UpdatedOn		DATETIME NOT NULL,
										IsDeleted		BIT NOT NULL)
GO

CREATE TABLE dbo.ScriptLanguage		(	Id				INT IDENTITY PRIMARY KEY,
										LanguageName	VARCHAR(15) NOT NULL,
										CreatedBy		INT NOT NULL,
										CreatedOn		DATETIME NOT NULL,
										UpdatedBy		INT NOT NULL,
										UpdatedOn		DATETIME NOT NULL,
										IsDeleted		BIT NOT NULL)
GO

CREATE TABLE dbo.ApplicationUser	(	Id				INT	IDENTITY PRIMARY KEY,
										Username		VARCHAR(100) NOT NULL,
										UserPassword	VARCHAR(15) NOT NULL,
										FirstName		VARCHAR(50) NOT NULL,
										LastName		VARCHAR(50) NOT NULL,
										UserRoleId		INT NOT NULL,
										CreatedBy		INT NOT NULL,
										CreatedOn		DATETIME NOT NULL,
										UpdatedBy		INT NOT NULL,
										UpdatedOn		DATETIME NOT NULL,
										IsDeleted		BIT NOT NULL,
										CONSTRAINT FK_User_Role FOREIGN KEY (Id) REFERENCES ApplicationUserRole(Id))

GO

CREATE TABLE dbo.Author		(	Id						INT IDENTITY PRIMARY KEY,
								AuthorName				VARCHAR(100) NOT NULL,
								CreatedBy				INT NOT NULL,
								CreatedOn				DATETIME NOT NULL,
								UpdatedBy				INT NOT NULL,
								UpdatedOn				DATETIME NOT NULL,
								IsDeleted				BIT NOT NULL)
GO


CREATE TABLE dbo.Book		(	Id				INT IDENTITY PRIMARY KEY,
								BookName		VARCHAR(100) NOT NULL,
								AuthorId		INT NOT NULL,
								CategoryId		INT NOT NULL,
								LanguageId		INT NOT NULL,
								PublishedOn		CHAR(4) NULL,
								Edition			INT NOT NULL,
								CreatedBy		INT NOT NULL,
								CreatedOn		DATETIME NOT NULL,
								UpdatedBy		INT NOT NULL,
								UpdatedOn		DATETIME NOT NULL,
								IsDeleted		BIT NOT NULL,
								CONSTRAINT FK_Book_Author FOREIGN KEY (AuthorId) REFERENCES Author(Id),
								CONSTRAINT FK_Book_ScriptLanguage FOREIGN KEY (LanguageId) REFERENCES ScriptLanguage(Id),
								CONSTRAINT FK_Book_BookCategory FOREIGN KEY (CategoryId) REFERENCES BookCategory(Id))
GO


CREATE TABLE dbo.BookImage	(	Id						INT IDENTITY PRIMARY KEY,
								BookId					INT NOT NULL,
								BookImage				IMAGE NOT NULL,
								CreatedBy				INT NOT NULL,
								CreatedOn				DATETIME NOT NULL,
								UpdatedBy				INT NOT NULL,
								UpdatedOn				DATETIME NOT NULL,
								IsDeleted				BIT NOT NULL,
								CONSTRAINT FK_BookImage_Book FOREIGN KEY (BookId) REFERENCES Book(Id))


GO
CREATE TABLE dbo.BookLog	(	Id				INT IDENTITY PRIMARY KEY NOT NULL,
								BookId			INT NOT NULL,
								IssuedOn		DATE NOT NULL,
								IssueTo			INT NOT NULL,
								RecievedOn		DATE NULL,
								CreatedBy		INT NOT NULL,
								CreatedOn		DATETIME NOT NULL,
								UpdatedBy		INT NOT NULL,
								UpdatedOn		DATETIME NOT NULL,
								IsDeleted		BIT NOT NULL,
								CONSTRAINT FK_BookLog_Book FOREIGN KEY (BookId) REFERENCES Book(Id),
								CONSTRAINT FK_BookLog_User FOREIGN KEY (IssueTo) REFERENCES ApplicationUser(Id))



INSERT INTO dbo.ApplicationUserRole VALUES (1,'Admin')
INSERT INTO dbo.ApplicationUserRole VALUES (2,'Librarian')
INSERT INTO dbo.ApplicationUserRole VALUES (3,'Visitor')
GO

INSERT INTO dbo.BookCategory VALUES ('Literature',1,GETDATE(),1,GETDATE(),0)
INSERT INTO dbo.BookCategory VALUES ('Poetry',1,GETDATE(),1,GETDATE(),0)
INSERT INTO dbo.BookCategory VALUES ('Novel',1,GETDATE(),1,GETDATE(),0)
INSERT INTO dbo.BookCategory VALUES ('Hostory',1,GETDATE(),1,GETDATE(),0)
GO

INSERT INTO dbo.ApplicationUser VALUES ('admin@gmail.com','pass@123','SP','Admin', 1,1,GETDATE(),1,GETDATE(),0)
GO

INSERT INTO dbo.Author VALUES ('Asrar Ahmed Narvi',1,GETDATE(),1,GETDATE(),0)
GO

INSERT INTO dbo.ScriptLanguage VALUES ('Urdu',1,GETDATE(),1,GETDATE(),0)
INSERT INTO dbo.ScriptLanguage VALUES ('Hindi',1,GETDATE(),1,GETDATE(),0)
INSERT INTO dbo.ScriptLanguage VALUES ('English',1,GETDATE(),1,GETDATE(),0)
GO


INSERT INTO dbo.Book VALUES ('Sugar Bank',1,3,1,'1968',1,1,GETDATE(),1,GETDATE(),0)