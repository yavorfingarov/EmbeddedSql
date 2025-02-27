using MySqlConnector;
using Testcontainers.MariaDb;

namespace EmbeddedSql.UnitTests.MigratorTests
{
    [Trait("Category", "Expensive")]
    public class MariaDb : BaseMigratorTests, IClassFixture<MariaDbFixture>
    {
        public MariaDb(MariaDbFixture fixture)
        {
            var connectionString = fixture.Container.GetConnectionString();
            AdminDb = new MySqlConnection(connectionString);
            Db = new MySqlConnection(connectionString);
            Db.Open();
        }
    }

    public class MariaDbFixture : BaseDbFixture
    {
        public MariaDbFixture()
        {
            Container = new MariaDbBuilder()
                .WithImage("mariadb:latest")
                .Build();
        }
    }
}
