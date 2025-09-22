CREATE TABLE [dbo].[Routes] (
    [RouteId]           INT            IDENTITY (1, 1) NOT NULL,
    [RouteName]         NVARCHAR (MAX) NOT NULL,
    [FirstStationId]    INT            NOT NULL,
    [LastStationId]     INT            NOT NULL,
    [StationsNumber]    INT            NULL,
    [ReverseRouteId]    INT            NULL,
    [EstimatedTime]     INT            NOT NULL,
    [EstimatedDistance] INT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Routes] PRIMARY KEY CLUSTERED ([RouteId] ASC),
    CONSTRAINT [FK_Routes_Stations_FirstStationId] FOREIGN KEY ([FirstStationId]) REFERENCES [dbo].[Stations] ([StationId]),
    CONSTRAINT [FK_Routes_Stations_LastStationId] FOREIGN KEY ([LastStationId]) REFERENCES [dbo].[Stations] ([StationId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Routes_FirstStationId]
    ON [dbo].[Routes]([FirstStationId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Routes_LastStationId]
    ON [dbo].[Routes]([LastStationId] ASC);

