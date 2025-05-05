using Cake.Frosting;
using Yf.Cake.Layers;
using Yf.Cake.Layers.Steps;

namespace Build
{
    public static class Program
    {
        public static int Main(string[] args) => Runner.Run(args);

        public sealed class Initialize : BaseInitialize
        {
        }

        [IsDependentOn(typeof(Initialize))]
        public sealed class RestoreNuGetPackages : BaseRestoreNuGetPackages
        {
        }

        [IsDependentOn(typeof(RestoreNuGetPackages))]
        public sealed class Build : BaseBuild
        {
        }

        [IsDependentOn(typeof(Build))]
        public sealed class ScanCode : BaseScanCode
        {
        }

        public sealed class RunInexpensiveUnitTests : BaseRunTests
        {
            public override string[] CoverageReportClassFilter => new[] { "-*Generated*", "-*RegexGenerator*" };
            public override string? Filter => "Category!=Expensive";
        }

        [IsDependentOn(typeof(ScanCode))]
        public sealed class RunUnitTests : BaseRunTests
        {
            public override bool CalculateCoverage => false;
        }

        [IsDependentOn(typeof(RunUnitTests))]
        public sealed class RunMutationTests : BaseRunMutationTests
        {
        }

        [IsDependentOn(typeof(RunMutationTests))]
        public sealed class CreatePackage : BasePack
        {
        }
    }
}
