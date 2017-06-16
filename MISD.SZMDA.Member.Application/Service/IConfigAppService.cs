using MISD.SZMDA.Member.Core.Entities;
using MISD.SZMDA.Member.Runtime.Application.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISD.SZMDA.Member.Application.Service
{
    public interface IConfigAppService  : IApplicationService
    {
        List<MInfo_Config> getall();
    }
}
