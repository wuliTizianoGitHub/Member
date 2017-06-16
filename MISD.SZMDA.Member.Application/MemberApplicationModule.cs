using MISD.SZMDA.Member.Runtime.NHibernateCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISD.SZMDA.Member.Application
{
    public class MemberApplicationModule : NHibernateCoreModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
