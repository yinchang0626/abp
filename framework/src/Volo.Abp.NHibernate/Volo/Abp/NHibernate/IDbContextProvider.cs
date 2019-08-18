namespace Volo.Abp.NHibernate
{
    public interface IDbContextProvider<out TDbContext>
        where TDbContext : IDbContext
    {
        TDbContext GetDbContext();
    }
}