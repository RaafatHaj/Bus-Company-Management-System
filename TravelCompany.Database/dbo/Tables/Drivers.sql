CREATE TABLE [dbo].[Drivers] (
    [DriverId]      INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]     NVARCHAR (50) NOT NULL,
    [LastName]      NVARCHAR (50) NOT NULL,
    [LicenseNumber] NVARCHAR (50) NOT NULL,
    [LicenseExpiry] DATE          NOT NULL,
    [HireDate]      DATE          NOT NULL,
    [IsActive]      BIT           NOT NULL,
    [PhoneNumber]   NVARCHAR (20) NOT NULL,
    [Email]         NVARCHAR (50) NULL,
    [StationId]     INT           NOT NULL,
    CONSTRAINT [PK_Drivers] PRIMARY KEY CLUSTERED ([DriverId] ASC),
    CONSTRAINT [FK_Drivers_Stations_StationId] FOREIGN KEY ([StationId]) REFERENCES [dbo].[Stations] ([StationId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Drivers_StationId]
    ON [dbo].[Drivers]([StationId] ASC);

