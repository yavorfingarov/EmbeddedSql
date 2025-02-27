--- Schema.Describe

SELECT name
FROM sqlite_schema
ORDER BY name

--- _Migration.EnsureTable

CREATE TABLE IF NOT EXISTS _Migration (
    Id TEXT NOT NULL,
    CONSTRAINT PK__Migration PRIMARY KEY (Id)
)

--- _Migration.GetAll

SELECT Id
FROM _Migration
ORDER BY Id

--- _Migration.Create

INSERT INTO _Migration (Id)
VALUES (@Id)

--- _MigrationCustom.EnsureTable

CREATE TABLE IF NOT EXISTS _Migration (
    Id TEXT NOT NULL,
    Timestamp TEXT NOT NULL,
    CONSTRAINT PK__Migration PRIMARY KEY (Id)
)

--- _MigrationCustom.GetAll

SELECT *
FROM _Migration
ORDER BY Id

--- _MigrationCustom.Create

INSERT INTO _Migration (Id, Timestamp)
VALUES (@Id, @Timestamp)

--- Migration.0001_Init

CREATE TABLE User (
    Id TEXT NOT NULL,
    FirstName TEXT NOT NULL,
    LastName TEXT NOT NULL,
    CONSTRAINT PK_User PRIMARY KEY (Id)
)

--- Migration.0002_AddIndexLastName

CREATE INDEX IX_User_LastName 
ON User (LastName)

--- Migration.0003_AddIndexFirstName

CREATE INDEX IX_User_FirstName 
ON User (FirstName)

--- MigrationIdempotent.0001_Init

CREATE TABLE IF NOT EXISTS User (
    Id TEXT NOT NULL,
    FirstName TEXT NOT NULL,
    LastName TEXT NOT NULL,
    CONSTRAINT PK_User PRIMARY KEY (Id)
)

--- MigrationIdempotent.0002_AddIndexLastName

CREATE INDEX IF NOT EXISTS IX_User_LastName 
ON User (LastName)

--- MigrationIdempotent.0003_AddIndexFirstName

CREATE INDEX IF NOT EXISTS IX_User_FirstName 
ON User (FirstName)

--- MigrationError.0001_Init

CREATE TABLE User (
    Id TEXT NOT NULL,
    FirstName TEXT NOT NULL,
    LastName TEXT NOT NULL,
    CONSTRAINT PK_User PRIMARY KEY (Id)
)

--- MigrationError.0002_AddIndexLastName

CREATE INDEX IX_User_LastName 
ON User (LastName)

--- MigrationError.0003_AddIndexFirstName

CREATE NDEX IX_User_FirstName 
ON User (FirstName)
