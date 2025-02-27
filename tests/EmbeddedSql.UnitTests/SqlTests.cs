namespace EmbeddedSql.UnitTests
{
    public class SqlTests
    {
        private readonly string[] _Args;

        public SqlTests()
        {
            _Args = new[] { "1 = 1", "2 = 2", "3 = 3", "4 = 4", "5 = 5" };
        }

        [Fact]
        public Task Ctor_Assemblies()
        {
            var sql = CreateSql(options =>
            {
                options.Assemblies = new[] { GetType().Assembly, typeof(TestData.Entry).Assembly };
            });

            return Verify(sql.AsEnumerable());
        }

        [Fact]
        public Task Ctor_Filter()
        {
            var sql = CreateSql(options =>
            {
                options.UseFilter(x => !x.Contains("Migrations"));
            });

            return Verify(sql.AsEnumerable());
        }

        [Fact]
        public Task Ctor_FilterNull()
        {
            return Throws(() =>
                CreateSql(options =>
                {
                    options.UseFilter(null!);
                }));
        }

        [Fact]
        public Task Ctor_DuplicateKey()
        {
            return Throws(() =>
                CreateSql(options =>
                {
                    options.Assemblies = new[] { GetType().Assembly, typeof(TestData.Duplicate.Entry).Assembly };
                }));
        }

        [Fact]
        public Task Indexer()
        {
            var sql = CreateSql();

            return Verify(sql["WithComment"]);
        }

        [Fact]
        public Task Indexer_Null()
        {
            var sql = CreateSql();

            return Throws(() => sql[null!]);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public Task Indexer_Empty(string key)
        {
            var sql = CreateSql();

            return Throws(() => sql[key])
                .DisableRequireUniquePrefix();
        }

        [Fact]
        public Task Indexer_InvalidKey()
        {
            var sql = CreateSql();

            return Throws(() => sql["Invalid"]);
        }

        [Fact]
        public Task UnsafeFormat_Arg0()
        {
            var sql = CreateSql();

            return Verify(sql.UnsafeFormat("1Arg", _Args[0]));
        }

        [Fact]
        public Task UnsafeFormat_Arg0_InvalidKey()
        {
            var sql = CreateSql();

            return Throws(() => sql.UnsafeFormat("Invalid", _Args[0]));
        }

        [Fact]
        public Task UnsafeFormat_Arg0_InvalidFormat()
        {
            var sql = CreateSql();

            return Throws(() => sql.UnsafeFormat("2Args", _Args[0]));
        }

        [Fact]
        public Task UnsafeFormat_Arg0Arg1()
        {
            var sql = CreateSql();

            return Verify(sql.UnsafeFormat("2Args", _Args[0], _Args[1]));
        }

        [Fact]
        public Task UnsafeFormat_Arg0Arg1_InvalidKey()
        {
            var sql = CreateSql();

            return Throws(() => sql.UnsafeFormat("Invalid", _Args[0], _Args[1]));
        }

        [Fact]
        public Task UnsafeFormat_Arg0Arg1_InvalidFormat()
        {
            var sql = CreateSql();

            return Throws(() => sql.UnsafeFormat("3Args", _Args[0], _Args[1]));
        }

        [Fact]
        public Task UnsafeFormat_Arg0Arg1Arg2()
        {
            var sql = CreateSql();

            return Verify(sql.UnsafeFormat("3Args", _Args[0], _Args[1], _Args[2]));
        }

        [Fact]
        public Task UnsafeFormat_Arg0Arg1Arg2_InvalidKey()
        {
            var sql = CreateSql();

            return Throws(() => sql.UnsafeFormat("Invalid", _Args[0], _Args[1], _Args[2]));
        }

        [Fact]
        public Task UnsafeFormat_Arg0Arg1Arg2_InvalidFormat()
        {
            var sql = CreateSql();

            return Throws(() => sql.UnsafeFormat("5Args", _Args[0], _Args[1], _Args[2]));
        }

        [Fact]
        public Task UnsafeFormat_Args()
        {
            var sql = CreateSql();

            return Verify(sql.UnsafeFormat("5Args", _Args[0], _Args[1], _Args[2], _Args[3], _Args[4]));
        }

        [Fact]
        public Task UnsafeFormat_Args_Null()
        {
            var sql = CreateSql();

            return Throws(() => sql.UnsafeFormat("5Args", (string[])null!));
        }

        [Fact]
        public Task UnsafeFormat_Args_InvalidKey()
        {
            var sql = CreateSql();

            return Throws(() => sql.UnsafeFormat("Invalid", _Args[0], _Args[1], _Args[2], _Args[3], _Args[4]));
        }

        [Fact]
        public Task UnsafeFormat_Args_InvalidFormat()
        {
            var sql = CreateSql();

            return Throws(() => sql.UnsafeFormat("6Args", _Args[0], _Args[1], _Args[2], _Args[3], _Args[4]));
        }

        [Fact]
        public Task AsEnumerable()
        {
            var sql = CreateSql();

            return Verify(sql.AsEnumerable());
        }

        private static ISql CreateSql(Action<EmbeddedSqlOptions>? configure = null)
        {
            var services = new ServiceCollection();
            configure ??= _ => { };
            services.AddEmbeddedSql(configure);
            var serviceProvider = services.BuildServiceProvider();
            var sql = serviceProvider.GetRequiredService<ISql>();

            return sql;
        }
    }
}
