using MISD.SZMDA.Member.Runtime.Domain.Entities;

namespace MISD.SZMDA.Member.Runtime.Domain.Repositories
{
    public interface IRepository<TEntity>:IRepository<TEntity,long> where TEntity:class,IEntity
    {

    }
}
