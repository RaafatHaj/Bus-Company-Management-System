CREATE TYPE [dbo].[IdDateType] AS TABLE (
    [Id]   INT           NOT NULL,
    [Date] DATETIME2 (7) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC));

