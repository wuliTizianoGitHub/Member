using System.Reflection;

namespace MISD.SZMDA.Member.Runtime.Dependency
{
    public interface IConventionalRegistrationContext
    {
        Assembly Assembly { get; }

        IIocManager IocManager { get; }

        ConventionalRegistrationConfig Config { get; }
    }
}