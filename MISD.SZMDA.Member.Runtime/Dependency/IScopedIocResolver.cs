using System;

namespace MISD.SZMDA.Member.Runtime.Dependency
{
    /// <summary>
    /// 用于在单个引用状态下包装一批解析的范围，它继承了<see cref="IDisposable" /> 和 <see cref="IIocResolver" />，所以解析完对象之后释放IocResolver
    /// </summary>
    public interface IScopedIocResolver : IIocResolver, IDisposable
    {

    }
}
