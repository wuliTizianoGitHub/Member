using MISD.SZMDA.Member.Runtime.Dependency;
using System;
using System.Collections.Generic;

namespace MISD.SZMDA.Member.Runtime.Configuration
{
    // <summary>
    /// 配置startup以及模块
    /// </summary>
    internal class StartupConfiguration : DictionaryBasedConfig, IStartupConfiguration
    {
        /// <summary>
        /// Reference to the IocManager.
        /// </summary>
        public IIocManager IocManager { get; }

        public StartupConfiguration(IIocManager iocManager)
        {
            IocManager = iocManager;
        }

        /// <summary>
        /// 配置默认连接字符串用于ORM模块
        /// 它可以是应用程序中的配置文件的连接字符串的名字，也可以是一个完整的连接字符串
        /// </summary>
        public string DefaultNameOrConnectionString { get; set; }

        /// <summary>
        /// 用于配置模块
        /// </summary>
        public IModuleConfigurations Modules { get; private set; }


        public Dictionary<Type, Action> ServiceReplaceActions { get; private set; }

        //todo:一些配置属性


        public void Initialize()
        {
            //todo: 初始化上述的配置属性
            ServiceReplaceActions = new Dictionary<Type, Action>();
            //注册模块配置
            Modules = IocManager.Resolve<IModuleConfigurations>();
        }

        public void ReplaceService(Type type, Action replaceAction)
        {
            ServiceReplaceActions[type] = replaceAction;
        }

        public T Get<T>()
        {
            return GetOrCreate(typeof(T).FullName, () => IocManager.Resolve<T>());
        }


    }
}
