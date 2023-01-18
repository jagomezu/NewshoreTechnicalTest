CREATE TABLE [dbo].[Transport]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FlightCarrier] VARCHAR(100) NULL, 
    [FlightNumber] VARCHAR(50) NULL
)
