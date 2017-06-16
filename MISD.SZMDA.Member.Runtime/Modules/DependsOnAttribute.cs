using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISD.SZMDA.Member.Runtime.Modules
{

    /// <summary>
    /// 用于定义模块与其他模块的依赖关系。它应该用于一个派生自<see cref="BaseModule"/>的类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = true)]
    public class DependsOnAttribute : Attribute
    {
        /// <summary>
        /// 依赖模块类型数组
        /// </summary>
        public Type[] DependedModuleTypes { get; private set; }

        /// <summary>
        /// 定义模块与其他模块的依赖关系
        /// </summary>
        /// <param name="dependedModuleTypes"></param>
        public DependsOnAttribute(params Type[] dependedModuleTypes)
        {
            DependedModuleTypes = dependedModuleTypes;
        }
    }
}
