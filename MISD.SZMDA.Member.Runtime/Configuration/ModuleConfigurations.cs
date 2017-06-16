namespace MISD.SZMDA.Member.Runtime.Configuration
{
    internal class ModuleConfigurations : IModuleConfigurations
    {
        public IStartupConfiguration StartupConfiguration { get; private set; }

        public ModuleConfigurations(IStartupConfiguration startupConfiguration)
        {
            StartupConfiguration = startupConfiguration;
        }
    }
}
