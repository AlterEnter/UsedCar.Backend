USE [UsedCarDb]
GO
IF EXISTS (SELECT name FROM sys.schemas WHERE name = N'UsedCar')
    BEGIN
        PRINT 'DROP SCHEMA'
        DROP SCHEMA [UsedCar]
    END
GO

USE [UsedCarDb]
GO

CREATE SCHEMA [UsedCar]
GO