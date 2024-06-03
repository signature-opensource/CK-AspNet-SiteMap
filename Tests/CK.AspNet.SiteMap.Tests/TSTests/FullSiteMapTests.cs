using CK.Auth;
using CK.Core;
using CK.Testing;
using NUnit.Framework;
using System.Reflection;
using static CK.Testing.SqlServerTestHelper;

namespace CK.AspNet.SiteMap.Tests
{
    [TestFixture]
    public partial class FullSiteMapTests
    {
        [Test]
        public async Task E2ETest_Async()
        {
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


            // We must register CK.IO.Auth.Basic to have the IBasicLoginCommand and ILogoutCommand (there is no dependency on it).
            // And we must also register CK.Cris.Auth to have the CrisAuthenticationService
            var types = TestHelper.CreateTypeCollector( typeof( SiteMapCommandHandler ).Assembly );
            // 
            //                    .AddModelDependentAssembly( typeof( IBasicLoginCommand ).Assembly )
            //                    .AddModelDependentAssembly( typeof( CrisAuthenticationService ).Assembly );

            var engineConfiguration = TestHelper.CreateDefaultEngineConfiguration();
            TestHelper.EnsureSqlServerConfigurationAspect( engineConfiguration );

            var targetOutputPath = TestHelper.GetTypeScriptWithTestsSupportTargetProjectPath();
            Throw.DebugAssert( targetOutputPath.EndsWith( "/TSTests/E2ETest_Async" ) );
            await TestHelper.RunSingleBinPathAspNetE2ETestAsync( engineConfiguration,
                                                                 targetOutputPath,
                                                                 registeredTypes: types,
                                                                 tsTypes: Type.EmptyTypes,
                                                                 runner => TestHelper.SuspendAsync( resume => resume ) );
        }
    }
}
