using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NHibernate;
using System.Data.Common;
using Volo.Abp.Data;
using Volo.Abp.NHibernate;
using Volo.Abp.NHibernate.Filters;

namespace Volo.Abp.Uow.NHibernate
{
    public class UnitOfWorkDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : IDbContext
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IConnectionStringResolver _connectionStringResolver;
        private readonly DbContextOptions<TDbContext> _dbContextOptions;


        public UnitOfWorkDbContextProvider(
            //1.Init from TOptions instances(Configure<DbContextOptions<TDbContext>>)
            //2.Get from DbContextOptionsFactory Factory method
            //3.Init fluentConfiguration from DbContextRegistrationOptionsBuilder
            //4.Setup Configuration
            //5.Finall BuildSessionFactory and cache in DbContextOptions
            //6.UnitOfWork use BuildSessionFactory to create session
            //7.Push UnitOfWork to Repository
            DbContextOptions<TDbContext> dbContextOptions,
            IUnitOfWorkManager unitOfWorkManager,
            IConnectionStringResolver connectionStringResolver)
        {

            _dbContextOptions = dbContextOptions;
            _unitOfWorkManager = unitOfWorkManager;
            _connectionStringResolver = connectionStringResolver;
        }

        public TDbContext GetDbContext()
        {
            var unitOfWork = _unitOfWorkManager.Current;
            if (unitOfWork == null)
            {
                throw new AbpException($"A DbContext instance can only be created inside a unit of work!");
            }
            var connectionStringName = ConnectionStringNameAttribute.GetConnStringName<TDbContext>();
            var connectionString = _connectionStringResolver.Resolve(connectionStringName);
            var dbContextKey = $"{typeof(TDbContext).FullName}_{connectionString}";

            var databaseApi = unitOfWork.GetOrAddDatabaseApi(
                dbContextKey,
                () =>
                {
                    var dbContext = CreateDbContext(unitOfWork, dbContextKey, connectionString);

                    return new DbDatabaseApi<TDbContext>(dbContext);
                });
            return ((DbDatabaseApi<TDbContext>)databaseApi).DbContext;
        }
        private TDbContext CreateDbContext(IUnitOfWork unitOfWork, string dbContextKey, string connectionString)
        {
            _dbContextOptions
                .FluentConfiguration
                .Mappings(m =>
                {
                    m.FluentMappings.AddFromAssemblyOf<AbpNHibernateModule>();
                })
                .Database(_dbContextOptions.UseDataBase(connectionString));
            var dbContext = unitOfWork.ServiceProvider.GetRequiredService<TDbContext>();
            dbContext.InitializeSession(_dbContextOptions.Session);
            if (unitOfWork.Options.IsTransactional)
            {
                WithTransaction(unitOfWork, dbContext, dbContextKey);
            }

            return dbContext;
        }
        private void WithTransaction(IUnitOfWork unitOfWork, TDbContext dbContext, string dbContextKey)
        {
            var transactionApiKey = dbContextKey;
            var activeTransaction = unitOfWork.FindTransactionApi(transactionApiKey) as TransactionApi;

            if (activeTransaction == null)
            {
                var dbtransaction = unitOfWork.Options.IsolationLevel.HasValue
                    ? dbContext.Session.BeginTransaction(unitOfWork.Options.IsolationLevel.Value)
                    : dbContext.Session.BeginTransaction();

                unitOfWork.AddTransactionApi(
                    transactionApiKey,
                    new TransactionApi(
                        dbtransaction,
                        dbContext
                    )
                );
            }
            else
            {
                //todo nest  transaction??
            }
        }


    }
}