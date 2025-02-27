namespace EmbeddedSql
{
    internal sealed class Migrator : IMigrator
    {
        private readonly DbConnection _Db;
        private readonly ISql _Sql;
        private readonly MigratorOptions _Options;
        private readonly ILogger _Logger;

        private DbTransaction? _Transaction;
        private ConnectionState _InitialConnectionState;

        internal Migrator(IServiceProvider serviceProvider, object? serviceKey)
        {
            _Db =
                serviceProvider.GetKeyedService<IDbConnection>(serviceKey).AsDbConnection() ??
                serviceProvider.GetRequiredService<IDbConnection>().AsDbConnection();

            _Sql = serviceProvider.GetRequiredKeyedService<ISql>(serviceKey);
            _Options = serviceProvider.GetRequiredKeyedService<MigratorOptions>(serviceKey);
            var loggerProvider = serviceProvider.GetRequiredService<ILoggerProvider>();
            _Logger = loggerProvider.CreateLogger("EmbeddedSql.Migrator");
        }

        public void Run()
        {
            try
            {
                PrepareDb();
                ApplyMigrations();
                _Logger.DbMigrated(_Db.Database);
            }
            finally
            {
                _Transaction?.Dispose();
                if (_InitialConnectionState == ConnectionState.Closed)
                {
                    _Db.Close();
                }
            }
        }

        public async Task RunAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await PrepareDbAsync(cancellationToken);
                await ApplyMigrationsAsync(cancellationToken);
                _Logger.DbMigrated(_Db.Database);
            }
            finally
            {
                if (_Transaction != null)
                {
                    await _Transaction.DisposeAsync();
                }

                if (_InitialConnectionState == ConnectionState.Closed)
                {
                    await _Db.CloseAsync();
                }
            }
        }

        private void PrepareDb()
        {
            CheckDb();
            _InitialConnectionState = _Db.State;
            if (_Db.State != ConnectionState.Open)
            {
                _Db.Open();
            }

            if (_Options.TransactionBehavior == TransactionBehavior.Overarching)
            {
                _Transaction = _Db.BeginTransaction();
            }
        }

        private async Task PrepareDbAsync(CancellationToken cancellationToken)
        {
            CheckDb();
            _InitialConnectionState = _Db.State;
            if (_Db.State != ConnectionState.Open)
            {
                await _Db.OpenAsync(cancellationToken);
            }

            if (_Options.TransactionBehavior == TransactionBehavior.Overarching)
            {
                _Transaction = await _Db.BeginTransactionAsync(cancellationToken);
            }
        }

        private void CheckDb()
        {
            if (_Options.TransactionBehavior != TransactionBehavior.None &&
                _Db.GetType().Name.Contains("MySql", StringComparison.OrdinalIgnoreCase))
            {
                _Logger.MySqlWithTransactionDetected();
            }
        }

        private void ApplyMigrations()
        {
            var migrations = GetHangingMigrations();
            foreach (var migration in migrations)
            {
                if (_Options.TransactionBehavior == TransactionBehavior.PerScript)
                {
                    _Transaction?.Dispose();
                    _Transaction = _Db.BeginTransaction();
                }

                _Logger.ApplyingMigration(migration, _Db.Database);
                ExecuteNonQuery($"{_Options.MigrationScriptPrefix}{migration}", null);

                if (!_Options.Idempotent)
                {
                    var parameters = new Dictionary<string, object?>();
                    _Options.CreateMigrationCommandParametersAction.Invoke(parameters, migration);
                    ExecuteNonQuery(_Options.CreateMigrationCommand, parameters);
                }

                if (_Options.TransactionBehavior == TransactionBehavior.PerScript)
                {
                    _Transaction?.Commit();
                }
            }

            if (_Options.TransactionBehavior == TransactionBehavior.Overarching)
            {
                _Transaction?.Commit();
            }
        }

        private async Task ApplyMigrationsAsync(CancellationToken cancellationToken)
        {
            var migrations = await GetHangingMigrationsAsync(cancellationToken);
            foreach (var migration in migrations)
            {
                if (_Options.TransactionBehavior == TransactionBehavior.PerScript)
                {
                    if (_Transaction != null)
                    {
                        await _Transaction.DisposeAsync();
                    }

                    _Transaction = await _Db.BeginTransactionAsync(cancellationToken);
                }

                _Logger.ApplyingMigration(migration, _Db.Database);
                await ExecuteNonQueryAsync($"{_Options.MigrationScriptPrefix}{migration}", null, cancellationToken);

                if (!_Options.Idempotent)
                {
                    var parameters = new Dictionary<string, object?>();
                    _Options.CreateMigrationCommandParametersAction.Invoke(parameters, migration);
                    await ExecuteNonQueryAsync(_Options.CreateMigrationCommand, parameters, cancellationToken);
                }

                if (_Options.TransactionBehavior == TransactionBehavior.PerScript && _Transaction != null)
                {
                    await _Transaction.CommitAsync(cancellationToken);
                }
            }

            if (_Options.TransactionBehavior == TransactionBehavior.Overarching && _Transaction != null)
            {
                await _Transaction.CommitAsync(cancellationToken);
            }
        }

        private IEnumerable<string> GetHangingMigrations()
        {
            var appliedMigrations = Enumerable.Empty<string>();
            if (!_Options.Idempotent)
            {
                ExecuteNonQuery(_Options.EnsureMigrationTableCommand, null);
                appliedMigrations = GetAppliedMigrations();
            }

            var hangingMigrations = GetHangingMigrations(appliedMigrations);

            return hangingMigrations;
        }

        private async Task<IEnumerable<string>> GetHangingMigrationsAsync(CancellationToken cancellationToken)
        {
            var appliedMigrations = Enumerable.Empty<string>();
            if (!_Options.Idempotent)
            {
                await ExecuteNonQueryAsync(_Options.EnsureMigrationTableCommand, null, cancellationToken);
                appliedMigrations = await GetAppliedMigrationsAsync(cancellationToken);
            }

            var hangingMigrations = GetHangingMigrations(appliedMigrations);

            return hangingMigrations;
        }

        private void ExecuteNonQuery(string commandKey, Dictionary<string, object?>? parameters)
        {
            using var command = CreateCommand(commandKey, parameters);
            command.ExecuteNonQuery();
        }

        private async Task ExecuteNonQueryAsync(
            string commandKey,
            Dictionary<string, object?>? parameters,
            CancellationToken cancellationToken)
        {
            await using var command = CreateCommand(commandKey, parameters);
            await command.ExecuteNonQueryAsync(cancellationToken);
        }

        private List<string> GetAppliedMigrations()
        {
            var appliedMigrations = new List<string>();
            using var command = CreateCommand(_Options.GetMigrationsQuery);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var migration = reader.GetString(0);
                appliedMigrations.Add(migration);
            }

            return appliedMigrations;
        }

        private async Task<List<string>> GetAppliedMigrationsAsync(CancellationToken cancellationToken)
        {
            var appliedMigrations = new List<string>();
            await using var command = CreateCommand(_Options.GetMigrationsQuery);
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var migration = reader.GetString(0);
                appliedMigrations.Add(migration);
            }

            return appliedMigrations;
        }

        private DbCommand CreateCommand(string key, Dictionary<string, object?>? parameters = null)
        {
            var command = _Db.CreateCommand();
            command.CommandText = _Sql[key];
            command.Transaction = _Transaction;
            if (parameters != null)
            {
                foreach (var (name, value) in parameters)
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = name;
                    parameter.Value = value;
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }

        private IEnumerable<string> GetHangingMigrations(IEnumerable<string> appliedMigrations)
        {
            var hangingMigrations = _Sql.AsEnumerable()
                .Select(x => x.Key)
                .Where(x => x.StartsWith(_Options.MigrationScriptPrefix, StringComparison.Ordinal))
                .Select(x => x[_Options.MigrationScriptPrefix.Length..])
                .Except(appliedMigrations)
                .OrderBy(x => x);

            return hangingMigrations;
        }
    }
}
