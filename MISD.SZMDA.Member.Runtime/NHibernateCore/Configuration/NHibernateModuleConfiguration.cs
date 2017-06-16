using FluentNHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISD.SZMDA.Member.Runtime.NHibernateCore.Configuration
{
    internal class NHibernateModuleConfiguration : INHibernateModuleConfiguration
    {
        public FluentConfiguration FluentConfiguration { get; }

        public NHibernateModuleConfiguration()
        {
            FluentConfiguration = Fluently.Configure();
        }
    }
}
