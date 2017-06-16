using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using MISD.SZMDA.Member.Runtime.Attributes;
using MISD.SZMDA.Member.Runtime.Configuration;
using MISD.SZMDA.Member.Runtime.Dependency;
using MISD.SZMDA.Member.Runtime.Dependency.Installers;
using MISD.SZMDA.Member.Runtime.Tools;
using MISD.SZMDA.Member.Runtime.Modules;
using System;

namespace MISD.SZMDA.Member.Runtime
{
    /// <summary>
    /// 主类，负责整个系统开始。
    /// 启动时准备依赖注入系统和注册所需要的公共组件。
    /// 它第一次必须初始化和实例化
    /// </summary>
    public class Bootstrapper : IDisposable
    {
        /// <summary>
        /// 在应用程序中获取Startup模块，以及它依赖的其它模块
        /// </summary>
        public Type StartupModule { get; }

        /// <summary>
        /// 获取IIocManager对象以便在此类使用
        /// </summary>
        public IIocManager IocManager { get; }

        /// <summary>
        /// 对象之前是否被销毁
        /// </summary>
        protected bool IsDisposed;

        /// <summary>
        /// 模块管理器
        /// </summary>
        private ModuleManager _moduleManager;

        /// <summary>
        /// 日志
        /// </summary>
        private ILogger _logger;


        /// <summary>
        /// 创建一个新的<see cref="Bootstrapper"/>实例
        /// </summary>
        /// <param name="startupModule">应用程序中的启动模块依赖于其它使用模块，应该继承自<see cref="BaseModule"></param>
        private Bootstrapper([NotNull] Type startupModule)
            : this(startupModule, Dependency.IocManager.Instance)
        {

        }

        /// <summary>
        /// 创建一个新的<see cref="Bootstrapper"/>实例
        /// </summary>
        /// <param name="startupModule">应用程序中的启动模块依赖于其它使用模块，应该继承自<see cref="BaseModule"></param>
        /// <param name="iocManager">IIocManager用于引导系统</param>
        private Bootstrapper([NotNull] Type startupModule, [NotNull] IIocManager iocManager)
        {
            Check.NotNull(startupModule, nameof(startupModule));
            Check.NotNull(iocManager, nameof(iocManager));

            if (!typeof(BaseModule).IsAssignableFrom(startupModule))
            {
                throw new ArgumentException($"{nameof(startupModule)} should be derived from {nameof(BaseModule)}.");
            }

            StartupModule = startupModule;
            IocManager = iocManager;

            _logger = NullLogger.Instance;
        }

        /// <summary>
        /// 创建一个新的<see cref="Bootstrapper"/>实例
        /// </summary>
        /// <typeparam name="TStartupModule">应用程序中的启动模块依赖于其它使用模块，应该继承自<see cref="BaseModule"></typeparam>
        /// <returns></returns>
        public static Bootstrapper Create<TStartupModule>()
            where TStartupModule : BaseModule
        {
            return new Bootstrapper(typeof(TStartupModule));
        }

        /// <summary>
        /// 创建一个新的<see cref="Bootstrapper"/>实例
        /// </summary>
        /// <typeparam name="TStartupModule">应用程序中的启动模块依赖于其它使用模块，应该继承自<see cref="BaseModule"></typeparam>
        /// <param name="iocManager">IIocManager用于引导系统</param>
        /// <returns></returns>
        public static Bootstrapper Create<TStartupModule>([NotNull]IIocManager iocManager)
            where TStartupModule : BaseModule
        {
            return new Bootstrapper(typeof(TStartupModule), iocManager);
        }

        /// <summary>
        /// 创建一个新的<see cref="Bootstrapper"/>实例
        /// </summary>
        /// <param name="startupModule">应用程序中的启动模块依赖于其它使用模块，应该继承自<see cref="BaseModule"></param>
        public static Bootstrapper Create([NotNull]Type startupModule)
        {
            return new Bootstrapper(startupModule);
        }

        /// <summary>
        /// 创建一个新的<see cref="Bootstrapper"/>实例
        /// </summary>
        /// <param name="startupModule">应用程序中的启动模块依赖于其它使用模块，应该继承自<see cref="BaseModule"></param>
        /// <param name="iocManager">IIocManager用于引导系统</param>
        public static Bootstrapper Create([NotNull] Type startupModule, [NotNull] IIocManager iocManager)
        {
            return new Bootstrapper(startupModule, iocManager);
        }


        /// <summary>
        /// 初始化系统
        /// </summary>
        public virtual void Initialize()
        {
            ResolveLogger();

            try
            {
                RegisterBootstrpper();

                //todo： 安装或者解析一些组件
                IocManager.IocContainer.Install(new CoreInstaller());
                IocManager.Resolve<StartupConfiguration>().Initialize();

                //。。。。

                //启动模块
                _moduleManager = IocManager.Resolve<ModuleManager>();
                _moduleManager.Initialize(StartupModule);
                _moduleManager.StartModules();
            }
            catch (System.Exception ex)
            {

                _logger.Fatal(ex.ToString(), ex);
                throw;
            }


        }


        /// <summary>
        ///  解析日志管理器
        /// </summary>
        private void ResolveLogger()
        {
            if (IocManager.IsRegistered<ILoggerFactory>())
            {
                _logger = IocManager.Resolve<ILoggerFactory>().Create(typeof(Bootstrapper));
            }
        }

        /// <summary>
        /// 注册引导
        /// </summary>
        private void RegisterBootstrpper()
        {
            if (!IocManager.IsRegistered<Bootstrapper>())
            {
                IocManager.IocContainer.Register(Component.For<Bootstrapper>().Instance(this));
            }
        }

        /// <summary>
        /// 销毁整个系统
        /// </summary>
        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }
            IsDisposed = true;

            //关闭所有模块
            _moduleManager?.ShutdownModules();
        }
    }
}
