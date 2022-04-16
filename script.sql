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

CREATE TABLE [Folders] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [ParentFolderId] bigint NULL,
    [IsActive] bit NOT NULL,
    [CreationDate] datetime2 NOT NULL,
    [LastModifiedDate] datetime2 NULL,
    CONSTRAINT [PK_Folders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Folders_Folders_ParentFolderId] FOREIGN KEY ([ParentFolderId]) REFERENCES [Folders] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Files] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [FolderId] bigint NOT NULL,
    [IsActive] bit NOT NULL,
    [CreationDate] datetime2 NOT NULL,
    [LastModifiedDate] datetime2 NULL,
    CONSTRAINT [PK_Files] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Files_Folders_FolderId] FOREIGN KEY ([FolderId]) REFERENCES [Folders] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Files_FolderId] ON [Files] ([FolderId]);
GO

CREATE INDEX [IX_Folders_ParentFolderId] ON [Folders] ([ParentFolderId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220411215457_InitialMigration', N'6.0.3');
GO

COMMIT;
GO

