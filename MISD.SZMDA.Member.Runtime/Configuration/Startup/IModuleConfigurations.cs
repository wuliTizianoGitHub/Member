namespace MISD.SZMDA.Member.Runtime.Configuration.Startup
{
    /// <summary>
    /// 用于提供配置模块的方法。
    /// 这个类创建扩展方法用于结束<see cref="IStartupConfiguration.Modules"/>对象
    /// </summary>
    public interface IModuleConfigurations
    {
        /// <summary>
        /// 获取配置对象
        /// </summary>
        IStartupConfiguration StartupConfiguration { get; }
    }
}
