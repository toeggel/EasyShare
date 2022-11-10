USE [<DatabaseName>]	

-- Comply with ANSI standard.
SET ANSI_DEFAULTS ON;  
-- Allow creating and changing indexes on computed columns and indexed views.
SET ARITHABORT ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET NUMERIC_ROUNDABORT OFF;

-- Allow SNAPSHOT isolation level and make READ COMMITTED SNAPSHOT the default.
ALTER DATABASE [<DatabaseName>] SET ALLOW_SNAPSHOT_ISOLATION ON;
ALTER DATABASE [<DatabaseName>] SET READ_COMMITTED_SNAPSHOT ON;

