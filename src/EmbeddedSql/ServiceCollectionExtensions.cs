namespace EmbeddedSql
{
    /// <summary>
    /// Extension methods for configuring services at application startup.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="EmbeddedSql"/> services to the <see cref="IServiceCollection"/> 
        /// using the default <see cref="EmbeddedSqlOptions"/>:
        /// <list type="bullet">
        ///     <item>
        ///         <see cref="ISql"/> with a <see cref="ServiceLifetime.Singleton"/>
        ///     </item>
        ///     <item>
        ///         <see cref="IMigrator"/> with a <see cref="ServiceLifetime.Scoped"/>
        ///     </item>
        /// </list>
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static IServiceCollection AddEmbeddedSql(this IServiceCollection services)
        {
            var callingAssembly = Assembly.GetCallingAssembly();

            return services.AddEmbeddedSql(callingAssembly);
        }

        /// <summary>
        /// Adds the <see cref="EmbeddedSql"/> services to the <see cref="IServiceCollection"/> 
        /// using custom <see cref="EmbeddedSqlOptions"/>:
        /// <list type="bullet">
        ///     <item>
        ///         <see cref="ISql"/> with a <see cref="ServiceLifetime.Singleton"/>
        ///     </item>
        ///     <item>
        ///         <see cref="IMigrator"/> with a <see cref="ServiceLifetime.Scoped"/>
        ///     </item>
        /// </list>
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static IServiceCollection AddEmbeddedSql(this IServiceCollection services, Action<EmbeddedSqlOptions> configure)
        {
            ArgumentNullException.ThrowIfNull(configure);
            var callingAssembly = Assembly.GetCallingAssembly();

            return services.AddEmbeddedSql(callingAssembly, configure);
        }

        private static IServiceCollection AddEmbeddedSql(
            this IServiceCollection services,
            Assembly callingAssembly,
            Action<EmbeddedSqlOptions>? configure = null)
        {
            var options = new EmbeddedSqlOptions(callingAssembly);
            configure?.Invoke(options);
            var sql = new Sql(options);
            services.AddKeyedSingleton<ISql>(options.ServiceKey, sql);
            services.AddKeyedSingleton(options.ServiceKey, options.MigratorOptions);
            services.AddKeyedScoped<IMigrator>(
                options.ServiceKey,
                (serviceProvider, serviceKey) => new Migrator(serviceProvider, serviceKey));

            return services;
        }
    }
}
