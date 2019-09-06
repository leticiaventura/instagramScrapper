CREATE TABLE [dbo].[TBVenue_Similarity]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY,
	[venue_1] INT NOT NULL, 
    [venue_2] INT NOT NULL, 
	[similarity] DECIMAL(6,5) NOT NULL,
    [distance] DECIMAL(18, 13) NOT NULL, 
    CONSTRAINT [FK_TBVenue_1] FOREIGN KEY ([venue_1]) REFERENCES [TBVenue]([id]),
    CONSTRAINT [FK_TBVenue_2] FOREIGN KEY ([venue_2]) REFERENCES [TBVenue]([id])
)
