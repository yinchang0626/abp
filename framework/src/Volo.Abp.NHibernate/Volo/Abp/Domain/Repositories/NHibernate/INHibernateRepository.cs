using NHibernate;
using Volo.Abp.Domain.Entities;
using Volo.Abp.NHibernate;

namespace Volo.Abp.Domain.Repositories.NHibernate
{
    public interface INHibernateRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        ISession Session { get; }
    }

    public interface INHibernateRepository<TEntity, TKey> : INHibernateRepository<TEntity>, IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {

    }
}