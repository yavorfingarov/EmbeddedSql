namespace EmbeddedSql
{
    /// <summary>
    /// Options for <see cref="ISql"/> and <see cref="IMigrator"/>.
    /// </summary>
    public sealed class EmbeddedSqlOptions
    {
        private IEnumerable<Assembly> _Assemblies;

        internal EmbeddedSqlOptions(Assembly callingAssembly)
        {
            _Assemblies = new[] { callingAssembly };
            Filter = _ => true;
            MigratorOptions = new MigratorOptions();
        }

        /// <summary>
        /// Sets the <see cref="ISql"/>, <see cref="IMigrator"/> and <see cref="EmbeddedSql.MigratorOptions"/> service key.
        /// </summary>
        /// <remarks>
        /// Default: <see langword="null"/>
        /// </remarks>
        public object? ServiceKey { internal get; set; }

        /// <summary>
        /// Sets the assemblies to scan for embedded <c>.sql</c> files.
        /// </summary>
        /// <remarks>
        /// Default: the calling assembly for 
        /// <see cref="ServiceCollectionExtensions.AddEmbeddedSql(IServiceCollection, Action{EmbeddedSqlOptions}?)"/>
        /// </remarks>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<Assembly> Assemblies
        {
            internal get => _Assemblies;
            set
            {
                ArgumentNullException.ThrowIfNull(value);

                _Assemblies = value;
            }
        }

        internal Func<string, bool> Filter { get; private set; }

        internal MigratorOptions MigratorOptions { get; }

        /// <summary>
        /// Sets a resource name filter.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public void UseFilter(Func<string, bool> filter)
        {
            ArgumentNullException.ThrowIfNull(filter);

            Filter = filter;
        }

        /// <summary>
        /// Configures the <see cref="EmbeddedSql.MigratorOptions"/> for <see cref="IMigrator"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public void ConfigureMigrator(Action<MigratorOptions> configure)
        {
            ArgumentNullException.ThrowIfNull(configure);

            configure.Invoke(MigratorOptions);
        }
    }
}
