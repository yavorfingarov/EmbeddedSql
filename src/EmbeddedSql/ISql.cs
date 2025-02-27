namespace EmbeddedSql
{
    /// <summary>
    /// Specifies the contract for getting embedded SQL statements.
    /// </summary>
    public interface ISql
    {
        /// <summary>
        /// Gets the SQL statement associated with the specified key.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        string this[string key] { get; }

        /// <summary>
        /// Replaces the format item in the SQL statement associated with the specified key.
        /// </summary>
        /// <remarks>
        /// <b>Never</b> pass non-validated user-provided values into this method. 
        /// Doing so may expose your application to SQL injection attacks.
        /// </remarks>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="FormatException"></exception>
        string UnsafeFormat(string key, string arg0);

        /// <summary>
        /// Replaces the format items in the SQL statement associated with the specified key.
        /// </summary>
        /// <inheritdoc cref="UnsafeFormat(string, string)"/>
        string UnsafeFormat(string key, string arg0, string arg1);

        /// <inheritdoc cref="UnsafeFormat(string, string, string)"/>
        string UnsafeFormat(string key, string arg0, string arg1, string arg2);

        /// <inheritdoc cref="UnsafeFormat(string, string, string)"/>
        string UnsafeFormat(string key, params string[] args);

        /// <summary>
        /// Gets all SQL statements.
        /// </summary>
        IEnumerable<KeyValuePair<string, string>> AsEnumerable();
    }
}
