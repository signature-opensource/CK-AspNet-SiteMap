using CK.Auth;
using CK.Core;
using CK.Testing;
using FluentAssertions.Equivalency.Steps;
using NUnit.Framework;
using System.Reflection;
using static CK.Testing.SqlServerTestHelper;

namespace CK.AspNet.SiteMap.Tests
{
    [TestFixture]
    public partial class SiteMapTests
    {
        [Test]
        public async Task AspNet_SiteMap_Async()
        {
            var targetOutputPath = TestHelper.GetTypeScriptBuildModeTargetProjectPath();

            var engineConfiguration = TestHelper.CreateDefaultEngineConfiguration();
            engineConfiguration.FirstBinPath.Assemblies.Add( "CK.AspNet.SiteMap" );
            engineConfiguration.FirstBinPath.EnsureTypeScriptConfigurationAspect( targetOutputPath );
            TestHelper.EnsureSqlServerConfigurationAspect( engineConfiguration );

            engineConfiguration.RunSuccessfully();

            await using var runner = TestHelper.CreateTypeScriptRunner(targetOutputPath);

            //
            // When running in Debug, this will wait until resume is set to true.
            // Until then, the .NET server is running and tests can be manually executed
            // written and fixed.
            // 
            // To stop, simply put a breakpoint in the resume lambda and sets the resume value
            // to true with the Watch window.
            //
            // In regular run, this will not wait for resume.
            //
            await TestHelper.SuspendAsync( resume => resume);

            runner.Run();

        }
    }
}
