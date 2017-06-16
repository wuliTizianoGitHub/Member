using MISD.SZMDA.Member.Runtime;
using MISD.SZMDA.Member.Runtime.Dependency;
using MISD.SZMDA.Member.Runtime.Exceptions;
using MISD.SZMDA.Member.Runtime.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISD.SZMDA.Member.Tests
{
    /// <summary>
    /// 所有测试集成的基础类
    /// </summary>
    /// <typeparam name="TStartupModule"></typeparam>
    public abstract class IntegratedTestBase<TStartupModule> : IDisposable
        where TStartupModule : BaseModule
    {
        protected IIocManager LocalIocManager { get; }

        protected Bootstrapper Bootstrapper { get; }

        //protected TestAbpSession AbpSession { get; private set; }

        public virtual void Dispose()
        {
            Bootstrapper.Dispose();
            LocalIocManager.Dispose();
        }


        protected IntegratedTestBase(bool _initialize = true)
        {
            LocalIocManager = new IocManager();
            Bootstrapper = Bootstrapper.Create<TStartupModule>(LocalIocManager);

            if (_initialize)
            {
                IntegratedInitialize();
            }
        }

        protected void IntegratedInitialize()
        {
            //LocalIocManager.Register<IAbpSession, TestAbpSession>();

            PreInitialize();

            Bootstrapper.Initialize();

            //AbpSession = LocalIocManager.Resolve<TestAbpSession>();
        }


        protected virtual void PreInitialize()
        {

        }


        protected T Resolve<T>()
        {
            EnsureClassRegistered(typeof(T));
            return LocalIocManager.Resolve<T>();
        }

        protected T Resolve<T>(object argumentsAsAnonymousType)
        {
            EnsureClassRegistered(typeof(T));
            return LocalIocManager.Resolve<T>(argumentsAsAnonymousType);
        }

        protected object Resolve(Type type)
        {
            EnsureClassRegistered(type);
            return LocalIocManager.Resolve(type);
        }

        protected object Resolve(Type type, object argumentsAsAnonymousType)
        {
            EnsureClassRegistered(type);
            return LocalIocManager.Resolve(type, argumentsAsAnonymousType);
        }

        protected void EnsureClassRegistered(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Transient)
        {
            if (!LocalIocManager.IsRegistered(type))
            {
                if (!type.IsClass || type.IsAbstract)
                {
                    throw new BaseException("Can not register " + type.Name + ". It should be a non-abstract class. If not, it should be registered before.");
                }

                LocalIocManager.Register(type, lifeStyle);
            }
        }
    }
}
