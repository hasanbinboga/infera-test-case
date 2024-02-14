USE [Infera]
GO
ALTER ROLE [db_datareader] ADD MEMBER [infera_app]
GO
USE [Infera]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [infera_app]
GO
USE [Infera]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [infera_app]
GO
