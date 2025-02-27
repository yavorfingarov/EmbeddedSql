--- _Migration.EnsureTable

CREATE TABLE IF NOT EXISTS _Migration (
    Id TEXT NOT NULL,
    CONSTRAINT PK__Migration PRIMARY KEY (Id)
)

--- _Migration.GetAll

SELECT Id
FROM _Migration

--- _Migration.Create

INSERT INTO _Migration (Id)
VALUES (@Id)
