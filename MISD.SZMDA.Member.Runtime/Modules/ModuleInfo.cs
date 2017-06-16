using MISD.SZMDA.Member.Runtime.Attributes;
using MISD.SZMDA.Member.Runtime.Tools;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace MISD.SZMDA.Member.Runtime.Modules
{
    /// <summary>
    /// 该类用于存储一个模块所有需要的信息
    /// </summary>
    public class ModuleInfo
    {
        /// <summary>
        /// 包含模块的定义
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// 模块类型
        /// </summary>
        public Type Type { get; } 

        /// <summary>
        /// 模块实例
        /// </summary>
        public BaseModule Instance { get; }

        /// <summary>
        /// 当前模块的所有依赖项
        /// </summary>
        public List<ModuleInfo> Dependencies { get; }

        /// <summary>
        /// 创建一个新的ModuleInfo对象
        /// </summary>
        public ModuleInfo([NotNull]Type type,[NotNull]BaseModule instance)
        {
            Check.NotNull(type, nameof(type));
            Check.NotNull(instance, nameof(instance));

            Type = type;
            Instance = instance;
            Assembly = Type.Assembly;
            Dependencies = new List<ModuleInfo>();
        }

        public override string ToString()
        {
            return Type.AssemblyQualifiedName ?? Type.FullName;
        }
    }
}
