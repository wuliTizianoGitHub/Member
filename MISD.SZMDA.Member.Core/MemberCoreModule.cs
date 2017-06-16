using MISD.SZMDA.Member.Runtime.NHibernateCore;
using System.Reflection;

namespace MISD.SZMDA.Member.Core
{
    public class MemberCoreModule : NHibernateCoreModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
