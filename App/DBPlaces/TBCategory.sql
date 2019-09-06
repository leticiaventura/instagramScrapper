CREATE TABLE [dbo].[TBCategory]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [name] VARCHAR(MAX) NOT NULL, 
    [foursquare_id] VARCHAR(MAX) NOT NULL
)
