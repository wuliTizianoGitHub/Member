using MISD.SZMDA.Member.Core.Entities;
using MISD.SZMDA.Member.Runtime.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISD.SZMDA.Member.Core.IRepositories
{
    public interface IConfigRepository : IRepository<MInfo_Config, int>
    {
        List<MInfo_Config> GetAll1();
    }
}
