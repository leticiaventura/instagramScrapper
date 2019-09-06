CREATE TABLE [dbo].[TBVenue_TBCategory]
(	
	[id]  INT NOT NULL PRIMARY KEY IDENTITY,
	[venue_id] INT NOT NULL, 
    [category_id] INT NOT NULL,
	CONSTRAINT [FK_TBVenue_TBcatogorie] FOREIGN KEY ([venue_id]) REFERENCES [TBVenue]([id]),
	CONSTRAINT [FK_TBCategory_TBVenue] FOREIGN KEY ([category_id]) REFERENCES [TBCategory]([id])
)
