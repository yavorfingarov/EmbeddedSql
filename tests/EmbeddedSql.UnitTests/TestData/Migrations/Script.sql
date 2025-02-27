SELECT * FROM user

-- And also

SELECT *
FROM user u
JOIN city c ON c.id = u.city_id
