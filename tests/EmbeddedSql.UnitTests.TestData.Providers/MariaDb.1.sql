--- Db.Clean

DROP DATABASE IF EXISTS mariadb;

CREATE DATABASE mariadb;

--- Schema.Describe

SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_SCHEMA = 'mariadb'

UNION

SELECT Index_Name
FROM INFORMATION_SCHEMA.STATISTICS
WHERE INDEX_NAME != 'PRIMARY'

ORDER BY 1

--- _Migration.EnsureTable

CREATE TABLE IF NOT EXISTS _Migration (
    Id VARCHAR(255) NOT NULL,
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
    Id VARCHAR(255) NOT NULL,
    Timestamp DATETIME NOT NULL,
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
    Id VARCHAR(36) NOT NULL,
    FirstName VARCHAR(255) NOT NULL,
    LastName VARCHAR(255) NOT NULL,
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
    Id VARCHAR(36) NOT NULL,
    FirstName VARCHAR(255) NOT NULL,
    LastName VARCHAR(255) NOT NULL,
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
    Id VARCHAR(36) NOT NULL,
    FirstName VARCHAR(255) NOT NULL,
    LastName VARCHAR(255) NOT NULL,
    CONSTRAINT PK_User PRIMARY KEY (Id)
)

--- MigrationError.0002_AddIndexLastName

CREATE INDEX IX_User_LastName 
ON User (LastName)

--- MigrationError.0003_AddIndexFirstName

CREATE NDEX IX_User_FirstName 
ON User (FirstName)
