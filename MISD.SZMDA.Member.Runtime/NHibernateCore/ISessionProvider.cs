using NHibernate;

namespace MISD.SZMDA.Member.Runtime.NHibernateCore
{
    public interface ISessionProvider
    {
        ISession Session { get; }
    }
}
