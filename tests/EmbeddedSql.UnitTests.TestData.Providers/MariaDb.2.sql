--- Migration.0004_AddCountry

CREATE TABLE Country (
    Id VARCHAR(36) NOT NULL,
    Name VARCHAR(255) NOT NULL,
    CONSTRAINT PK_Country PRIMARY KEY (Id)
)

--- MigrationIdempotent.0004_AddCountry

CREATE TABLE IF NOT EXISTS Country (
    Id VARCHAR(36) NOT NULL,
    Name VARCHAR(255) NOT NULL,
    CONSTRAINT PK_Country PRIMARY KEY (Id)
)
