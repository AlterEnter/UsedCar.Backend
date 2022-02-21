USE [UsedCarDb]
CREATE TABLE [UsedCar].[IdaasInfo] (
    [IdaasInfoNumber] BIGINT IDENTITY NOT NULL PRIMARY KEY,
    [CreatedAt] datetime2 NOT NULL DEFAULT GETUTCDATE(),
    [IdpUserId] NVARCHAR(36) NOT NULL,
    [DisplayName] NVARCHAR(30) NOT NULL,
    [MailAddress] NVARCHAR(254) NOT NULL,
    CONSTRAINT UQ_IdaasInfo_IdpUserId UNIQUE([IdpUserId]),
    CONSTRAINT UQ_IdaasInfo_MailAddress UNIQUE([MailAddress])
) ON [PRIMARY]
GO