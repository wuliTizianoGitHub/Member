using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MISD.SZMDA.Member.Runtime.Dependency;
using MISD.SZMDA.Member.Runtime.NHibernateCore;

namespace MISD.SZMDA.Member.NHibernate
{
    public class MemberDataModule : NHibernateCoreModule
    {
        //public MemberDataModule(string nameOrconnectionString) : base(nameOrconnectionString)
        //{
        //    nameOrconnectionString = "Data Source = 192.168.2.248; Initial Catalog = MISD.SZMDA.OWS.Information; User ID = sa; Password = ydwl@248";
        //}

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
