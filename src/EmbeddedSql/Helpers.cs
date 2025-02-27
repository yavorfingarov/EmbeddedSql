using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace EmbeddedSql
{
    internal static partial class Helpers
    {
        internal static IEnumerable<(Assembly Assembly, string ResourceName)> GetEmbeddedResources(Assembly assembly)
        {
            var resourceNames = assembly.GetManifestResourceNames();
            foreach (var resourceName in resourceNames)
            {
                yield return (assembly, resourceName);
            }
        }

        internal static string GetEmbeddedResource(Assembly assembly, string resourceName)
        {
            using var stream = assembly.GetManifestResourceStream(resourceName)
                ?? throw new InvalidOperationException($"Could not read resource '{resourceName}'.");

            using var reader = new StreamReader(stream);
            var resource = reader.ReadToEnd();

            return resource;
        }

        internal static IEnumerable<KeyValuePair<string, string>> GetKeyedStatements(string file)
        {
            var statementMatches = StatementRegex().Matches(file);
            foreach (Match statementMatch in statementMatches)
            {
                var key = statementMatch.Groups["Key"].Value;
                var value = statementMatch.Groups["Statement"].Value;
                var keyValuePair = new KeyValuePair<string, string>(key, value);

                yield return keyValuePair;
            }
        }

        internal static string ThrowWhenNullOrEmpty(this string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);

            return value;
        }

        [return: NotNullIfNotNull(nameof(db))]
        internal static DbConnection? AsDbConnection(this IDbConnection? db)
        {
            if (db == null)
            {
                return null;
            }
            else if (db is DbConnection dbConnection)
            {
                return dbConnection;
            }
            else
            {
                throw new InvalidOperationException($"'{db.GetType()}' does not inherit '{typeof(DbConnection)}'.");
            }
        }

        [GeneratedRegex(@"(?<=---[ \t]*)(?'Key'\w.*?)\s*(?<=[\n\r]+\s*)(?'Statement'--[ \w][\s\S]*?|\w[\s\S]*?)(?=\s*[\n\r]+---|\s*$)")]
        private static partial Regex StatementRegex();
    }
}
