using MISD.SZMDA.Member.Runtime.Tools;
using System;
using System.Collections.Generic;

namespace MISD.SZMDA.Member.Runtime.Configuration
{
    /// <summary>
    /// 用于获取或者设置自定义配置
    /// </summary>
    public class DictionaryBasedConfig : IDictionaryBasedConfig
    {

        /// <summary>
        /// 自定义配置
        /// </summary>
        protected Dictionary<string, object> CustomSettings { get; private set; }

        public DictionaryBasedConfig()
        {
            CustomSettings = new Dictionary<string, object>();
        }

        /// <summary>
        /// 索引器，获取或者设置一个配置的值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object this[string name]
        {
            get { return CustomSettings.GetOrDefault(name); }
            set { CustomSettings[name] = value; }
        }

        /// <summary>
        /// 根据名称获取配置
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object Get(string name)
        {
            return Get(name, null);
        }

        public object Get(string name, object defaultValue)
        {
            var value = this[name];
            if (value == null)
            {
                return defaultValue;
            }
            return this[name];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T Get<T>(string name)
        {
            var value = this[name];
            return value == null ? default(T) : (T)Convert.ChangeType(value, typeof(T));
        }

        public T Get<T>(string name, object defaultValue)
        {
            return (T)Get(name, (object)defaultValue);
        }

        public T GetOrCreate<T>(string name, Func<T> creator)
        {
            var value = Get(name);

            if (value == null)
            {
                value = creator();
                Set(name, value);
            }

            return (T)value;
        }

        public void Set<T>(string name, T value)
        {
            this[name] = value;
        }
    }
}
