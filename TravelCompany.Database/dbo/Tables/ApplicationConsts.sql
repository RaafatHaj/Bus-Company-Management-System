CREATE TABLE [dbo].[ApplicationConsts] (
    [Name]  NVARCHAR (450) NOT NULL,
    [Value] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_ApplicationConsts] PRIMARY KEY CLUSTERED ([Name] ASC)
);

