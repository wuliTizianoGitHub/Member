using MISD.SZMDA.Member.Runtime.Configuration;
using MISD.SZMDA.Member.Runtime.Dependency;
using System.Reflection;

namespace MISD.SZMDA.Member.Runtime.Modules
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class KernelModule : BaseModule
    {
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new BasicConventionalRegistrar());

            IocManager.Register<IScopedIocResolver, ScopedIocResolver>(DependencyLifeStyle.Transient);

            //TODO:some registrar...


        }

        public override void Initialize()
        {
            foreach (var replaceAction in ((StartupConfiguration)Configuration).ServiceReplaceActions.Values)
            {
                replaceAction();
            }


            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly(),
                new ConventionalRegistrationConfig
                {
                    InstallInstallers = false
                });
        }

        public override void PostInitialize()
        {
            
        }

        public override void Shutdown()
        {
           
        }
    }
}
