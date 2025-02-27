



--- SingleLine

SELECT * FROM user

--- Multiline

SELECT *
FROM user u
JOIN city c ON c.id = u.city_id

--- WithComment

SELECT *
-- Check this
FROM user

--- StartWithComment

-- Check this
SELECT *
FROM user

--- EndWithComment

SELECT *
FROM user
-- Check this

---		Whitespace		



		 SELECT *
FROM user		

--- Other characters _ 560932/ | ???@

SELECT * FROM user

--- 1Arg

SELECT * 
FROM user
WHERE {0}

--- 2Args

SELECT * 
FROM user
WHERE {0}
AND {1}
AND {0}

--- 3Args

SELECT * 
FROM user
WHERE {0}
AND {1}
AND {2}
AND {0}

--- 5Args

SELECT * 
FROM user
WHERE {0}
AND {1}
AND {2}
AND {3}
AND {4}
AND {0}

--- 6Args

SELECT * 
FROM user
WHERE {0}
AND {1}
AND {2}
AND {3}
AND {4}
AND {5}
AND {0}