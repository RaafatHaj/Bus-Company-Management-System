CREATE TABLE [dbo].[TripPatterns] (
    [Id]                    INT             IDENTITY (1, 1) NOT NULL,
    [RouteId]               INT             NOT NULL,
    [Time]                  TIME (7)        NOT NULL,
    [PatternType]           INT             NULL,
    [Percentage]            DECIMAL (18, 2) NULL,
    [OccupiedWeekDaysCode]  INT             NULL,
    [StartDate]             DATE            NULL,
    [EndDate]               DATE            NULL,
    [TripsNumber]           INT             NULL,
    [UnassignedTripsNumber] INT             NULL,
    [EmptyWeeksNumber]      INT             NOT NULL,
    [OccupiedWeeksNumber]   INT             NOT NULL,
    CONSTRAINT [PK_TripPatterns] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TripPatterns_Routes_RouteId] FOREIGN KEY ([RouteId]) REFERENCES [dbo].[Routes] ([RouteId])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_TripPatterns_RouteId_Time]
    ON [dbo].[TripPatterns]([RouteId] ASC, [Time] ASC);

