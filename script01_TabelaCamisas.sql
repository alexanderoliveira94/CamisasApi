IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Camisas] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NULL,
    [Valor] decimal(18,2) NOT NULL,
    [Tamanho] nvarchar(max) NULL,
    [Classe] int NOT NULL,
    CONSTRAINT [PK_Camisas] PRIMARY KEY ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Classe', N'Nome', N'Tamanho', N'Valor') AND [object_id] = OBJECT_ID(N'[Camisas]'))
    SET IDENTITY_INSERT [Camisas] ON;
INSERT INTO [Camisas] ([Id], [Classe], [Nome], [Tamanho], [Valor])
VALUES (1, 1, N'Corinthians', N'GG', 500.0),
(2, 2, N'Palmeiras', N'P', 50.0),
(3, 0, N'Vasco', N'G', 150.0),
(4, 1, N'São Paulo', N'M', 250.0),
(5, 2, N'Santos', N'GG', 70.0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Classe', N'Nome', N'Tamanho', N'Valor') AND [object_id] = OBJECT_ID(N'[Camisas]'))
    SET IDENTITY_INSERT [Camisas] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230614234057_InitialCreate', N'7.0.7');
GO

COMMIT;
GO

