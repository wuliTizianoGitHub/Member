using FluentNHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISD.SZMDA.Member.Runtime.NHibernateCore.Configuration
{
    public interface INHibernateModuleConfiguration
    {
        FluentConfiguration FluentConfiguration { get; }
    }
}
