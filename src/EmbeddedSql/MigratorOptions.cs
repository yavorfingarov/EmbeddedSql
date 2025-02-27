namespace EmbeddedSql
{
    /// <summary>
    /// Options for <see cref="IMigrator"/>.
    /// </summary>
    public sealed class MigratorOptions
    {
        private string _EnsureMigrationTableCommand;
        private string _GetMigrationsQuery;
        private string _CreateMigrationCommand;
        private string _MigrationScriptPrefix;
        private TransactionBehavior _TransactionBehavior;

        internal MigratorOptions()
        {
            _EnsureMigrationTableCommand = "_Migration.EnsureTable";
            _GetMigrationsQuery = "_Migration.GetAll";
            _CreateMigrationCommand = "_Migration.Create";
            _MigrationScriptPrefix = "Migration.";
            _TransactionBehavior = TransactionBehavior.PerScript;
            CreateMigrationCommandParametersAction = (parameters, migration) => parameters.Add("@Id", migration);
        }

        /// <summary>
        /// Sets the key of an idempotent command for creating the migration table.
        /// </summary>
        /// <remarks>
        /// Default: <c>_Migration.EnsureTable</c>
        /// </remarks>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public string EnsureMigrationTableCommand
        {
            internal get => _EnsureMigrationTableCommand;
            set => _EnsureMigrationTableCommand = value.ThrowWhenNullOrEmpty();
        }

        /// <summary>
        /// Sets the key of a query for getting the already applied migrations.
        /// </summary>
        /// <remarks>
        /// Default: <c>_Migration.GetAll</c>
        /// </remarks>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public string GetMigrationsQuery
        {
            internal get => _GetMigrationsQuery;
            set => _GetMigrationsQuery = value.ThrowWhenNullOrEmpty();
        }

        /// <summary>
        /// Sets the key of a command for creating an entry for an applied migration.
        /// </summary>
        /// <remarks>
        /// Default: <c>_Migration.Create</c>
        /// </remarks>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public string CreateMigrationCommand
        {
            internal get => _CreateMigrationCommand;
            set => _CreateMigrationCommand = value.ThrowWhenNullOrEmpty();
        }

        /// <summary>
        /// Sets the key prefix used for marking a migration.
        /// </summary>
        /// <remarks>
        /// Default: <c>Migration.</c>
        /// </remarks>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public string MigrationScriptPrefix
        {
            internal get => _MigrationScriptPrefix;
            set => _MigrationScriptPrefix = value.ThrowWhenNullOrEmpty();
        }

        /// <summary>
        /// Sets the boolean flag that determines whether the scripts are idempotent.
        /// </summary>
        /// <remarks>
        /// Default: <see langword="false"/>
        /// </remarks>
        public bool Idempotent { internal get; set; }

        /// <summary>
        /// Sets the transaction behavior when applying migrations.
        /// </summary>
        /// <remarks>
        /// Default: <see cref="TransactionBehavior.PerScript"/>
        /// </remarks>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public TransactionBehavior TransactionBehavior
        {
            internal get => _TransactionBehavior;
            set
            {
                if (!Enum.IsDefined(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"Got an invalid '{typeof(TransactionBehavior)}' value.");
                }

                _TransactionBehavior = value;
            }
        }

        internal Action<Dictionary<string, object?>, string> CreateMigrationCommandParametersAction { get; private set; }

        /// <summary>
        /// Configures the parameters for <see cref="CreateMigrationCommand"/>.
        /// </summary>
        /// <remarks>
        /// Default: <c>(parameters, migration) => parameters.Add("@Id", migration)</c>
        /// </remarks>
        /// <exception cref="ArgumentNullException"></exception>
        public void ConfigureCreateMigrationCommandParameters(Action<Dictionary<string, object?>, string> configure)
        {
            ArgumentNullException.ThrowIfNull(configure);

            CreateMigrationCommandParametersAction = configure;
        }
    }
}
