using Castle.Core.Logging;
using MISD.SZMDA.Member.Runtime.Configuration;
using MISD.SZMDA.Member.Runtime.Dependency;
using MISD.SZMDA.Member.Runtime.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace MISD.SZMDA.Member.Runtime.Modules
{
    /// <summary>
    /// 用于管理模块
    /// </summary>
    public class ModuleManager : IModuleManager
    {
        /// <summary>
        /// 启动模块
        /// </summary>
        public ModuleInfo StartupModule { get; private set; }


        //不可变集合
        public IReadOnlyList<ModuleInfo> Modules => _modules.ToImmutableList();

        /// <summary>
        /// 日志对象
        /// </summary>
        public ILogger Logger { get; set; }



        private ModuleCollection _modules;

        private readonly IIocManager _iocManager;

        public ModuleManager(IIocManager iocManager)
        {
            _iocManager = iocManager;
            Logger = NullLogger.Instance;
        }


        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="startupModule"></param>
        public virtual void Initialize(Type startupModule)
        {
            //获取模块
            _modules = new ModuleCollection(startupModule);
            LoadAllModules();
        }

        /// <summary>
        /// 模块开始
        /// </summary>
        public virtual void StartModules()
        {
            //使用拓扑排序根据依赖排序
            var sortedModules = _modules.GetSortedModuleListByDependency();

            //依次执行PreInitialize()，Initialize()，PostInitialize()方法
            sortedModules.ForEach(module=>module.Instance.PreInitialize());
            sortedModules.ForEach(module => module.Instance.Initialize());
            sortedModules.ForEach(module => module.Instance.PostInitialize());
        }

        /// <summary>
        /// 模块结束
        /// </summary>
        public virtual void ShutdownModules()
        {
            Logger.Debug("Shutting down has been started");
            //排序，翻转，关闭模块
            var sortedModules = _modules.GetSortedModuleListByDependency();
            sortedModules.Reverse();
            sortedModules.ForEach(sm => sm.Instance.Shutdown());
            Logger.Debug("Shutting down completed.");
        }


        /// <summary>
        /// 加载所有模块
        /// </summary>
        private void LoadAllModules()
        {
            Logger.Debug("Loading Abp modules...");

            var moduleTypes = FindAllModules().Distinct().ToList();

            Logger.Debug("Found " + moduleTypes.Count + " modules in total.");

            RegisterModules(moduleTypes);
            CreateModules(moduleTypes);

            _modules.EnsureKernelModuleToBeFirst();
            _modules.EnsureStartupModuleToBeLast();

            SetDependencies();

            Logger.DebugFormat("{0} modules loaded.", _modules.Count);
        }

        /// <summary>
        /// 设置依赖
        /// </summary>
        private void SetDependencies()
        {
            foreach (var moduleInfo in _modules)
            {
                //清理依赖项
                moduleInfo.Dependencies.Clear();

                foreach (var dependedModuleType in BaseModule.FindDependedModuleTypes(moduleInfo.Type))
                {
                    //
                    var dependedModuleInfo = _modules.FirstOrDefault(m => m.Type == dependedModuleType);

                    if (dependedModuleInfo == null)
                    {
                        throw new InitializationException("Could not find a depended module " + dependedModuleType.AssemblyQualifiedName + " for " + moduleInfo.Type.AssemblyQualifiedName);
                    }

                    if ((moduleInfo.Dependencies.FirstOrDefault(m=>m.Type == dependedModuleType)) == null)
                    {
                        //添加依赖
                        moduleInfo.Dependencies.Add(dependedModuleInfo);
                    }
                }
            }
        }

        /// <summary>
        /// 创建模块
        /// </summary>
        /// <param name="moduleTypes"></param>
        private void CreateModules(ICollection<Type> moduleTypes)
        {
            foreach (var moduleType in moduleTypes)
            {
                //解析模块
                var moduleObject = _iocManager.Resolve(moduleType) as BaseModule;

                if (moduleObject == null)
                {
                    throw new InitializationException("This type is not an module: " + moduleType.AssemblyQualifiedName);
                }

                moduleObject.IocManager = _iocManager;

                moduleObject.Configuration = _iocManager.Resolve<IStartupConfiguration>();

                var moduleInfo = new ModuleInfo(moduleType,moduleObject);

                _modules.Add(moduleInfo);

                if (moduleType == _modules.StartupModuleType)
                {
                    StartupModule = moduleInfo;
                }

                Logger.DebugFormat("Loaded module: " + moduleType.AssemblyQualifiedName);
            }
        }

        /// <summary>
        /// 注册模块
        /// </summary>
        /// <param name="moduleTypes"></param>
        private void RegisterModules(ICollection<Type> moduleTypes)
        {
            foreach (var moduleType in moduleTypes)
            {
                _iocManager.RegisterIfNot(moduleType);
            }
        }

        /// <summary>
        /// 查找所有模块
        /// </summary>
        /// <returns></returns>
        private List<Type> FindAllModules()
        {
            var modules = BaseModule.FindDependedModuleTypesRecursivelyIncludingGivenModule(_modules.StartupModuleType);

            return modules;
        }
    }
}
