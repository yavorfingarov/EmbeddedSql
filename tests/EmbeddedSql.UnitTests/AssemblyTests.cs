using System.Runtime.CompilerServices;

namespace EmbeddedSql.UnitTests
{
    public class AssemblyTests
    {
        private readonly Assembly _Assembly;

        public AssemblyTests()
        {
            _Assembly = typeof(ISql).Assembly;
        }

        [Fact]
        public Task References()
        {
            var assemblies = _Assembly
                .GetReferencedAssemblies()
                .Select(x => x.Name)
                .Where(x => x != "netstandard")
                .OrderBy(x => x);

            return Verify(assemblies);
        }

        [Fact]
        public Task PublicTypes()
        {
            return Verify(GetDescription(_Assembly, t => t.IsPublic));
        }

        [Fact]
        public Task NonPublicTypes()
        {
            var description = GetDescription(_Assembly,
                t => !t.IsPublic && !t.IsDefined(typeof(CompilerGeneratedAttribute)) &&
                    t.FullName!.StartsWith("EmbeddedSql", StringComparison.InvariantCulture));

            return Verify(description);
        }

        private static object GetDescription(Assembly assembly, Func<Type, bool> typePredicate)
        {
            var types = assembly
                .GetTypes()
                .Where(typePredicate)
                .ToList();

            var description = new
            {
                Interface = GetList(types, x => x.IsInterface),
                Abstract = GetList(types, x => x.IsClass && x.IsAbstract && !x.IsSealed),
                Sealed = GetList(types, x => x.IsClass && !x.IsAbstract && x.IsSealed),
                Open = GetList(types, x => x.IsClass && !x.IsAbstract && !x.IsSealed),
                Static = GetList(types, x => x.IsClass && x.IsAbstract && x.IsSealed),
                Enum = GetList(types, x => x.IsEnum)
            };

            var reflectedCount =
                description.Interface.Count +
                description.Abstract.Count +
                description.Sealed.Count +
                description.Open.Count +
                description.Static.Count +
                description.Enum.Count;

            Assert.Equal(types.Count, reflectedCount);

            return description;
        }

        private static List<string?> GetList(List<Type> types, Func<Type, bool> predicate)
        {
            var result = types
                .Where(predicate)
                .Select(x => x.FullName)
                .ToList();

            return result;
        }
    }
}
