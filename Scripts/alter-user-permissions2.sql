USE [Infera]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [infera_app]
GO
USE [Infera]
GO
ALTER ROLE [db_owner] ADD MEMBER [infera_app]
GO
USE [Infera]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [infera_app]
GO
