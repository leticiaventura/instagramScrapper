CREATE TABLE [dbo].[TBInstagramUser]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [username] VARCHAR(MAX) NOT NULL, 
    [instagram_id] VARCHAR(MAX) NOT NULL
)
