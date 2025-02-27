using Microsoft.Extensions.Logging;
using NSubstitute;
using VerifyTests.MicrosoftLogging;

namespace EmbeddedSql.UnitTests.MigratorTests
{
    public abstract class BaseMigratorTests : IDisposable
    {
        protected IDbConnection AdminDb { get; set; } = null!;
        protected IDbConnection Db { get; set; } = null!;
        protected ISql Sql { get; set; } = null!;

        private readonly ILoggerProvider _LoggerProvider;
        private readonly DateTime _UtcNow;

        protected BaseMigratorTests()
        {
            _LoggerProvider = new RecordingProvider();
            _UtcNow = new DateTime(2024, 10, 14, 16, 15, 0);
            CreateServiceProvider(firstRun: true, serviceKey: null, configureMigrator: null, overrideDbConfig: null);
            Recording.Start();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task DefaultOptions(bool runAsync)
        {
            await RunSequence(runAsync);

            await Verify();
        }

        [Fact]
        public async Task DefaultOptions_NoDb()
        {
            await ThrowsTask(() =>
                RunSequence(runAsync: default, overrideDbConfig: _ => { }));
        }

        [Fact]
        public async Task DefaultOptions_NoDbConnectionImplementation()
        {
            await ThrowsTask(() =>
                RunSequence(
                    runAsync: default,
                    overrideDbConfig: services =>
                    {
                        services.AddScoped(_ => Substitute.For<IDbConnection>());
                    }));
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task DefaultOptions_ServiceKey(bool runAsync)
        {
            await RunSequence(runAsync, serviceKey: "Test");

            await Verify();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task DefaultOptions_ServiceKey_KeyedDb(bool runAsync)
        {
            await RunSequence(
                runAsync,
                serviceKey: "Test",
                overrideDbConfig: services =>
                {
                    services.AddKeyedScoped("Test", (_, _) => Db);
                });

            await Verify();
        }

        [Fact]
        public async Task DefaultOptions_ServiceKey_NoDb()
        {
            await ThrowsTask(() =>
                RunSequence(runAsync: default, serviceKey: "Test", overrideDbConfig: _ => { }));
        }

        [Fact]
        public async Task DefaultOptions_ServiceKey_NoDbConnectionImplementation()
        {
            await ThrowsTask(() =>
                RunSequence(
                    runAsync: default,
                    serviceKey: "Test",
                    overrideDbConfig: services =>
                    {
                        services.AddScoped(_ => Substitute.For<IDbConnection>());
                    }));
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task DefaultOptions_ConnectionClosed(bool runAsync)
        {
            Db.Close();

            await RunSequence(runAsync);

            await Verify();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task DefaultOptions_Error(bool runAsync)
        {
            await Assert.ThrowsAnyAsync<Exception>(() =>
                RunSequence(
                    runAsync,
                    configureMigrator: options =>
                    {
                        options.MigrationScriptPrefix = "MigrationError.";
                    }));

            await Verify();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task DefaultOptions_Idempotent(bool runAsync)
        {
            await RunSequence(
                runAsync,
                configureMigrator: options =>
                {
                    options.MigrationScriptPrefix = "MigrationIdempotent.";
                    options.Idempotent = true;
                });

            await Verify();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task DefaultOptions_Idempotent_Error(bool runAsync)
        {
            await Assert.ThrowsAnyAsync<Exception>(() =>
                RunSequence(
                    runAsync,
                    configureMigrator: options =>
                    {
                        options.MigrationScriptPrefix = "MigrationError.";
                        options.Idempotent = true;
                    }));

            await Verify();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task OverarchingTransaction(bool runAsync)
        {
            await RunSequence(
                runAsync,
                configureMigrator: options =>
                {
                    options.TransactionBehavior = TransactionBehavior.Overarching;
                });

            await Verify();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task OverarchingTransaction_Error(bool runAsync)
        {
            await Assert.ThrowsAnyAsync<Exception>(() =>
                RunSequence(
                    runAsync,
                    configureMigrator: options =>
                    {
                        options.MigrationScriptPrefix = "MigrationError.";
                        options.TransactionBehavior = TransactionBehavior.Overarching;
                    }));

            await Verify();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task OverarchingTransaction_Idempotent(bool runAsync)
        {
            await RunSequence(
                runAsync,
                configureMigrator: options =>
                {
                    options.MigrationScriptPrefix = "MigrationIdempotent.";
                    options.Idempotent = true;
                    options.TransactionBehavior = TransactionBehavior.Overarching;
                });

            await Verify();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task OverarchingTransaction_Idempotent_Error(bool runAsync)
        {
            await Assert.ThrowsAnyAsync<Exception>(() =>
                RunSequence(
                    runAsync,
                    configureMigrator: options =>
                    {
                        options.MigrationScriptPrefix = "MigrationError.";
                        options.Idempotent = true;
                        options.TransactionBehavior = TransactionBehavior.Overarching;
                    }));

            await Verify();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task NoTransaction(bool runAsync)
        {
            await RunSequence(
                runAsync,
                configureMigrator: options =>
                {
                    options.TransactionBehavior = TransactionBehavior.None;
                });

            await Verify();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task NoTransaction_Error(bool runAsync)
        {
            await Assert.ThrowsAnyAsync<Exception>(() =>
                RunSequence(
                    runAsync,
                    configureMigrator: options =>
                    {
                        options.MigrationScriptPrefix = "MigrationError.";
                        options.TransactionBehavior = TransactionBehavior.None;
                    }));

            await Verify();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task NoTransaction_Idempotent(bool runAsync)
        {
            await RunSequence(
                runAsync,
                configureMigrator: options =>
                {
                    options.MigrationScriptPrefix = "MigrationIdempotent.";
                    options.Idempotent = true;
                    options.TransactionBehavior = TransactionBehavior.None;
                });

            await Verify();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task NoTransaction_Idempotent_Error(bool runAsync)
        {
            await Assert.ThrowsAnyAsync<Exception>(() =>
                RunSequence(
                    runAsync,
                    configureMigrator: options =>
                    {
                        options.MigrationScriptPrefix = "MigrationError.";
                        options.Idempotent = true;
                        options.TransactionBehavior = TransactionBehavior.None;
                    }));

            await Verify();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task CustomMigrationTable(bool runAsync)
        {
            await RunSequence(
                runAsync,
                configureMigrator: options =>
                {
                    options.EnsureMigrationTableCommand = "_MigrationCustom.EnsureTable";
                    options.GetMigrationsQuery = "_MigrationCustom.GetAll";
                    options.CreateMigrationCommand = "_MigrationCustom.Create";
                    options.ConfigureCreateMigrationCommandParameters((parameters, migration) =>
                    {
                        parameters["@Id"] = migration;
                        parameters["@Timestamp"] = _UtcNow;
                    });
                });

            await Verify();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task CustomMigrationTable_Error(bool runAsync)
        {
            await Assert.ThrowsAnyAsync<Exception>(() =>
                RunSequence(
                    runAsync,
                    configureMigrator: options =>
                    {
                        options.EnsureMigrationTableCommand = "_MigrationCustom.EnsureTable";
                        options.GetMigrationsQuery = "_MigrationCustom.GetAll";
                        options.CreateMigrationCommand = "_MigrationCustom.Create";
                        options.TransactionBehavior = TransactionBehavior.PerScript;
                        options.ConfigureCreateMigrationCommandParameters((parameters, migration) =>
                        {
                            parameters["@Id"] = migration;
                            parameters["@Timestamp"] = migration.Contains("0003") ? null : _UtcNow;
                        });
                    }));

            await Verify();
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
            Db?.Dispose();
            AdminDb?.Execute(Sql["Db.Clean"]);
            AdminDb?.Dispose();
        }

        protected IServiceProvider CreateServiceProvider(
            bool firstRun,
            object? serviceKey,
            Action<MigratorOptions>? configureMigrator,
            Action<IServiceCollection>? overrideDbConfig)
        {
            var services = new ServiceCollection();
            services.AddSingleton(_LoggerProvider);
            if (overrideDbConfig != null)
            {
                overrideDbConfig.Invoke(services);
            }
            else
            {
                services.AddScoped(_ => Db);
            }

            services.AddEmbeddedSql(options =>
            {
                options.ServiceKey = serviceKey;
                options.Assemblies = new[] { typeof(TestData.Providers.Entry).Assembly };
                if (firstRun)
                {
                    options.UseFilter(x => x.Contains(GetType().Name) && x.Contains('1'));
                }
                else
                {
                    options.UseFilter(x => x.Contains(GetType().Name));
                }

                if (configureMigrator != null)
                {
                    options.ConfigureMigrator(configureMigrator);
                }
            });

            var serviceProvider = services.BuildServiceProvider();
            Sql = serviceProvider.GetRequiredKeyedService<ISql>(serviceKey);

            return serviceProvider;
        }

        private async Task RunSequence(
            bool runAsync,
            object? serviceKey = null,
            Action<MigratorOptions>? configureMigrator = null,
            Action<IServiceCollection>? overrideDbConfig = null)
        {
            await RunMigrator(runAsync, serviceKey, firstRun: true, configureMigrator, overrideDbConfig);
            await RunMigrator(runAsync, serviceKey, firstRun: false, configureMigrator, overrideDbConfig);
            await RunMigrator(runAsync, serviceKey, firstRun: false, configureMigrator, overrideDbConfig);
        }

        private async Task RunMigrator(
            bool runAsync,
            object? serviceKey,
            bool firstRun,
            Action<MigratorOptions>? configureMigrator,
            Action<IServiceCollection>? overrideDbConfig)
        {
            var serviceProvider = CreateServiceProvider(firstRun, serviceKey, configureMigrator, overrideDbConfig);
            var migrator = serviceProvider.GetRequiredKeyedService<IMigrator>(serviceKey);
            if (runAsync)
            {
                await migrator.RunAsync();
            }
            else
            {
                migrator.Run();
            }
        }

        private async Task Verify()
        {
            var connection = Db.State.ToString();
            var transactionException = Db.State == ConnectionState.Open ? Record.Exception(Db.BeginTransaction) : null;
            Db.Close();
            var schema = await Db.QueryAsync<string>(Sql["Schema.Describe"]);
            var hasMigrations = schema
                .Any(x => x.Contains("Migration", StringComparison.OrdinalIgnoreCase));

            var migrations = hasMigrations ? await Db.QueryAsync(Sql["_MigrationCustom.GetAll"]) : null;

            await Verifier
                .Verify(new
                {
                    connection,
                    transactionDisposed = transactionException == null,
                    schema,
                    migrations
                })
                .DontScrubDateTimes()
                .DisableRequireUniquePrefix();
        }
    }
}
