CREATE TABLE [dbo].[Reservations] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [TripId]                INT            NOT NULL,
    [StationAId]            INT            NOT NULL,
    [StationBId]            INT            NOT NULL,
    [SeatNumber]            INT            NOT NULL,
    [SeatCode]              BIGINT         NOT NULL,
    [PersonId]              NVARCHAR (MAX) NOT NULL,
    [PersonName]            NVARCHAR (MAX) NOT NULL,
    [PersonPhone]           NVARCHAR (MAX) NOT NULL,
    [PersonEmail]           NVARCHAR (50)  NULL,
    [PersonGender]          INT            NOT NULL,
    [CreatedById]           NVARCHAR (450) NULL,
    [CreatedOn]             DATETIME2 (7)  DEFAULT ('0001-01-01T00:00:00.0000000') NOT NULL,
    [IsDeleted]             BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [LastUpdatedById]       NVARCHAR (450) NULL,
    [LastUpdatedOn]         DATETIME2 (7)  NULL,
    [TripDepartureDateTime] DATETIME2 (7)  DEFAULT ('0001-01-01T00:00:00.0000000') NOT NULL,
    CONSTRAINT [PK_Reservations] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Reservations_AspNetUsers_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Reservations_AspNetUsers_LastUpdatedById] FOREIGN KEY ([LastUpdatedById]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Reservations_Stations_StationAId] FOREIGN KEY ([StationAId]) REFERENCES [dbo].[Stations] ([StationId]),
    CONSTRAINT [FK_Reservations_Stations_StationBId] FOREIGN KEY ([StationBId]) REFERENCES [dbo].[Stations] ([StationId]),
    CONSTRAINT [FK_Reservations_Trips_TripId] FOREIGN KEY ([TripId]) REFERENCES [dbo].[Trips] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Reservations_TripId]
    ON [dbo].[Reservations]([TripId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Reservations_StationAId]
    ON [dbo].[Reservations]([StationAId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Reservations_StationBId]
    ON [dbo].[Reservations]([StationBId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Reservations_CreatedById]
    ON [dbo].[Reservations]([CreatedById] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Reservations_LastUpdatedById]
    ON [dbo].[Reservations]([LastUpdatedById] ASC);

