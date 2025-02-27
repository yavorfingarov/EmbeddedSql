namespace EmbeddedSql
{
    /// <summary>
    /// Specifies a transaction behavior when applying migrations.
    /// </summary>
    public enum TransactionBehavior
    {
        /// <summary>
        /// Migrations are applied without transaction.
        /// </summary>
        None,

        /// <summary>
        /// Every migration is wrapped in a transaction.
        /// </summary>
        PerScript,

        /// <summary>
        /// All migrations are wrapped in a single transaction.
        /// </summary>
        Overarching
    }
}
