using Npgsql;
using Testcontainers.PostgreSql;

namespace EmbeddedSql.UnitTests.MigratorTests
{
    [Trait("Category", "Expensive")]
    public class PostgreSql : BaseMigratorTests, IClassFixture<PostgreSqlFixture>
    {
        public PostgreSql(PostgreSqlFixture fixture)
        {
            var adminConnectionString = fixture.Container.GetConnectionString();
            adminConnectionString = $"{adminConnectionString};Pooling=false;";
            AdminDb = new NpgsqlConnection(adminConnectionString);
            AdminDb.Execute(Sql["Db.Create"]);
            var testConnectionString = adminConnectionString.Replace($"Database={PostgreSqlBuilder.DefaultDatabase}", "Database=test");
            Db = new NpgsqlConnection(testConnectionString);
            Db.Open();
        }
    }

    public class PostgreSqlFixture : BaseDbFixture
    {
        public PostgreSqlFixture()
        {
            Container = new PostgreSqlBuilder()
                .WithImage("postgres:latest")
                .Build();
        }
    }
}
