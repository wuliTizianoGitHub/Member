using MISD.SZMDA.Member.Core.Entities;
using MISD.SZMDA.Member.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISD.SZMDA.Member.Application.Service
{
    public class ConfigAppService
    {
        private readonly IConfigRepository _configRepository;

        public ConfigAppService(IConfigRepository configRepository)
        {
            _configRepository = configRepository;
        }

        public List<MInfo_Config> getall()
        {
            return _configRepository.GetAll1();
        }
    }
}
