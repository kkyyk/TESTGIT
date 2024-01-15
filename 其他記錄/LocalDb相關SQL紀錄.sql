CREATE TABLE [dbo].[Persona] (
    [Id]   INT           NOT NULL IDENTITY,
    [Name] NVARCHAR (50) NOT NULL,
    [Age] NCHAR(10) NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

--Age型態轉成INT
ALTER TABLE [dbo].[Persona] ALTER COLUMN [Age] INT NOT NULL;


ALTER TABLE [dbo].[Persona] ADD  [Type] NCHAR(5) NULL;

CREATE TABLE [dbo].[PersonaDetail] (
    [Id]   INT           NOT NULL IDENTITY,
    [PersonaId] INT NOT NULL,
    [Comment] NVARCHAR(10) NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Type] (
    [Id]   INT     NOT NULL IDENTITY,
    [Type] NCHAR(5) NOT NULL,
    [TypeName] NVARCHAR(10) NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

INSERT INTO [dbo].[Type] ([Type], [TypeName]) VALUES ('A', '進攻型');
INSERT INTO [dbo].[Type] ([Type], [TypeName]) VALUES ('D', '防守型');
INSERT INTO [dbo].[Type] ([Type], [TypeName]) VALUES ('P', '全能型');

INSERT INTO [dbo].[Type] ([Type], [TypeName]) VALUES ('S', N'輔助型');

UPDATE [dbo].[Type] SET [TypeName] = N'進攻型' WHERE [Type] = 'A'
UPDATE [dbo].[Type] SET [TypeName] = N'防守型' WHERE [Type] = 'D'
UPDATE [dbo].[Type] SET [TypeName] = N'全能型' WHERE [Type] = 'P'