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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311042658_CriaDb')
BEGIN
    CREATE TABLE [JobVacancies] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Company] nvarchar(max) NOT NULL,
        [IsRemote] bit NOT NULL,
        [SalaryRange] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_JobVacancies] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311042658_CriaDb')
BEGIN
    CREATE TABLE [JobApplications] (
        [Id] int NOT NULL IDENTITY,
        [ApplicationName] nvarchar(max) NOT NULL,
        [ApplicationEmail] nvarchar(max) NOT NULL,
        [IdJobVacancy] int NOT NULL,
        CONSTRAINT [PK_JobApplications] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_JobApplications_JobVacancies_IdJobVacancy] FOREIGN KEY ([IdJobVacancy]) REFERENCES [JobVacancies] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311042658_CriaDb')
BEGIN
    CREATE INDEX [IX_JobApplications_IdJobVacancy] ON [JobApplications] ([IdJobVacancy]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311042658_CriaDb')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220311042658_CriaDb', N'6.0.3');
END;
GO

COMMIT;
GO

