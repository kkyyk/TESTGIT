-- Persona表
CREATE TABLE [dbo].[Persona] (
    [Id] INT IDENTITY PRIMARY KEY,
    [Name] NVARCHAR(50) NOT NULL,
    [Age] INT NOT NULL,
    [Type] NCHAR(5) NULL
);

-- PersonaDetail表
CREATE TABLE [dbo].[PersonaDetail] (
    [Id] INT IDENTITY PRIMARY KEY,
    [PersonaId] INT NOT NULL,
    [Comment] NVARCHAR(10) NOT NULL,
    CONSTRAINT FK_PersonaDetail_Persona FOREIGN KEY (PersonaId) REFERENCES Persona(Id)
);

-- Type表
CREATE TABLE [dbo].[Type] (
    [Id] INT IDENTITY PRIMARY KEY,
    [Type] NCHAR(5) NOT NULL,
    [TypeName] NVARCHAR(10) NOT NULL
);

-- 初始Type資料
INSERT INTO [dbo].[Type] ([Type], [TypeName]) VALUES 
('A', N'進攻型'),
('D', N'防守型'),
('P', N'全能型'),
('S', N'輔助型');