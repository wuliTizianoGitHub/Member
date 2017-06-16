using MISD.SZMDA.Member.Runtime.Dependency;
using System;

namespace MISD.SZMDA.Member.Tests
{
    public abstract class TestBaseWithLocalIocManager : IDisposable
    {
        protected IIocManager LocalIocManager;

        protected TestBaseWithLocalIocManager()
        {
            LocalIocManager = new IocManager();
        }

        public void Dispose()
        {
            LocalIocManager.Dispose();
        }
    }
}
