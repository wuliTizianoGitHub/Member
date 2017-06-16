using System.Reflection;

namespace MISD.SZMDA.Member.Runtime.Dependency
{
    internal class ConventionalRegistrationContext : IConventionalRegistrationContext
    {
        public Assembly Assembly { get; private set; }
        public ConventionalRegistrationConfig Config { get; private set; }
        public IIocManager IocManager { get; private set; }

        public ConventionalRegistrationContext(Assembly assembly, IIocManager iocManager, ConventionalRegistrationConfig config)
        {
            Assembly = assembly;
            IocManager = iocManager;
            Config = config;
        }
    }
}