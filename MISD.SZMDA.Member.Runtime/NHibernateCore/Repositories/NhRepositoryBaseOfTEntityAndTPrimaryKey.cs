using MISD.SZMDA.Member.Runtime.Domain.Entities;
using MISD.SZMDA.Member.Runtime.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;
using System.Linq;

namespace MISD.SZMDA.Member.Runtime.NHibernateCore.Repositories
{
    public class NhRepositoryBase<TEntity, TPrimaryKey> : RepositoryBase<TEntity, TPrimaryKey> where TEntity : class, IEntity
    {
        public virtual ISession Session { get { return _sessionProvider.Session; } }

        private readonly ISessionProvider _sessionProvider;

        //private readonly ISession _session;

        public NhRepositoryBase(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        public override IQueryable<TEntity> GetAll()
        {
            return Session.Query<TEntity>();
        }

        public override TEntity FirstOrDefault(TPrimaryKey id)
        {
            return Session.Get<TEntity>(id);
        }

        public override TEntity Insert(TEntity entity)
        {
            Session.Save(entity);
            return entity;
        }

        public override TEntity Update(TEntity entity)
        {
            Session.Update(entity);
            return entity;
        }

        public override TEntity Load(TPrimaryKey id)
        {
            return Session.Load<TEntity>(id);
        }

        public override void Delete(TEntity entity)
        {
            Session.Delete(entity);
        }

        public override void Delete(TPrimaryKey id)
        {
            Delete(Session.Load<TEntity>(id));
        }
    }
}
