CREATE TABLE [dbo].[TripAssignments] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [TripId]             INT           NOT NULL,
    [VehicleId]          INT           NOT NULL,
    [DriverId]           INT           NULL,
    [StopedStationId]    INT           NOT NULL,
    [EndDateAndTime]     DATETIME2 (7) NOT NULL,
    [StartDateAndTime]   DATETIME2 (7) NOT NULL,
    [DepartureStationId] INT           NULL,
    CONSTRAINT [PK_TripAssignments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TripAssignments_Drivers_DriverId] FOREIGN KEY ([DriverId]) REFERENCES [dbo].[Drivers] ([DriverId]),
    CONSTRAINT [FK_TripAssignments_Stations_StopedStationId] FOREIGN KEY ([StopedStationId]) REFERENCES [dbo].[Stations] ([StationId]),
    CONSTRAINT [FK_TripAssignments_Trips_TripId] FOREIGN KEY ([TripId]) REFERENCES [dbo].[Trips] ([Id]),
    CONSTRAINT [FK_TripAssignments_Vehicles_VehicleId] FOREIGN KEY ([VehicleId]) REFERENCES [dbo].[Vehicles] ([VehicleId])
);


GO
CREATE NONCLUSTERED INDEX [IX_TripAssignments_DriverId]
    ON [dbo].[TripAssignments]([DriverId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_TripAssignments_TripId]
    ON [dbo].[TripAssignments]([TripId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TripAssignments_VehicleId]
    ON [dbo].[TripAssignments]([VehicleId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TripAssignments_StopedStationId]
    ON [dbo].[TripAssignments]([StopedStationId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TripAssignments_Vehicle_EndDate]
    ON [dbo].[TripAssignments]([VehicleId] ASC, [EndDateAndTime] ASC)
    INCLUDE([StartDateAndTime], [StopedStationId]);


GO
CREATE NONCLUSTERED INDEX [IX_TripAssignments_EndDate]
    ON [dbo].[TripAssignments]([EndDateAndTime] ASC)
    INCLUDE([StartDateAndTime], [VehicleId], [StopedStationId]);

