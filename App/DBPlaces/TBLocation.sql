CREATE TABLE [dbo].[TBLocation]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [crosstreet] VARCHAR(MAX) NULL, 
    [city] VARCHAR(MAX) NULL,
	[state] VARCHAR(MAX) NULL,
	[postalcode] VARCHAR(MAX) NULL,
	[country] VARCHAR(MAX) NULL,
	[lat] VARCHAR(MAX) NOT NULL,
	[lng] VARCHAR(MAX) NOT NULL, 
    [venue_id] INT NOT NULL,
	CONSTRAINT [FK_TBLocation_TBVenue] FOREIGN KEY ([venue_id]) REFERENCES [TBVenue]([id])
)
