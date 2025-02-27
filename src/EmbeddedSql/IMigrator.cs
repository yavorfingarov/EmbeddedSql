namespace EmbeddedSql
{
    /// <summary>
    /// Specifies the contract for applying database migrations.
    /// </summary>
    public interface IMigrator
    {
        /// <summary>
        /// Applies database migrations.
        /// </summary>
        void Run();

        /// <inheritdoc cref="Run"/>
        Task RunAsync(CancellationToken cancellationToken = default);
    }
}
