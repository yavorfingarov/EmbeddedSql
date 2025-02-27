--- Migration.0004_AddCountry

CREATE TABLE Country (
    Id TEXT NOT NULL,
    Name TEXT NOT NULL,
    CONSTRAINT PK_Country PRIMARY KEY (Id)
)

--- MigrationIdempotent.0004_AddCountry

CREATE TABLE IF NOT EXISTS Country (
    Id TEXT NOT NULL,
    Name TEXT NOT NULL,
    CONSTRAINT PK_Country PRIMARY KEY (Id)
)
