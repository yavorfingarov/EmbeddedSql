using Microsoft.Data.Sqlite;

namespace EmbeddedSql.SampleApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("Default");

            builder.Services.AddScoped<IDbConnection>(_ => new SqliteConnection(connectionString));

            builder.Services.AddEmbeddedSql();

            var app = builder.Build();

            app.MapUserEndpoints();

            app.EnsureDb();

            app.Run();
        }

        private static void EnsureDb(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var migrator = scope.ServiceProvider.GetRequiredService<IMigrator>();
            migrator.Run();
        }
    }
}
