CREATE TABLE [dbo].[Journey]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Origin] VARCHAR(4) NULL, 
    [Destination] VARCHAR(4) NULL, 
    [Price] FLOAT NULL, 
    [IsDirectFlight] BIT NULL, 
    [IsRoundTripFlight] BIT NULL
)
