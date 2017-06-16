using FluentNHibernate.Cfg.Db;
using MISD.SZMDA.Member.Runtime.Modules;
using MISD.SZMDA.Member.Runtime.NHibernateCore;
using MISD.SZMDA.Member.Runtime.NHibernateCore.Configuration.Startup;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISD.SZMDA.Member.Tests
{
    [DependsOn(typeof(NHibernateCoreModule),typeof(TestBaseModule))]
    public class NHibernateTestModule : BaseModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.NHibernateConfig().FluentConfiguration
                .Database(SQLiteConfiguration.Standard.InMemory())
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Execute(true, true, false, IocManager.Resolve<IDbConnection>(), Console.Out));
        }
    }
}
