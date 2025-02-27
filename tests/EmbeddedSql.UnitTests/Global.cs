global using System.Data;
global using System.Reflection;
global using Dapper;
global using Microsoft.Extensions.DependencyInjection;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Xunit.Abstractions;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: TestCollectionOrderer("EmbeddedSql.UnitTests.ContainerOrderer", "EmbeddedSql.UnitTests")]

[assembly: SuppressMessage(
    "Naming",
    "CA1707:Identifiers should not contain underscores",
    Justification = "Test names can contain underscores.")]

[assembly: SuppressMessage(
    "Performance",
    "CA1861:Avoid constant arrays as arguments",
    Justification = "Constant arrays are acceptable in tests.")]

[assembly: SuppressMessage(
    "Performance",
    "CA1859:Use concrete types when possible for improved performance",
    Justification = "Performance is not an issue.")]

namespace EmbeddedSql.UnitTests
{
    public static class ModuleInitializer
    {
        [ModuleInitializer]
        public static void Initialize()
        {
            VerifyMicrosoftLogging.Initialize();

            VerifierSettings.IgnoreStackTrace();
        }
    }

    public class ContainerOrderer : ITestCollectionOrderer
    {
        public IEnumerable<ITestCollection> OrderTestCollections(IEnumerable<ITestCollection> testCollections)
        {
            var containerCollections = testCollections
                .Where(x => x.DisplayName.Contains("MigratorTests") && !x.DisplayName.Contains("Sqlite"));
            var result = testCollections
                .Where(x => !x.DisplayName.Contains("MigratorTests") || x.DisplayName.Contains("Sqlite"))
                .Concat(containerCollections);

            return result;
        }
    }
}
