using MISD.SZMDA.Member.Runtime.Configuration;

namespace MISD.SZMDA.Member.Runtime.Dependency
{
    public class ConventionalRegistrationConfig : DictionaryBasedConfig
    {
        public bool InstallInstallers { get; set; }

        public ConventionalRegistrationConfig()
        {
            InstallInstallers = true;
        }
    }
}