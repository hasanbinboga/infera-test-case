USE [master]
GO
CREATE LOGIN [infera_app] WITH PASSWORD=N'123', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
use [master];
GO
USE [Infera]
GO
CREATE USER [infera_app] FOR LOGIN [infera_app]
GO