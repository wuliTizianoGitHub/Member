using MISD.SZMDA.Member.Runtime;
using MISD.SZMDA.Member.Runtime.Modules;
using Shouldly;
using Xunit;

namespace MISD.SZMDA.Member.Tests.Modules
{
    public class ModuleBootstrapper_Tests: TestBaseWithLocalIocManager
    {
        [Fact]
        public void Bootstrapper_ShouldBe_Startup()
        {
            //初始化
            var bootstrapper = Bootstrapper.Create<MyStartupModule>(LocalIocManager);

            bootstrapper.Initialize();

            var modules = bootstrapper.IocManager.Resolve<IModuleManager>().Modules;

            modules.Count.ShouldBe(4);
        }
    }
    [DependsOn(typeof(MyModule1), typeof(MyModule2))]
    public class MyStartupModule : BaseModule { }

    public class MyModule1 : BaseModule { }

    public class MyModule2 : BaseModule { }

}
