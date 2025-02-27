--- Db.Create

CREATE DATABASE test

--- Db.Clean

DROP DATABASE IF EXISTS test WITH (force)

--- Schema.Describe

SELECT tablename
FROM pg_tables
WHERE schemaname = 'public'

UNION

SELECT indexname
FROM pg_indexes
WHERE schemaname = 'public'

ORDER BY 1

--- _Migration.EnsureTable

CREATE TABLE IF NOT EXISTS _migration (
    id varchar(255) NOT NULL,
    CONSTRAINT pk__migration PRIMARY KEY (id)
)

--- _Migration.GetAll

SELECT id
FROM _migration
ORDER BY id

--- _Migration.Create

INSERT INTO _migration (id)
VALUES (@Id)

--- _MigrationCustom.EnsureTable

CREATE TABLE IF NOT EXISTS _migration (
    id varchar(255) NOT NULL,
    timestamp timestamp with time zone NOT NULL,
    CONSTRAINT pk__migration PRIMARY KEY (id)
)

--- _MigrationCustom.GetAll

SELECT *
FROM _migration
ORDER BY id

--- _MigrationCustom.Create

INSERT INTO _migration (id, timestamp)
VALUES (@Id, @Timestamp)

--- Migration.0001_Init

CREATE TABLE app_user (
    id varchar(36) NOT NULL,
    first_name varchar(255) NOT NULL,
    last_name varchar(255) NOT NULL,
    CONSTRAINT pk_app_user PRIMARY KEY (id)
)

--- Migration.0002_AddIndexLastName

CREATE INDEX ix_app_user_last_name 
ON app_user (last_name)

--- Migration.0003_AddIndexFirstName

CREATE INDEX ix_app_user_first_name 
ON app_user (first_name)

--- MigrationIdempotent.0001_Init

CREATE TABLE IF NOT EXISTS app_user (
    id varchar(36) NOT NULL,
    first_name varchar(255) NOT NULL,
    last_name varchar(255) NOT NULL,
    CONSTRAINT pk_app_user PRIMARY KEY (id)
)

--- MigrationIdempotent.0002_AddIndexLastName

CREATE INDEX IF NOT EXISTS ix_app_user_last_name 
ON app_user (last_name)

--- MigrationIdempotent.0003_AddIndexFirstName

CREATE INDEX IF NOT EXISTS ix_app_user_first_name
ON app_user (first_name)

--- MigrationError.0001_Init

CREATE TABLE app_user (
    id varchar(36) NOT NULL,
    first_name varchar(255) NOT NULL,
    last_name varchar(255) NOT NULL,
    CONSTRAINT pk_app_user PRIMARY KEY (id)
)

--- MigrationError.0002_AddIndexLastName

CREATE INDEX IF NOT EXISTS ix_app_user_last_name 
ON app_user (last_name)

--- MigrationError.0003_AddIndexFirstName

CREATE NDEX IF NOT EXISTS ix_app_user_first_name
ON app_user (first_name)
