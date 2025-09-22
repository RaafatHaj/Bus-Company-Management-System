CREATE TABLE [dbo].[Vehicles] (
    [VehicleId]     INT            IDENTITY (1, 1) NOT NULL,
    [StationId]     INT            NOT NULL,
    [IsActive]      BIT            DEFAULT (CONVERT([bit],(1))) NOT NULL,
    [Seats]         INT            NOT NULL,
    [Type]          NVARCHAR (100) NOT NULL,
    [VehicleNumber] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Vehicles] PRIMARY KEY CLUSTERED ([VehicleId] ASC),
    CONSTRAINT [FK_Vehicles_Stations_StationId] FOREIGN KEY ([StationId]) REFERENCES [dbo].[Stations] ([StationId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Vehicles_StationId]
    ON [dbo].[Vehicles]([StationId] ASC);

