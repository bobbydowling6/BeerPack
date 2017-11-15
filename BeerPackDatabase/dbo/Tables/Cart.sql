CREATE TABLE [dbo].[Cart]
(
[ID] uniqueidentifier DEFAULT newid(),
	[AspNetUserId] NVARCHAR (128) NULL,
	[DateCreated] DATETIME NOT NULL DEFAULT GetDate(),
	[DateLastModified] DATETIME NOT NULL DEFAULT GetDate(),
    CONSTRAINT [PK_Cart] PRIMARY KEY ([ID]) 
)
