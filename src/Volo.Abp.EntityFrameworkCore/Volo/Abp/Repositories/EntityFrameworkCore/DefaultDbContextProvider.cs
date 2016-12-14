using Volo.Abp.EntityFrameworkCore;

namespace Volo.Abp.Repositories.EntityFrameworkCore
{
    public class DefaultDbContextProvider<TDbContext> : IDbContextProvider<TDbContext> 
        where TDbContext : AbpDbContext<TDbContext>
    {
        private readonly TDbContext _dbContext;

        public DefaultDbContextProvider(TDbContext dbContext) //TODO: Should create this dynamically!
        {
            _dbContext = dbContext;
        }

        public TDbContext GetDbContext()
        {
            return _dbContext;
        }
    }
}