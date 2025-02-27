# EmbeddedSql

[![nuget](https://img.shields.io/nuget/v/EmbeddedSql)](https://www.nuget.org/packages/EmbeddedSql)
[![downloads](https://img.shields.io/nuget/dt/EmbeddedSql?color=blue)](https://www.nuget.org/stats/packages/EmbeddedSql?groupby=Version)
[![build](https://img.shields.io/github/actions/workflow/status/yavorfingarov/EmbeddedSql/cd.yml?branch=master)](https://github.com/yavorfingarov/EmbeddedSql/actions/workflows/cd.yml?query=branch%3Amaster)
[![loc](https://img.shields.io/endpoint?url=https://gist.githubusercontent.com/yavorfingarov/479024692dc528451f53707bff2b443a/raw/lines-of-code.json)](https://github.com/yavorfingarov/EmbeddedSql/actions/workflows/cd.yml?query=branch%3Amaster)
[![maintainability](https://img.shields.io/endpoint?url=https://gist.githubusercontent.com/yavorfingarov/479024692dc528451f53707bff2b443a/raw/maintainability.json)](https://github.com/yavorfingarov/EmbeddedSql/actions/workflows/cd.yml?query=branch%3Amaster)
[![test coverage](https://img.shields.io/endpoint?url=https://gist.githubusercontent.com/yavorfingarov/479024692dc528451f53707bff2b443a/raw/test-coverage.json)](https://github.com/yavorfingarov/EmbeddedSql/actions/workflows/cd.yml?query=branch%3Amaster)
[![mutation score](https://img.shields.io/endpoint?url=https://gist.githubusercontent.com/yavorfingarov/479024692dc528451f53707bff2b443a/raw/mutation-score.json)](https://github.com/yavorfingarov/EmbeddedSql/actions/workflows/cd.yml?query=branch%3Amaster)
[![openssf scorecard](https://img.shields.io/ossf-scorecard/github.com/yavorfingarov/EmbeddedSql?label=openssf+scorecard)](https://scorecard.dev/viewer/?uri=github.com/yavorfingarov/EmbeddedSql&sort_by=risk-level&sort_direction=desc)

EmbeddedSql is a NuGet package that provides a clean and organized way to manage SQL statements and database migrations using `.sql` files. It works on top of `System.Data` abstractions, making it database provider and ORM agnostic.

## Rationale

Having SQL statements as plain strings scattered around your codebase is not only ugly but error-prone as well. When all of them are organized in `.sql` files, you can have a better overview, reduce the cognitive overhead of mixed languages, employ proper code style, and have nice syntax highlighting.

## Setup

* Install the [EmbeddedSql NuGet package](https://www.nuget.org/packages/EmbeddedSql).

* Make sure all `.sql` files are going to be embedded in the assembly:

```xml
<ItemGroup>
  <EmbeddedResource Include="**/*.sql" />
</ItemGroup>
```

* Add EmbeddedSql services:

```csharp
builder.Services.AddEmbeddedSql();
```

* Alternatively, you can also point specific assemblies for scanning and apply filename-based filtering:

```csharp
builder.Services.AddEmbeddedSql(options => 
{
    options.Assemblies = new[] { typeof(TestApi.Common.Entry).Assembly };
    options.UseFilter(resourceName => !resourceName.Contains("Scripts"));
});
```

## SQL statements

A special type of comment with three dashes `---` denotes a key for a SQL statement.

> [!NOTE]
> All code samples use [Dapper](https://github.com/DapperLib/Dapper) and [SQLite](https://www.sqlite.org/index.html).

```sql
--- AppUser.Get

SELECT Id, FirstName, LastName
FROM AppUser
WHERE Id = @Id
```

On application startup, a singleton service `ISql` is registered in the DI container. All embedded `.sql` resources are scanned and stored internally in a [FrozenDictionary](https://learn.microsoft.com/en-us/dotnet/api/system.collections.frozen.frozendictionary-2?view=net-9.0) for better performance.

> [!NOTE]
> The name of the files containing SQL statements doesn't matter.

Getting SQL statements is then straightforward:

```csharp
app.MapGet("/api/users/{id}", (string id, IDbConnection db, ISql sql) =>
{
    var user = db.QuerySingleOrDefault<User>(sql["AppUser.Get"], new { Id = id });
    if (user == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(user);
});
```

### Unsafe format

The `UnsafeFormat` overloads offer a great way to construct SQL statements dynamically.

Consider this statement:

```sql
--- AppUser.Search

SELECT Id, FirstName, LastName
FROM AppUser
WHERE {0}
```

You can use it like that:

```csharp
app.MapGet("/api/users/search", (string? firstName, string? lastName, IDbConnection db, ISql sql) =>
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
    var query = sql.UnsafeFormat("AppUser.Search", condition);
    var users = db.Query<User>(query, parameters);

    return Results.Ok(users);
});
```

> [!WARNING]
> Never pass non-validated user-provided values into this method. Doing so may expose your application to SQL injection attacks.

## Migrations

EmbeddedSql offers a simple way to handle your database migrations.

```csharp
private static void MigrateDb(this WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var migrator = scope.ServiceProvider.GetRequiredService<IMigrator>();
    migrator.Run();
}

// OR

private static async Task MigrateDb(this WebApplication app)
{
    await using var scope = app.Services.CreateAsyncScope();
    var migrator = scope.ServiceProvider.GetRequiredService<IMigrator>();
    await migrator.RunAsync();
}

```

A scoped `IMigrator` service is registered by default. Without further customization, it will create a table for tracking all applied migrations. You should provide its definition and the statements to work with it. Furthermore, the migrator will expect to find an `IDbConnection` in the DI container.

```sql
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
```

> [!NOTE]
> The script for ensuring the migration tracking table is run every time, so it should be indempotent.

By default, all statements that are prefixed with `Migration.` are considered migration scripts.

```sql
--- Migration.AppUser.0001_Init

CREATE TABLE AppUser (
    Id TEXT NOT NULL,
    FirstName TEXT NOT NULL,
    LastName TEXT NOT NULL,
    CONSTRAINT PK_AppUser PRIMARY KEY (Id)
)
```

Migration scripts are going to be applied in alphabetical order.

### Customization

You can customize many aspects of the behavior of the migrator.

```csharp
builder.Services.AddEmbeddedSql(options => 
{
    options.ConfigureMigrator(migratorOptions =>
    {
        // ...
    });
});
```

#### Idempotent scripts

By default, the migrator will expect non-idempotent scripts. You can change this:

```csharp
migratorOptions.Idempotent = true;
```

This would mean that no migration tracking table will be created.

#### Transaction behavior

By default, every script is wrapped in a transaction. Instead, you can also wrap all migrations in a single transaction:

```csharp
migratorOptions.TransactionBehavior = TransactionBehavior.Overarching;
```

Alternatively, you can disable transactions altogether:

```csharp
migratorOptions.TransactionBehavior = TransactionBehavior.None;
```

> [!WARNING]
> Please make sure you know how DDL statements are handled by your db.
>
> Currently, the only check that the migrator does is whether your provider is MySql/MariaDb and logs a warning if you are using transactions due to [implicit commit](https://dev.mysql.com/doc/refman/5.7/en/implicit-commit.html).

#### Script naming

If the default naming conventions don't fit well in your codebase, you can change them:

```csharp
migratorOptions.EnsureMigrationTableCommand = "_MyMigration.EnsureTable";
migratorOptions.GetMigrationsQuery = "_MyMigration.GetAll";
migratorOptions.CreateMigrationCommand = "_MyMigration.Create";
migratorOptions.MigrationScriptPrefix = "MyMigration.";
```

Also, since the migration tracking table is defined by you, you can pimp it up too:

```sql
--- _MyMigration.EnsureTable

CREATE TABLE IF NOT EXISTS _Migration (
    Id TEXT NOT NULL,
    Timestamp TEXT NOT NULL,
    CONSTRAINT PK__Migration PRIMARY KEY (Id)
)

--- _MyMigration.Create

INSERT INTO _Migration (Id, Timestamp)
VALUES (@Id, @Timestamp)
```

Then you should change the parameters for the create migration command accordingly.

```csharp
migratorOptions.ConfigureCreateMigrationCommandParameters((parameters, migration) =>
{
    parameters["@Id"] = migration;
    parameters["@Timestamp"] = DateTime.UtcNow;
});
```

## Keyed services

You can set a service key for `ISql` and `IMigrator` when registering:

```csharp
builder.Services.AddEmbeddedSql(options => 
{
    options.ServiceKey = "AppUsers";
});
```

The migrator will first try to resolve the `IDbConnection` with the same key, and if such is not found, it will fall back to the keyless one. This way you can have many sets of SQL statements, migrators, and database connections.

## Additional resources

* [API reference](https://github.com/yavorfingarov/EmbeddedSql/blob/master/docs/EmbeddedSql.md)

* [Sample app](https://github.com/yavorfingarov/EmbeddedSql/tree/master/samples/EmbeddedSql.SampleApi)

* [Changelog](https://github.com/yavorfingarov/EmbeddedSql/blob/master/CHANGELOG.md)

* [License](https://github.com/yavorfingarov/EmbeddedSql/blob/master/LICENSE)

## Feedback

* [Issue tracker](https://github.com/yavorfingarov/EmbeddedSql/issues)

* [Discussions](https://github.com/yavorfingarov/EmbeddedSql/discussions)
