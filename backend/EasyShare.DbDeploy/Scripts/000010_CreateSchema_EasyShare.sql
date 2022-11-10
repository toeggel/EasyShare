PRINT N'Creating schemas'
GO
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'ES' )
    EXEC('CREATE SCHEMA [ES] AUTHORIZATION [dbo]');
GO