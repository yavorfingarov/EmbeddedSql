--- Migration.User.0001_Init

CREATE TABLE AppUser (
    Id TEXT NOT NULL,
    FirstName TEXT NOT NULL,
    LastName TEXT NOT NULL,
    CONSTRAINT PK_AppUser PRIMARY KEY (Id)
)

--- User.GetAll

SELECT Id, FirstName, LastName
FROM AppUser

--- User.Get

SELECT Id, FirstName, LastName
FROM AppUser
WHERE Id = @Id

--- User.Search

SELECT Id, FirstName, LastName
FROM AppUser
WHERE {0}

--- User.Add

INSERT INTO AppUser (Id, FirstName, LastName)
VALUES (@Id, @FirstName, @LastName)

--- User.Update

UPDATE AppUser
SET FirstName = @FirstName, 
    LastName = @LastName
WHERE Id = @Id

--- User.Delete

DELETE FROM AppUser
WHERE Id = @Id
