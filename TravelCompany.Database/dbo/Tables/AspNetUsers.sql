CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                   NVARCHAR (450)     NOT NULL,
    [UserName]             NVARCHAR (256)     NULL,
    [NormalizedUserName]   NVARCHAR (256)     NULL,
    [Email]                NVARCHAR (256)     NULL,
    [NormalizedEmail]      NVARCHAR (256)     NULL,
    [EmailConfirmed]       BIT                NOT NULL,
    [PasswordHash]         NVARCHAR (MAX)     NULL,
    [SecurityStamp]        NVARCHAR (MAX)     NULL,
    [ConcurrencyStamp]     NVARCHAR (MAX)     NULL,
    [PhoneNumber]          NVARCHAR (MAX)     NULL,
    [PhoneNumberConfirmed] BIT                NOT NULL,
    [TwoFactorEnabled]     BIT                NOT NULL,
    [LockoutEnd]           DATETIMEOFFSET (7) NULL,
    [LockoutEnabled]       BIT                NOT NULL,
    [AccessFailedCount]    INT                NOT NULL,
    [CreatedById]          NVARCHAR (MAX)     NULL,
    [CreatedOn]            DATETIME2 (7)      DEFAULT ('0001-01-01T00:00:00.0000000') NOT NULL,
    [FullName]             NVARCHAR (MAX)     DEFAULT (N'') NOT NULL,
    [IsDeleted]            BIT                DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [LastUpdatedById]      NVARCHAR (MAX)     NULL,
    [LastUpdatedOn]        DATETIME2 (7)      NULL,
    [StationId]            INT                DEFAULT ((0)) NOT NULL,
    [StationName]          NVARCHAR (30)      NULL,
    [BirthDate]            DATETIME2 (7)      NOT NULL,
    [IdNumber]             NVARCHAR (MAX)     NOT NULL,
    [Gender]               INT                DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AspNetUsers_Stations_StationId] FOREIGN KEY ([StationId]) REFERENCES [dbo].[Stations] ([StationId])
);


GO
CREATE NONCLUSTERED INDEX [EmailIndex]
    ON [dbo].[AspNetUsers]([NormalizedEmail] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[AspNetUsers]([NormalizedUserName] ASC) WHERE ([NormalizedUserName] IS NOT NULL);


GO
CREATE NONCLUSTERED INDEX [IX_AspNetUsers_StationId]
    ON [dbo].[AspNetUsers]([StationId] ASC);

