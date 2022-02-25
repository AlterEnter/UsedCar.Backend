USE [UsedCarDb]
CREATE TABLE [UsedCar].[User] (
    [UserNumber] BIGINT IDENTITY NOT NULL PRIMARY KEY,
    [CreatedAt] datetime2 NOT NULL DEFAULT GETUTCDATE(),
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [IdpUserId] NVARCHAR(36) NOT NULL,
    [FirstName] NVARCHAR(15) NOT NULL,
    [LastName] NVARCHAR(15) NOT NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [PhoneNumber] NVARCHAR(15)NOT NULL,
    [Zip] NVARCHAR(10) NOT NULL,
    [State] NVARCHAR(10) NOT NULL,
    [City] NVARCHAR(10) NOT NULL,
    [Street1] NVARCHAR(30) NOT NULL,
    [Street2] NVARCHAR(30),
    CONSTRAINT UQ_User_UserId UNIQUE([UserId]),
    CONSTRAINT FK_User_IdpUserId FOREIGN KEY ([IdpUserId]) REFERENCES [UsedCar].[IdaasInfo] ([IdpUserId])
) ON [PRIMARY]
GO