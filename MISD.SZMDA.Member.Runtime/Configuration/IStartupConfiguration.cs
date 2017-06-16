using MISD.SZMDA.Member.Runtime.Dependency;
using System;

namespace MISD.SZMDA.Member.Runtime.Configuration
{
    /// <summary>
    /// 配置startup以及模块
    /// </summary>
    public interface IStartupConfiguration : IDictionaryBasedConfig
    {
        /// <summary>
        /// 获取IOC管理器关联配置
        /// </summary>
        IIocManager IocManager { get; }

        /// <summary>
        /// 配置默认连接字符串用于ORM模块
        /// 它可以是应用程序中的配置文件的连接字符串的名字，也可以是一个完整的连接字符串
        /// </summary>
        string DefaultNameOrConnectionString { get; set; }

        IModuleConfigurations Modules { get; }


        //TODO： some configuration property in this place...


        /// <summary>
        /// 用于替换服务类型，获取replaceAction为这个类型注册一个实现
        /// </summary>
        /// <param name="type"></param>
        /// <param name="replaceAction"></param>
        void ReplaceService(Type type, Action replaceAction);


        /// <summary>
        /// get configuration objects
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>();

    }
}
