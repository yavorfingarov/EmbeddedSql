namespace EmbeddedSql
{
    static class LoggerExtensions
    {
        private readonly static Action<ILogger, string, string, Exception?> _ApplyingMigration =
            LoggerMessage.Define<string, string>(LogLevel.Information, default, "Applying '{Migration}' to '{Db}'.");

        private readonly static Action<ILogger, string, Exception?> _DbMigrated =
            LoggerMessage.Define<string>(LogLevel.Information, default, "Database '{Db}' is migrated to the latest state.");

        private readonly static Action<ILogger, Exception?> _MySqlWithTransaction =
            LoggerMessage.Define(LogLevel.Warning, default, "Executing DDL statements in transaction on a MySql database " +
                "might have unexpected consequences due to implicit commit.");

        internal static void ApplyingMigration(this ILogger logger, string migration, string dbName)
        {
            _ApplyingMigration(logger, migration, dbName, null);
        }

        internal static void DbMigrated(this ILogger logger, string dbName)
        {
            _DbMigrated(logger, dbName, null);
        }

        internal static void MySqlWithTransactionDetected(this ILogger logger)
        {
            _MySqlWithTransaction(logger, null);
        }
    }
}
