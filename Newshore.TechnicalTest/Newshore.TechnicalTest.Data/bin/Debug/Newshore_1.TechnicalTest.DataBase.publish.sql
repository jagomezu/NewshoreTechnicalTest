/*
Script de implementación para Newshore.TechnicalTest.Database

Una herramienta generó este código.
Los cambios realizados en este archivo podrían generar un comportamiento incorrecto y se perderán si
se vuelve a generar el código.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Newshore.TechnicalTest.Database"
:setvar DefaultFilePrefix "Newshore.TechnicalTest.Database"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detectar el modo SQLCMD y deshabilitar la ejecución del script si no se admite el modo SQLCMD.
Para volver a habilitar el script después de habilitar el modo SQLCMD, ejecute lo siguiente:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'El modo SQLCMD debe estar habilitado para ejecutar correctamente este script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
Se está quitando la columna [dbo].[JourneyFlight].[flight_id]; puede que se pierdan datos.

Se está quitando la columna [dbo].[JourneyFlight].[journey_id]; puede que se pierdan datos.
*/

IF EXISTS (select top 1 1 from [dbo].[JourneyFlight])
    RAISERROR (N'Se detectaron filas. La actualización del esquema va a terminar debido a una posible pérdida de datos.', 16, 127) WITH NOWAIT

GO
PRINT N'La siguiente operación se generó a partir de un archivo de registro de refactorización 2ce206ee-77f4-4bbe-ab93-945ad25ca584';

PRINT N'Cambiar el nombre de [dbo].[Flight].[transport_id] a transportId';


GO
EXECUTE sp_rename @objname = N'[dbo].[Flight].[transport_id]', @newname = N'transportId', @objtype = N'COLUMN';


GO
PRINT N'Quitando Clave externa [dbo].[FK_JourneyFlight_Flight]...';


GO
ALTER TABLE [dbo].[JourneyFlight] DROP CONSTRAINT [FK_JourneyFlight_Flight];


GO
PRINT N'Quitando Clave externa [dbo].[FK_JourneyFlight_Journey]...';


GO
ALTER TABLE [dbo].[JourneyFlight] DROP CONSTRAINT [FK_JourneyFlight_Journey];


GO
PRINT N'Modificando Tabla [dbo].[JourneyFlight]...';


GO
ALTER TABLE [dbo].[JourneyFlight] DROP COLUMN [flight_id], COLUMN [journey_id];


GO
ALTER TABLE [dbo].[JourneyFlight]
    ADD [journeyId] INT NULL,
        [flightId]  INT NULL;


GO
PRINT N'Creando Clave externa [dbo].[FK_JourneyFlight_Flight]...';


GO
ALTER TABLE [dbo].[JourneyFlight] WITH NOCHECK
    ADD CONSTRAINT [FK_JourneyFlight_Flight] FOREIGN KEY ([flightId]) REFERENCES [dbo].[Flight] ([id]);


GO
PRINT N'Creando Clave externa [dbo].[FK_JourneyFlight_Journey]...';


GO
ALTER TABLE [dbo].[JourneyFlight] WITH NOCHECK
    ADD CONSTRAINT [FK_JourneyFlight_Journey] FOREIGN KEY ([journeyId]) REFERENCES [dbo].[Journey] ([id]);


GO
-- Paso de refactorización para actualizar el servidor de destino con los registros de transacciones implementadas
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '2ce206ee-77f4-4bbe-ab93-945ad25ca584')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('2ce206ee-77f4-4bbe-ab93-945ad25ca584')

GO

GO
PRINT N'Comprobando los datos existentes con las restricciones recién creadas';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[JourneyFlight] WITH CHECK CHECK CONSTRAINT [FK_JourneyFlight_Flight];

ALTER TABLE [dbo].[JourneyFlight] WITH CHECK CHECK CONSTRAINT [FK_JourneyFlight_Journey];


GO
PRINT N'Actualización completada.';


GO
