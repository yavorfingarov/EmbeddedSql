--- Migration.0004_AddCountry

CREATE TABLE [Country] (
    [Id] nvarchar(36) NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY ([Id])
)

--- MigrationIdempotent.0004_AddCountry

IF OBJECT_ID(N'[Country]') IS NULL
BEGIN
    CREATE TABLE [Country] (
        [Id] nvarchar(36) NOT NULL,
        [Name] nvarchar(255) NOT NULL,
        CONSTRAINT [PK_Country] PRIMARY KEY ([Id])
    )
END
