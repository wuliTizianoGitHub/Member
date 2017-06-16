using MISD.SZMDA.Member.Runtime.Exceptions;
using MISD.SZMDA.Member.Runtime.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MISD.SZMDA.Member.Runtime.Modules
{
    /// <summary>
    /// 用于存储ModuleInfo对象的集合
    /// </summary>
    public class ModuleCollection : List<ModuleInfo>
    {
        public Type StartupModuleType { get; }

        public ModuleCollection(Type startupModuleType)
        {
            StartupModuleType = startupModuleType;
        }

        /// <summary>
        /// 获取模块实例
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        /// <returns></returns>
        public TModule GetModule<TModule>()
            where TModule : BaseModule
        {
            var module = this.FirstOrDefault(m => m.Type == typeof(TModule));
            if (module == null)
            {
                throw new BaseException("Can not find module for " + typeof(TModule).FullName);
            }

            return (TModule)module.Instance;
        }

        /// <summary>
        /// 依赖排序
        /// 如果模块A依赖于模块B，A之后的B也在返回列表中
        /// </summary>
        /// <returns></returns>
        public List<ModuleInfo> GetSortedModuleListByDependency()
        {
            //获取到排序后的Module
            var sortedModules = this.SortByDependencies(x => x.Dependencies);
            EnsureKernelModuleToBeFirst(sortedModules);
            EnsureStartupModuleToBeLast(sortedModules, StartupModuleType);
            return sortedModules;
        }

        /// <summary>
        /// 确保KernelModule是第一个模块
        /// </summary>
        /// <param name="modules"></param>
        public static void EnsureKernelModuleToBeFirst(List<ModuleInfo> modules)
        {
            var kernelModuleIndex = modules.FindIndex(m => m.Type == typeof(KernelModule));
            if (kernelModuleIndex <= 0)
            {
                //是第一个模块
                return;
            }

            var kernelModule = modules[kernelModuleIndex];
            modules.RemoveAt(kernelModuleIndex);
            modules.Insert(0, kernelModule);
        }

        /// <summary>
        /// 确保StartupModule是最后一个模块
        /// </summary>
        /// <param name="modules"></param>
        /// <param name="startupModuleType"></param>
        public static void EnsureStartupModuleToBeLast(List<ModuleInfo> modules, Type startupModuleType)
        {
            var startupModuleIndex = modules.FindIndex(m => m.Type == startupModuleType);

            if (startupModuleIndex >= modules.Count - 1)
            {
                //是最后一个模块
                return;
            }

            var startupModule = modules[startupModuleIndex];

            modules.RemoveAt(startupModuleIndex);
            modules.Add(startupModule);
        }

        public void EnsureKernelModuleToBeFirst()
        {
            EnsureKernelModuleToBeFirst(this);
        }

        public void EnsureStartupModuleToBeLast()
        {
            EnsureStartupModuleToBeLast(this, StartupModuleType);
        }
    }
}
