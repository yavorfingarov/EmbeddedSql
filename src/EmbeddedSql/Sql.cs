using System.Collections.Frozen;
using System.Globalization;

namespace EmbeddedSql
{
    internal sealed class Sql : ISql
    {
        private readonly FrozenDictionary<string, string> _Statements;

        internal Sql(EmbeddedSqlOptions options)
        {
            var keyedStatements = options.Assemblies
                .SelectMany(Helpers.GetEmbeddedResources)
                .Where(x => x.ResourceName.EndsWith(".sql", StringComparison.OrdinalIgnoreCase))
                .Where(x => options.Filter(x.ResourceName))
                .Select(x => Helpers.GetEmbeddedResource(x.Assembly, x.ResourceName))
                .SelectMany(Helpers.GetKeyedStatements);

            var statements = new Dictionary<string, string>();
            foreach (var (key, value) in keyedStatements)
            {
                if (!statements.TryAdd(key, value))
                {
                    throw new InvalidOperationException($"Could not add statement with a duplicate key '{key}'.");
                }
            }

            _Statements = statements.ToFrozenDictionary();
        }

        public string this[string key]
        {
            get
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(key);

                if (!_Statements.TryGetValue(key, out var statement))
                {
                    throw new KeyNotFoundException($"Could not find statement with a key '{key}'.");
                }

                return statement;
            }
        }

        public string UnsafeFormat(string key, string arg0)
        {
            return string.Format(CultureInfo.InvariantCulture, this[key], arg0);
        }

        public string UnsafeFormat(string key, string arg0, string arg1)
        {
            return string.Format(CultureInfo.InvariantCulture, this[key], arg0, arg1);
        }

        public string UnsafeFormat(string key, string arg0, string arg1, string arg2)
        {
            return string.Format(CultureInfo.InvariantCulture, this[key], arg0, arg1, arg2);
        }

        public string UnsafeFormat(string key, params string[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, this[key], args);
        }

        public IEnumerable<KeyValuePair<string, string>> AsEnumerable()
        {
            return _Statements.AsEnumerable();
        }
    }
}
