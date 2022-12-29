CREATE TABLE [dbo].[Table]
(
	[ID] INT NOT NULL PRIMARY KEY, 
    [Name] NCHAR(10) NOT NULL, 
    [Email] NCHAR(10) NOT NULL, 
    [Phone] NCHAR(10) NOT NULL, 
    [Address] NVARCHAR(MAX) NOT NULL
)
