CREATE TABLE [dbo].[Trips] (
    [Id]                      INT      IDENTITY (1, 1) NOT NULL,
    [Date]                    DATE     NOT NULL,
    [Time]                    TIME (7) NOT NULL,
    [status]                  INT      NOT NULL,
    [RouteId]                 INT      NOT NULL,
    [Seats]                   INT      NULL,
    [HasBookedSeat]           BIT      NOT NULL,
    [MainTripId]              INT      NULL,
    [ReturnTripId]            INT      NULL,
    [IsVehicleMoving]         BIT      DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [HasBreak]                BIT      DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [StationOrderNextToBreak] INT      NULL,
    [StationStopMinutes]      INT      DEFAULT ((10)) NOT NULL,
    [BreakMinutes]            INT      NULL,
    [ArrivedStationOrder]     INT      DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Trips] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Trips_Routes_RouteId] FOREIGN KEY ([RouteId]) REFERENCES [dbo].[Routes] ([RouteId])
);




GO
CREATE NONCLUSTERED INDEX [IX_Trips_RouteId]
    ON [dbo].[Trips]([RouteId] ASC);

