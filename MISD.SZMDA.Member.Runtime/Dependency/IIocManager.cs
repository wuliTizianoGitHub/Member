using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISD.SZMDA.Member.Runtime.Dependency
{
    public interface IIocManager: IIocRegistrar, IIocResolver, IDisposable
    {
        IWindsorContainer IocContainer { get; }

        new bool IsRegistered(Type type);

        new bool IsRegistered<T>();
    }
}
