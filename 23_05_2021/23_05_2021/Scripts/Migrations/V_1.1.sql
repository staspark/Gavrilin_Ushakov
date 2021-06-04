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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523223341_test_24')
BEGIN
    CREATE TABLE [Authors] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Authors] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523223341_test_24')
BEGIN
    CREATE TABLE [Clients] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        CONSTRAINT [PK_Clients] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523223341_test_24')
BEGIN
    CREATE TABLE [Orders] (
        [Id] int NOT NULL IDENTITY,
        [Date] datetime2 NOT NULL,
        [ClientId] int NULL,
        [Sum] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Orders_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523223341_test_24')
BEGIN
    CREATE TABLE [Books] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(50) NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        [Mark2] nvarchar(max) NULL,
        [OrderId] int NULL,
        [mark] nvarchar(max) NULL,
        CONSTRAINT [PK_Books] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Books_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523223341_test_24')
BEGIN
    CREATE TABLE [AuthorBook] (
        [AuthorsId] int NOT NULL,
        [BooksId] int NOT NULL,
        CONSTRAINT [PK_AuthorBook] PRIMARY KEY ([AuthorsId], [BooksId]),
        CONSTRAINT [FK_AuthorBook_Authors_AuthorsId] FOREIGN KEY ([AuthorsId]) REFERENCES [Authors] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AuthorBook_Books_BooksId] FOREIGN KEY ([BooksId]) REFERENCES [Books] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523223341_test_24')
BEGIN
    CREATE INDEX [IX_AuthorBook_BooksId] ON [AuthorBook] ([BooksId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523223341_test_24')
BEGIN
    CREATE INDEX [IX_Books_OrderId] ON [Books] ([OrderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523223341_test_24')
BEGIN
    CREATE INDEX [IX_Orders_ClientId] ON [Orders] ([ClientId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210523223341_test_24')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210523223341_test_24', N'5.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525220736_test_26')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210525220736_test_26', N'5.0.6');
END;
GO

COMMIT;
GO

