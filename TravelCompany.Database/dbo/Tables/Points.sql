CREATE TABLE [dbo].[Points] (
    [PointId]           INT IDENTITY (1, 1) NOT NULL,
    [StationId]         INT NOT NULL,
    [PreviousStationId] INT NOT NULL,
    [DistanceValue]     INT NOT NULL,
    [TimeValue]         INT NOT NULL,
    CONSTRAINT [PK_Points] PRIMARY KEY CLUSTERED ([PointId] ASC),
    CONSTRAINT [FK_PreviousStationId] FOREIGN KEY ([PreviousStationId]) REFERENCES [dbo].[Stations] ([StationId]),
    CONSTRAINT [FK_StationId] FOREIGN KEY ([StationId]) REFERENCES [dbo].[Stations] ([StationId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Points_PreviousStationId]
    ON [dbo].[Points]([PreviousStationId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Points_StationId]
    ON [dbo].[Points]([StationId] ASC);

