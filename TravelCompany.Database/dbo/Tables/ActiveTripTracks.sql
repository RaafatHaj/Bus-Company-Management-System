CREATE TABLE [dbo].[ActiveTripTracks] (
    [TripId]                   INT            NOT NULL,
    [StationOrder]             INT            NOT NULL,
    [StationId]                INT            NOT NULL,
    [StationName]              NVARCHAR (200) NOT NULL,
    [Status]                   INT            NOT NULL,
    [PreviousStation]          NVARCHAR (200) NULL,
    [NexttStation]             NVARCHAR (200) NULL,
    [PlannedArrivalDateTime]   DATETIME2 (7)  NOT NULL,
    [PlannedDepartureDateTime] DATETIME2 (7)  NOT NULL,
    [ActualArrivalDateTime]    DATETIME2 (7)  NOT NULL,
    [ActualDepartureDateTime]  DATETIME2 (7)  NOT NULL,
    [TripStatus]               INT            NOT NULL,
    [RouteId]                  INT            NOT NULL,
    [RouteName]                NVARCHAR (200) NOT NULL,
    [EstimatedDistance]        INT            NOT NULL,
    CONSTRAINT [PK_ActiveTripTracks] PRIMARY KEY CLUSTERED ([TripId] ASC, [StationOrder] ASC),
    CONSTRAINT [FK_ActiveTripTracks_Routes_RouteId] FOREIGN KEY ([RouteId]) REFERENCES [dbo].[Routes] ([RouteId]),
    CONSTRAINT [FK_ActiveTripTracks_Trips_TripId] FOREIGN KEY ([TripId]) REFERENCES [dbo].[Trips] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ActiveTripTracks_RouteId]
    ON [dbo].[ActiveTripTracks]([RouteId] ASC);

