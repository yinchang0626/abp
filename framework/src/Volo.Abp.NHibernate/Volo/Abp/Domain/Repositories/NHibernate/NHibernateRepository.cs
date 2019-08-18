using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using NHibernate;
using Volo.Abp.Domain.Entities;
using Volo.Abp.NHibernate;

namespace Volo.Abp.Domain.Repositories.NHibernate
{
    public class NHibernateRepository<TDbContext, TEntity> : RepositoryBase<TEntity>, INHibernateRepository<TEntity>
        where TDbContext : IDbContext
        where TEntity : class, IEntity
    {
        protected virtual TDbContext DbContext => _dbContextProvider.GetDbContext();

        public ISession Session => DbContext.Session;

        private readonly IDbContextProvider<TDbContext> _dbContextProvider;

        public NHibernateRepository(IDbContextProvider<TDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        protected override IQueryable<TEntity> GetQueryable()
        {
            return Session.Query<TEntity>();
        }

        public override TEntity Insert(TEntity entity, bool autoSave = false)
        {
            Session.Save(entity);
            if (autoSave)
                Session.Flush();
            return entity;
        }

        public override TEntity Update(TEntity entity, bool autoSave = false)
        {
            Session.Update(entity);
            if (autoSave)
                Session.Flush();
            return entity;
        }

        public override void Delete(TEntity entity, bool autoSave = false)
        {
            if (entity is ISoftDelete)
            {
                (entity as ISoftDelete).IsDeleted = true;
                Update(entity);
            }
            else
            {
                Session.Delete(entity);
            }
            if (autoSave)
                Session.Flush();
        }

        public override List<TEntity> GetList(bool includeDetails = false)
        {
            return Session.Query<TEntity>().ToList();
        }

        public override long GetCount()
        {
            return Session.Query<TEntity>().Count();
        }
    }

    public class NHibernateRepository<TDbContext, TEntity, TKey> : NHibernateRepository<TDbContext, TEntity>,
        INHibernateRepository<TEntity, TKey>,
        ISupportsExplicitLoading<TEntity, TKey>

        where TDbContext : IDbContext
        where TEntity : class, IEntity<TKey>
    {
        public NHibernateRepository(IDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public void Delete(TKey id, bool autoSave = false)
        {
            var entity = Find(id, includeDetails: false);
            if (entity == null)
            {
                return;
            }

            Delete(entity, autoSave);
        }

        public async Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = await FindAsync(id, includeDetails: false, cancellationToken: cancellationToken);
            if (entity == null)
            {
                return;
            }
            await this.Session.DeleteAsync(entity, cancellationToken);
        }

        public Task EnsureCollectionLoadedAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression, CancellationToken cancellationToken) where TProperty : class
        {
            //todo??
            throw new NotImplementedException();
        }

        public Task EnsurePropertyLoadedAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> propertyExpression, CancellationToken cancellationToken) where TProperty : class
        {
            //todo??
            throw new NotImplementedException();
        }

        public TEntity Find(TKey id, bool includeDetails = true)
        {
            return Session.Get<TEntity>(id);
        }

        public async Task<TEntity> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Session.GetAsync<TEntity>(id, cancellationToken);
        }

        public TEntity Get(TKey id, bool includeDetails = true)
        {
            var entity = Find(id, includeDetails);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        public async Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = await FindAsync(id, includeDetails, GetCancellationToken(cancellationToken));

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

    }
}
