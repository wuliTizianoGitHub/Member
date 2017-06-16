using System;

namespace MISD.SZMDA.Member.Runtime.Configuration
{
    /// <summary>
    /// 用来操作配置Dictionary
    /// </summary>
    public interface IDictionaryBasedConfig
    {
        /// <summary>
        /// 用于设置一个字符串命名配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">唯一名称</param>
        /// <param name="value"></param>
        void Set<T>(string name, T value);

        /// <summary>
        /// 获取配置对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        object Get(string name);

        /// <summary>
        /// 获取配置对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        T Get<T>(string name);


        /// <summary>
        /// 获取配置对象，带默认值，返回值object
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        object Get(string name, object defaultValue);

        /// <summary>
        /// 获取配置对象，带默认值，返回值自定义
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        T Get<T>(string name, object defaultValue);

        /// <summary>
        /// 获取配置对象， 如果获取不到将使用creator来创建
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        T GetOrCreate<T>(string name, Func<T> creator);
    }
}
