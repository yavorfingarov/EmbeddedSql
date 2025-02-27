--- Db.Create

CREATE DATABASE [test]

--- Db.Clean

IF DB_ID('test') IS NOT NULL
BEGIN
    ALTER DATABASE [test] 
    SET SINGLE_USER 
    WITH ROLLBACK IMMEDIATE;

    DROP DATABASE [test];
END

--- Schema.Describe

SELECT [name]
FROM [sys].[tables]
WHERE OBJECT_SCHEMA_NAME(object_id) != 'sys'

UNION

SELECT [name]
FROM [sys].[indexes]
WHERE OBJECT_SCHEMA_NAME(object_id) != 'sys'

ORDER BY 1

--- _Migration.EnsureTable

IF OBJECT_ID(N'[_Migration]') IS NULL
BEGIN
    CREATE TABLE [_Migration] (
        [Id] nvarchar(255) NOT NULL,
        CONSTRAINT [PK__Migration] PRIMARY KEY ([Id])
    )
END

--- _Migration.GetAll

SELECT [Id]
FROM [_Migration]
ORDER BY [Id]

--- _Migration.Create

INSERT INTO [_Migration] ([Id])
VALUES (@Id)

--- _MigrationCustom.EnsureTable

IF OBJECT_ID(N'[_Migration]') IS NULL
BEGIN
    CREATE TABLE [_Migration] (
        [Id] nvarchar(255) NOT NULL,
        [Timestamp] datetime2 NOT NULL,
        CONSTRAINT [PK__Migration] PRIMARY KEY ([Id])
    )
END

--- _MigrationCustom.GetAll

SELECT *
FROM [_Migration]
ORDER BY [Id]

--- _MigrationCustom.Create

INSERT INTO [_Migration] ([Id], [Timestamp])
VALUES (@Id, @Timestamp)

--- Migration.0001_Init

CREATE TABLE [User] (
    [Id] nvarchar(36) NOT NULL,
    [FirstName] nvarchar(255) NOT NULL,
    [LastName] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id])
)

--- Migration.0002_AddIndexLastName

CREATE INDEX [IX_User_LastName]
ON [User] ([LastName])

--- Migration.0003_AddIndexFirstName

CREATE INDEX [IX_User_FirstName]
ON [User] ([FirstName])

--- MigrationIdempotent.0001_Init

IF OBJECT_ID(N'[User]') IS NULL
BEGIN
    CREATE TABLE [User] (
        [Id] nvarchar(36) NOT NULL,
        [FirstName] nvarchar(255) NOT NULL,
        [LastName] nvarchar(255) NOT NULL,
        CONSTRAINT [PK_User] PRIMARY KEY ([Id])
    )
END

--- MigrationIdempotent.0002_AddIndexLastName

IF NOT EXISTS (SELECT 1 FROM [sys].[indexes] WHERE [name] = 'IX_User_LastName')
BEGIN
    CREATE INDEX [IX_User_LastName]
    ON [User] ([LastName])
END

--- MigrationIdempotent.0003_AddIndexFirstName

IF NOT EXISTS (SELECT 1 FROM [sys].[indexes] WHERE [name] = 'IX_User_FirstName')
BEGIN
    CREATE INDEX [IX_User_FirstName]
    ON [User] ([FirstName])
END

--- MigrationError.0001_Init

CREATE TABLE [User] (
    [Id] nvarchar(36) NOT NULL,
    [FirstName] nvarchar(255) NOT NULL,
    [LastName] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id])
)

--- MigrationError.0002_AddIndexLastName

CREATE INDEX [IX_User_LastName]
ON [User] ([LastName])

--- MigrationError.0003_AddIndexFirstName

CREATE NDEX [IX_User_FirstName]
ON [User] ([FirstName])
