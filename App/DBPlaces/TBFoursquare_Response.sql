CREATE TABLE [dbo].[TBFoursquare_Response]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [response] NVARCHAR(MAX) NOT NULL,
	CONSTRAINT [response record should be formatted as JSON]
                   CHECK (ISJSON(response)=1)
)
