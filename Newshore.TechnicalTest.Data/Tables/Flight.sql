CREATE TABLE [dbo].[Flight]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Origin] VARCHAR(4) NULL, 
    [Destination] VARCHAR(4) NULL, 
    [Price] FLOAT NULL, 
    [TransportId] INT NULL, 
    CONSTRAINT [FK_Flight_Transport] FOREIGN KEY ([TransportId]) REFERENCES [Transport]([Id])
)
