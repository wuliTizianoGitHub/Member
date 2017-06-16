using MISD.SZMDA.Member.Core.Entities;
using MISD.SZMDA.Member.Runtime.NHibernateCore;
using MISD.SZMDA.Member.Runtime.NHibernateCore.Repositories;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISD.SZMDA.Member.NHibernate.Repositories
{
    public class ConfigRepository : NhRepositoryBase<MInfo_Config,int>
    {
        public ConfigRepository(ISessionProvider sessionProvider)
            : base(sessionProvider)
        {

        }

        public List<MInfo_Config> GetAll1()
        {
            return GetAll().ToList();
        }
    }
}
