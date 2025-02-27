namespace EmbeddedSql.UnitTests
{
    public class ServiceCollectionExtensionsTests
    {
        private readonly IServiceCollection _Services;

        public ServiceCollectionExtensionsTests()
        {
            _Services = new ServiceCollection();
        }

        [Fact]
        public Task AddEmbeddedSql()
        {
            _Services.AddEmbeddedSql();

            return Verify(_Services)
                .IgnoreMembersThatThrow<InvalidOperationException>();
        }

        [Fact]
        public Task AddEmbeddedSql_ServicesNull()
        {
            IServiceCollection services = null!;

            return Throws(services.AddEmbeddedSql);
        }

        [Fact]
        public Task AddEmbeddedSql_ConfigureNull()
        {
            _Services.AddEmbeddedSql();

            return Throws(() => _Services.AddEmbeddedSql(null!));
        }

        [Fact]
        public Task AddEmbeddedSql_ServiceKey()
        {
            _Services.AddEmbeddedSql(options =>
            {
                options.ServiceKey = "Test";
            });

            return Verify(_Services);
        }

        [Fact]
        public Task AddEmbeddedSql_AssembliesNull()
        {
            return Throws(() =>
                _Services.AddEmbeddedSql(options =>
                {
                    options.Assemblies = null!;
                }));
        }

        [Fact]
        public Task AddEmbeddedSql_FilterNull()
        {
            return Throws(() =>
                _Services.AddEmbeddedSql(options =>
                {
                    options.UseFilter(null!);
                }));
        }

        [Fact]
        public Task AddEmbeddedSql_ConfigureMigratorNull()
        {
            return Throws(() =>
                _Services.AddEmbeddedSql(options =>
                {
                    options.ConfigureMigrator(null!);
                }));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public Task AddEmbeddedSql_ConfigureMigrator_InvalidEnsureMigrationTableCommand(string? value)
        {
            return Throws(() =>
                _Services.AddEmbeddedSql(options =>
                {
                    options.ConfigureMigrator(migratorOptions =>
                    {
                        migratorOptions.EnsureMigrationTableCommand = value!;
                    });
                }))
                .UseParameters(value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public Task AddEmbeddedSql_ConfigureMigrator_InvalidGetMigrationsQuery(string? value)
        {
            return Throws(() =>
                _Services.AddEmbeddedSql(options =>
                {
                    options.ConfigureMigrator(migratorOptions =>
                    {
                        migratorOptions.GetMigrationsQuery = value!;
                    });
                }))
                .UseParameters(value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public Task AddEmbeddedSql_ConfigureMigrator_InvalidCreateMigrationCommand(string? value)
        {
            return Throws(() =>
                _Services.AddEmbeddedSql(options =>
                {
                    options.ConfigureMigrator(migratorOptions =>
                    {
                        migratorOptions.CreateMigrationCommand = value!;
                    });
                }))
                .UseParameters(value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public Task AddEmbeddedSql_ConfigureMigrator_InvalidMigrationScriptPrefix(string? value)
        {
            return Throws(() =>
                _Services.AddEmbeddedSql(options =>
                {
                    options.ConfigureMigrator(migratorOptions =>
                    {
                        migratorOptions.MigrationScriptPrefix = value!;
                    });
                }))
                .UseParameters(value);
        }

        [Fact]
        public Task AddEmbeddedSql_ConfigureMigrator_InvalidTransactionBehavior()
        {
            return Throws(() =>
                _Services.AddEmbeddedSql(options =>
                {
                    options.ConfigureMigrator(migratorOptions =>
                    {
                        migratorOptions.TransactionBehavior = (TransactionBehavior)100;
                    });
                }));
        }

        [Fact]
        public Task AddEmbeddedSql_ConfigureMigrator_ConfigureCreateMigrationCommandParametersNull()
        {
            return Throws(() =>
                _Services.AddEmbeddedSql(options =>
                {
                    options.ConfigureMigrator(migratorOptions =>
                    {
                        migratorOptions.ConfigureCreateMigrationCommandParameters(null!);
                    });
                }));
        }
    }
}
