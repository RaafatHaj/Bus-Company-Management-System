CREATE TABLE [dbo].[RoutePoints] (
    [RouteId]    INT NOT NULL,
    [PointId]    INT NOT NULL,
    [PointOrder] INT NOT NULL,
    [StationId]  INT NOT NULL,
    CONSTRAINT [PK_RoutePoints] PRIMARY KEY CLUSTERED ([RouteId] ASC, [PointId] ASC),
    CONSTRAINT [FK_RoutePoints_Points_PointId] FOREIGN KEY ([PointId]) REFERENCES [dbo].[Points] ([PointId]),
    CONSTRAINT [FK_RoutePoints_Routes_RouteId] FOREIGN KEY ([RouteId]) REFERENCES [dbo].[Routes] ([RouteId]),
    CONSTRAINT [FK_RoutePoints_Stations_StationId] FOREIGN KEY ([StationId]) REFERENCES [dbo].[Stations] ([StationId])
);


GO
CREATE NONCLUSTERED INDEX [IX_RoutePoints_PointId]
    ON [dbo].[RoutePoints]([PointId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_RoutePoints_StationId]
    ON [dbo].[RoutePoints]([StationId] ASC);

