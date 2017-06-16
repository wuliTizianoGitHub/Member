using MISD.SZMDA.Member.Runtime.Configuration;

namespace MISD.SZMDA.Member.Runtime.NHibernateCore.Configuration.Startup
{
    /// <summary>
    /// 为<see cref="IModuleConfigurations"/>定义扩展方法，让其允许配置NHibernate模块
    /// </summary>
    public static class NHibernateConfigurationExtensions
    {
        /// <summary>
        /// Used to configure ABP NHibernate module.
        /// </summary>
        public static INHibernateModuleConfiguration NHibernateConfig(this IModuleConfigurations configurations)
        {
            return configurations.StartupConfiguration.Get<INHibernateModuleConfiguration>();
        }
    }
}
