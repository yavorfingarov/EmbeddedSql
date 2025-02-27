namespace EmbeddedSql.SampleApi
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this WebApplication app)
        {
            app.MapGet("/api/users", GetAll);
            app.MapGet("/api/users/{id}", Get);
            app.MapGet("/api/users/search", Search);
            app.MapPost("/api/users", Create);
            app.MapPut("/api/users/{id}", Update);
            app.MapDelete("/api/users/{id}", Delete);
        }

        private static IEnumerable<User> GetAll(IDbConnection db, ISql sql)
        {
            var users = db.Query<User>(sql["User.GetAll"]);

            return users;
        }

        private static IResult Get(string id, IDbConnection db, ISql sql)
        {
            var user = db.QuerySingleOrDefault<User>(sql["User.Get"], new { Id = id });
            if (user == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(user);
        }

        private static IResult Search(string? firstName, string? lastName, IDbConnection db, ISql sql)
        {
            var parameters = new Dictionary<string, object>();
            var predicates = new List<string>();
            if (firstName != null)
            {
                parameters.Add("FirstName", firstName);
                predicates.Add("FirstName = @FirstName");
            }

            if (lastName != null)
            {
                parameters.Add("LastName", lastName);
                predicates.Add("LastName = @LastName");
            }

            if (parameters.Count == 0)
            {
                return Results.BadRequest();
            }

            var condition = string.Join(" AND ", predicates);
            var query = sql.UnsafeFormat("User.Search", condition);
            var users = db.Query<User>(query, parameters);

            return Results.Ok(users);
        }

        private static IResult Create(User user, IDbConnection db, ISql sql)
        {
            if (!user.IsValid())
            {
                return Results.BadRequest();
            }

            user.Id = Guid.NewGuid().ToString();
            db.Execute(sql["User.Add"], user);

            return Results.Created($"/api/users/{user.Id}", null);
        }

        private static IResult Update(string id, User user, IDbConnection db, ISql sql)
        {
            if (!user.IsValid())
            {
                return Results.BadRequest();
            }

            user.Id = id;
            var rows = db.Execute(sql["User.Update"], user);
            if (rows == 0)
            {
                return Results.NotFound();
            }

            return Results.Ok();
        }

        private static IResult Delete(string id, IDbConnection db, ISql sql)
        {
            var rows = db.Execute(sql["User.Delete"], new { Id = id });
            if (rows == 0)
            {
                return Results.NotFound();
            }

            return Results.Ok();
        }

        private static bool IsValid(this User user)
        {
            return !string.IsNullOrWhiteSpace(user.FirstName) && !string.IsNullOrWhiteSpace(user.LastName);
        }

        private sealed class User
        {
            public string Id { get; set; } = null!;
            public string FirstName { get; set; } = null!;
            public string LastName { get; set; } = null!;
        }
    }
}
