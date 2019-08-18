namespace Volo.Abp.Uow.NHibernate
{
    public class DbDatabaseApi<TAbpNHibernateDbContext> : IDatabaseApi
    {
        public TAbpNHibernateDbContext DbContext { get; }

        public DbDatabaseApi(TAbpNHibernateDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
