using System.Collections.Generic;
using System.Reflection;

namespace MISD.SZMDA.Member.Runtime.Reflection
{
    /// <summary>
    /// 用于获取应用程序中的assembly
    /// </summary>
    public interface IAssemblyFinder
    {
        List<Assembly> GetAllAssemblies();
    }
}
