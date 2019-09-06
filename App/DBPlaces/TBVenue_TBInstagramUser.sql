CREATE TABLE [dbo].[TBVenue_TBInstagramUser]
(	
	[id]  INT NOT NULL PRIMARY KEY IDENTITY,
	[venue_id] INT NOT NULL, 
    [instragramuser_id] INT NOT NULL,
	CONSTRAINT [FK_TBVenue_TBInstragramUser] FOREIGN KEY ([venue_id]) REFERENCES [TBVenue]([id]),
	CONSTRAINT [FK_TBInstragramUser_TBVenue] FOREIGN KEY ([instragramuser_id]) REFERENCES [TBInstagramUser]([id])
)
