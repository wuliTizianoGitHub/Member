using Castle.Core.Logging;
using MISD.SZMDA.Member.Runtime.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MISD.SZMDA.Member.Runtime.Reflection
{
    public class TypeFinder : ITypeFinder
    {

        public ILogger Logger { get; set; }

        private readonly IAssemblyFinder _assemblyFinder;

        private readonly object _syncObj = new object();

        private Type[] _types;

        public TypeFinder(IAssemblyFinder assemblyFinder)
        {
            _assemblyFinder = assemblyFinder;

            Logger = NullLogger.Instance;
        }

        public Type[] Find(Func<Type, bool> predicate)
        {
            return GetAllTypes().Where(predicate).ToArray();
        }

        public Type[] FindAll()
        {
            return GetAllTypes().ToArray();
        }


        private Type[] GetAllTypes()
        {
            if (_types == null)
            {
                lock (_syncObj)
                {
                    if (_types == null)
                    {
                        _types = CreateTypeList().ToArray();
                    }
                }
            }
            return _types;
        }


        /// <summary>
        /// 创建类型集合
        /// </summary>
        /// <returns></returns>
        private List<Type> CreateTypeList()
        {
            var allTypes = new List<Type>();

            //使用assembly查找器查找所有的assembly
            var assemblies = _assemblyFinder.GetAllAssemblies().Distinct();

            foreach (var assembly in assemblies)
            {
                try
                {
                    Type[] typesInThisAssembly;
                    try
                    {
                        typesInThisAssembly = assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException ex)
                    {

                        typesInThisAssembly = ex.Types;
                    }

                    if (typesInThisAssembly.IsNullOrEmpty())
                    {
                        continue;
                    }
                    allTypes.AddRange(typesInThisAssembly.Where(type => type != null));

                }
                catch (System.Exception ex)
                {

                    Logger.Warn(ex.ToString(), ex);
                }
            }
            return allTypes;
        }
    }
}
