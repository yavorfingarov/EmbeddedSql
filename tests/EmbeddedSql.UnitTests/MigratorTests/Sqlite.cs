using Microsoft.Data.Sqlite;

namespace EmbeddedSql.UnitTests.MigratorTests
{
    public sealed class Sqlite : BaseMigratorTests
    {
        private const string _ConnectionString = "DataSource=TestDb;Mode=Memory;Cache=Shared";

        private readonly SqliteConnection _KeepAliveConnection;

        public Sqlite()
        {
            _KeepAliveConnection = new SqliteConnection(_ConnectionString);
            _KeepAliveConnection.Open();
            Db = new SqliteConnection(_ConnectionString);
            Db.Open();
        }

        public override void Dispose()
        {
            base.Dispose();
            _KeepAliveConnection.Dispose();
        }
    }
}
