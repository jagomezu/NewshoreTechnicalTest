CREATE TABLE [dbo].[JourneyFlight]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [JourneyId] INT NULL, 
    [FlightId] INT NULL, 
    CONSTRAINT [FK_JourneyFlight_Journey] FOREIGN KEY ([JourneyId]) REFERENCES [Journey]([Id]), 
    CONSTRAINT [FK_JourneyFlight_Flight] FOREIGN KEY ([FlightId]) REFERENCES [Flight]([Id])
)
