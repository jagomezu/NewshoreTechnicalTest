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
PRINT N'La operación de refactorización Cambiar nombre con la clave 5df679e6-a8cf-45b5-9da5-1de6b2525e6b se ha omitido; no se cambiará el nombre del elemento [dbo].[Transport].[id] (SqlSimpleColumn) a Id';


GO
PRINT N'La operación de refactorización Cambiar nombre con la clave 9ae2f854-1a3a-4e52-83a0-8a3b2f4c6751 se ha omitido; no se cambiará el nombre del elemento [dbo].[Transport].[flightCarrier] (SqlSimpleColumn) a FlightCarrier';


GO
PRINT N'La operación de refactorización Cambiar nombre con la clave 524755a3-63bb-4f6a-b7f6-b50c12cf4a80 se ha omitido; no se cambiará el nombre del elemento [dbo].[Transport].[flightNumber] (SqlSimpleColumn) a FlightNumber';


GO
PRINT N'La operación de refactorización Cambiar nombre con la clave 3e2fabb3-1c26-4d29-97d2-20c8f29abce1 se ha omitido; no se cambiará el nombre del elemento [dbo].[Flight].[id] (SqlSimpleColumn) a Id';


GO
PRINT N'La operación de refactorización Cambiar nombre con la clave 7c4fad86-c1a6-49a7-9ee8-a925ab1e94f8 se ha omitido; no se cambiará el nombre del elemento [dbo].[Flight].[origin] (SqlSimpleColumn) a Origin';


GO
PRINT N'La operación de refactorización Cambiar nombre con la clave 4c5d238b-710b-43ba-9621-372bcedaaa84 se ha omitido; no se cambiará el nombre del elemento [dbo].[Flight].[destination] (SqlSimpleColumn) a Destination';


GO
PRINT N'La operación de refactorización Cambiar nombre con la clave 991cde78-9ac0-4af7-911f-7c8e9ad2d71e se ha omitido; no se cambiará el nombre del elemento [dbo].[Flight].[price] (SqlSimpleColumn) a Price';


GO
PRINT N'La operación de refactorización Cambiar nombre con la clave 905774ba-4f3c-4a53-959c-26aeb8b1610e se ha omitido; no se cambiará el nombre del elemento [dbo].[Flight].[transportId] (SqlSimpleColumn) a TransportId';


GO
PRINT N'La operación de refactorización Cambiar nombre con la clave 4b837f7e-2d2e-4f75-9858-52e4458e990b se ha omitido; no se cambiará el nombre del elemento [dbo].[Journey].[id] (SqlSimpleColumn) a Id';


GO
PRINT N'La operación de refactorización Cambiar nombre con la clave 46ed305a-0318-4323-b2a8-126f4db7d23e se ha omitido; no se cambiará el nombre del elemento [dbo].[Journey].[origin] (SqlSimpleColumn) a Origin';


GO
PRINT N'La operación de refactorización Cambiar nombre con la clave 86e62e32-ba8f-4061-bf9a-1c325e7bb8ca se ha omitido; no se cambiará el nombre del elemento [dbo].[Journey].[destination] (SqlSimpleColumn) a Destination';


GO
PRINT N'La operación de refactorización Cambiar nombre con la clave 9c9cc48f-3553-4cb1-9795-f3cc34e55504 se ha omitido; no se cambiará el nombre del elemento [dbo].[Journey].[price] (SqlSimpleColumn) a Price';


GO
PRINT N'La operación de refactorización Cambiar nombre con la clave 29d70806-27a0-4805-90d0-a1cf2f11ac89 se ha omitido; no se cambiará el nombre del elemento [dbo].[Journey].[isDirectFlight] (SqlSimpleColumn) a IsDirectFlight';


GO
PRINT N'La operación de refactorización Cambiar nombre con la clave 28728642-1fd5-4c93-b29f-faf843b74315 se ha omitido; no se cambiará el nombre del elemento [dbo].[Journey].[isRoundTripFlight] (SqlSimpleColumn) a IsRoundTripFlight';


GO
PRINT N'La operación de refactorización Cambiar nombre con la clave 584f4ac4-aa7c-40cc-9590-ef16d81f3e9f se ha omitido; no se cambiará el nombre del elemento [dbo].[JourneyFlight].[id] (SqlSimpleColumn) a Id';


GO
PRINT N'La operación de refactorización Cambiar nombre con la clave ea2941dc-818e-4f79-a29e-36e4555234e7 se ha omitido; no se cambiará el nombre del elemento [dbo].[JourneyFlight].[journeyId] (SqlSimpleColumn) a JourneyId';


GO
PRINT N'La operación de refactorización Cambiar nombre con la clave 1f54900e-8a89-4a4b-a2f3-1f14d87296bb se ha omitido; no se cambiará el nombre del elemento [dbo].[JourneyFlight].[flightId] (SqlSimpleColumn) a FlightId';


GO
-- Paso de refactorización para actualizar el servidor de destino con los registros de transacciones implementadas
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '5df679e6-a8cf-45b5-9da5-1de6b2525e6b')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('5df679e6-a8cf-45b5-9da5-1de6b2525e6b')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '9ae2f854-1a3a-4e52-83a0-8a3b2f4c6751')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('9ae2f854-1a3a-4e52-83a0-8a3b2f4c6751')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '524755a3-63bb-4f6a-b7f6-b50c12cf4a80')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('524755a3-63bb-4f6a-b7f6-b50c12cf4a80')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '3e2fabb3-1c26-4d29-97d2-20c8f29abce1')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('3e2fabb3-1c26-4d29-97d2-20c8f29abce1')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '7c4fad86-c1a6-49a7-9ee8-a925ab1e94f8')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('7c4fad86-c1a6-49a7-9ee8-a925ab1e94f8')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '4c5d238b-710b-43ba-9621-372bcedaaa84')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('4c5d238b-710b-43ba-9621-372bcedaaa84')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '991cde78-9ac0-4af7-911f-7c8e9ad2d71e')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('991cde78-9ac0-4af7-911f-7c8e9ad2d71e')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '905774ba-4f3c-4a53-959c-26aeb8b1610e')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('905774ba-4f3c-4a53-959c-26aeb8b1610e')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '4b837f7e-2d2e-4f75-9858-52e4458e990b')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('4b837f7e-2d2e-4f75-9858-52e4458e990b')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '46ed305a-0318-4323-b2a8-126f4db7d23e')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('46ed305a-0318-4323-b2a8-126f4db7d23e')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '86e62e32-ba8f-4061-bf9a-1c325e7bb8ca')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('86e62e32-ba8f-4061-bf9a-1c325e7bb8ca')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '9c9cc48f-3553-4cb1-9795-f3cc34e55504')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('9c9cc48f-3553-4cb1-9795-f3cc34e55504')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '29d70806-27a0-4805-90d0-a1cf2f11ac89')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('29d70806-27a0-4805-90d0-a1cf2f11ac89')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '28728642-1fd5-4c93-b29f-faf843b74315')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('28728642-1fd5-4c93-b29f-faf843b74315')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '584f4ac4-aa7c-40cc-9590-ef16d81f3e9f')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('584f4ac4-aa7c-40cc-9590-ef16d81f3e9f')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'ea2941dc-818e-4f79-a29e-36e4555234e7')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('ea2941dc-818e-4f79-a29e-36e4555234e7')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '1f54900e-8a89-4a4b-a2f3-1f14d87296bb')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('1f54900e-8a89-4a4b-a2f3-1f14d87296bb')

GO

GO
PRINT N'Actualización completada.';


GO
