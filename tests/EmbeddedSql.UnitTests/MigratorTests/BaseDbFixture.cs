using DotNet.Testcontainers.Containers;

namespace EmbeddedSql.UnitTests.MigratorTests
{
    public abstract class BaseDbFixture : IAsyncLifetime
    {
        public IDatabaseContainer Container { get; protected set; } = null!;

        public Task InitializeAsync()
        {
            return ((DockerContainer)Container).StartAsync();
        }

        public Task DisposeAsync()
        {
            return ((DockerContainer)Container).StartAsync();
        }
    }
}
