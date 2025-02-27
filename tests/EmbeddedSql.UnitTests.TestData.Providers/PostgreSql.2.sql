--- Migration.0004_AddCountry

CREATE TABLE country (
    id varchar(36) NOT NULL,
    name varchar(255) NOT NULL,
    CONSTRAINT pk_country PRIMARY KEY (id)
)

--- MigrationIdempotent.0004_AddCountry

CREATE TABLE IF NOT EXISTS country (
    id varchar(36) NOT NULL,
    name varchar(255) NOT NULL,
    CONSTRAINT pk_country PRIMARY KEY (id)
)
