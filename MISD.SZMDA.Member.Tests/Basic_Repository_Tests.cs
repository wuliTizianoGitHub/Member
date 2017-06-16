using MISD.SZMDA.Member.Runtime.Domain.Repositories;
using MISD.SZMDA.Member.Tests.Entities;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MISD.SZMDA.Member.Tests
{
    public class Basic_Repository_Tests : NHibernateTestBase
    {
        private readonly IRepository<MInfo_Config> _configRepository;

        public Basic_Repository_Tests()
        {
            _configRepository = Resolve<IRepository<MInfo_Config>>();
            UsingSession(session => session.Save(new MInfo_Config() { MC_Content="智障"}));
        }

        [Fact]
        public void Should_Get_All_Config()
        {
            _configRepository.GetAllList().Count.ShouldBe(1);
        }
    }
}
