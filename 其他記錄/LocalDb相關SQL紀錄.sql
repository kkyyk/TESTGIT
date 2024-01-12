CREATE TABLE [dbo].[Persona] (
    [Id]   INT           NOT NULL IDENTITY,
    [Name] NVARCHAR (50) NOT NULL,
    [Age] NCHAR(10) NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

--Age型態轉成INT
ALTER TABLE [dbo].[Persona] ALTER COLUMN [Age] INT NOT NULL;