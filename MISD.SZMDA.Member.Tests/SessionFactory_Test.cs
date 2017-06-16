using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MISD.SZMDA.Member.Tests
{
    public class SessionFactory_Test : NHibernateTestBase
    {
        private readonly ISessionFactory _sessionFactory;

        public SessionFactory_Test()
        {
            _sessionFactory = Resolve<ISessionFactory>();
        }

        [Fact]
        public void Should_OpenSession_Work()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                //nothing...
            }
        }
    }
}
