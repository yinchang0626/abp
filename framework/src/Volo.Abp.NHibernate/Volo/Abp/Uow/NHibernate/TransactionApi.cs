using NHibernate;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.NHibernate;

namespace Volo.Abp.Uow.NHibernate
{
    public class TransactionApi : ITransactionApi, ISupportsRollback
    {
        public ITransaction DbContextTransaction { get; }
        public IDbContext StarterDbContext { get; }
        //public List<IDbContext> AttendedDbContexts { get; }

        public TransactionApi(ITransaction dbContextTransaction, IDbContext starterDbContext)
        {

            DbContextTransaction = dbContextTransaction;
            StarterDbContext = starterDbContext;
            //AttendedDbContexts = new List<IDbContext>();
        }

        public void Commit()
        {
            DbContextTransaction.Commit();
            //todo ??
            //foreach (var dbContext in AttendedDbContexts)
            //{
            //    if (dbContext.As<DbContext>().HasRelationalTransactionManager())
            //    {
            //        continue; //Relational databases use the shared transaction
            //    }

            //    dbContext.Session.Transaction.Commit();
            //}
        }

        public Task CommitAsync()
        {
            Commit();
            return Task.CompletedTask;
        }

 

        public void Rollback()
        {
            DbContextTransaction.Rollback();
        }

        public Task RollbackAsync(CancellationToken cancellationToken)
        {
            DbContextTransaction.Rollback();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            DbContextTransaction.Dispose();
        }
    }
}