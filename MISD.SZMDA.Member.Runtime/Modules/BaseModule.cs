using Castle.Core.Logging;
using MISD.SZMDA.Member.Runtime.Configuration;
using MISD.SZMDA.Member.Runtime.Dependency;
using MISD.SZMDA.Member.Runtime.Exceptions;
using MISD.SZMDA.Member.Runtime.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MISD.SZMDA.Member.Runtime.Modules
{
    /// <summary>
    /// 根模块，必须被所有定义的模块类所继承
    /// 一个定义的模块类通常位于它自己的assembly中，并且在应用程序开始以及结束中实现了一些模块事件动作
    /// </summary>
    public abstract class BaseModule
    {
        /// <summary>
        /// 引用IOC管理器
        /// </summary>
        protected internal IIocManager IocManager { get; internal set; }

        protected internal IStartupConfiguration Configuration { get; internal set; }

        //日志
        public ILogger Logger { get; set; }

        protected BaseModule()
        {
            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// 第一个事件,
        /// </summary>
        public virtual void PreInitialize()
        {

        }

        /// <summary>
        /// 用于为当前模块注册依赖
        /// </summary>
        public virtual void Initialize()
        {

        }

        /// <summary>
        /// 应用程序startup最后调用
        /// </summary>
        public virtual void PostInitialize()
        {

        }

        /// <summary>
        /// 结束
        /// </summary>
        public virtual void Shutdown()
        {

        }

        public virtual Assembly[] GetAdditionalAssemblies()
        {
            return new Assembly[0];
        }

        /// <summary>
        /// 检查类型是否是模块
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsModule(Type type)
        {
            return type.IsClass && !type.IsAbstract && !type.IsGenericType && typeof(BaseModule).IsAssignableFrom(type);
        }

        /// <summary>
        /// 寻找模块的依赖模块
        /// </summary>
        /// <returns></returns>
        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            if (!IsModule(moduleType))
            {
                throw new InitializationException("This type is not module:"+moduleType.AssemblyQualifiedName);
            }

            var list = new List<Type>();

            if (moduleType.IsDefined(typeof(DependsOnAttribute),true))
            {
                var dependsOnAttributes = moduleType.GetCustomAttributes(typeof(DependsOnAttribute), true).Cast<DependsOnAttribute>();

                foreach (var dependsOnAttribute in dependsOnAttributes)
                {
                    foreach (var dependedModuleType in dependsOnAttribute.DependedModuleTypes)
                    {
                        list.Add(dependedModuleType);
                    }
                }
            }
            return list;
        }


        public static List<Type> FindDependedModuleTypesRecursivelyIncludingGivenModule(Type moduleType)
        {
            var list = new List<Type>();

            AddModuleAndDependciesResursively(list,moduleType);

            list.AddIfNotContains(typeof(KernelModule));

            return list;
        }


        private static void AddModuleAndDependciesResursively(List<Type> modules, Type module)
        {
            if (!IsModule(module))
            {
                throw new InitializationException("This type is not module:" + module.AssemblyQualifiedName);
            }

            if (modules.Contains(module))
            {
                return;
            }
            modules.Add(module);

            var dependenModules = FindDependedModuleTypes(module);

            foreach (var dependenModule in dependenModules)
            {
                AddModuleAndDependciesResursively(modules,dependenModule);
            }

        }

    }
}
