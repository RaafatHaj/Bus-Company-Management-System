CREATE TABLE [dbo].[Stations] (
    [StationId]   INT             IDENTITY (1, 1) NOT NULL,
    [StationName] NVARCHAR (MAX)  NOT NULL,
    [Latitude]    DECIMAL (18, 4) NOT NULL,
    [Longitude]   DECIMAL (18, 4) NOT NULL,
    CONSTRAINT [PK_Stations] PRIMARY KEY CLUSTERED ([StationId] ASC)
);

