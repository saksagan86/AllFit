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

CREATE TABLE [Aanbod] (
    [id] int NOT NULL IDENTITY,
    [naam] nvarchar(max) NOT NULL,
    [image] nvarchar(max) NOT NULL,
    [extraBegeleiding] bit NOT NULL,
    [beschrijvingBegeleiding] nvarchar(max) NOT NULL,
    [Discriminator] nvarchar(13) NOT NULL,
    [niveau] nvarchar(max) NULL,
    [duur] nvarchar(max) NULL,
    [doelgroep] nvarchar(max) NULL,
    CONSTRAINT [PK_Aanbod] PRIMARY KEY ([id])
);
GO

CREATE TABLE [ContactFormulieren] (
    [id] int NOT NULL IDENTITY,
    [titel] nvarchar(max) NOT NULL,
    [beschrijving] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_ContactFormulieren] PRIMARY KEY ([id])
);
GO

CREATE TABLE [Faciliteiten] (
    [id] int NOT NULL IDENTITY,
    [naam] nvarchar(max) NOT NULL,
    [beschrijving] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Faciliteiten] PRIMARY KEY ([id])
);
GO

CREATE TABLE [Gebruiker] (
    [id] int NOT NULL IDENTITY,
    [naam] nvarchar(max) NOT NULL,
    [email] nvarchar(max) NOT NULL,
    [wacthwoord] nvarchar(max) NOT NULL,
    [telefoonnummer] nvarchar(max) NOT NULL,
    [Discriminator] nvarchar(13) NOT NULL,
    [geboortedatum] nvarchar(max) NULL,
    [isActief] bit NULL,
    CONSTRAINT [PK_Gebruiker] PRIMARY KEY ([id])
);
GO

CREATE TABLE [Sportscholen] (
    [id] int NOT NULL IDENTITY,
    [naam] nvarchar(max) NOT NULL,
    [adres] nvarchar(max) NOT NULL,
    [stad] nvarchar(max) NOT NULL,
    [openingstijden] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Sportscholen] PRIMARY KEY ([id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260421021313_DBOpzet', N'8.0.26');
GO

COMMIT;
GO

