using Microsoft.Data.SqlClient;
using Testcontainers.MsSql;

namespace EmbeddedSql.UnitTests.MigratorTests
{
    // TODO Fix pipeline issue:
    // Docker.DotNet.DockerImageNotFoundException :
    // Docker API responded with status code=NotFound, response={"message":"No such image: mcr.microsoft.com/mssql/server:latest"}
    //[Trait("Category", "Expensive")]
    //public class SqlServer : BaseMigratorTests, IClassFixture<SqlServerFixture>
    //{
    //    public SqlServer(SqlServerFixture fixture)
    //    {
    //        var adminConnectionString = fixture.Container.GetConnectionString();
    //        AdminDb = new SqlConnection(adminConnectionString);
    //        AdminDb.Execute(Sql["Db.Create"]);
    //        var testConnectionString = adminConnectionString.Replace(MsSqlBuilder.DefaultDatabase, "test");
    //        Db = new SqlConnection(testConnectionString);
    //        Db.Open();
    //    }
    //}

    //public class SqlServerFixture : BaseDbFixture
    //{
    //    public SqlServerFixture()
    //    {
    //        Container = new MsSqlBuilder()
    //            .WithImage("mcr.microsoft.com/mssql/server:latest")
    //            .Build();
    //    }
    //}
}
