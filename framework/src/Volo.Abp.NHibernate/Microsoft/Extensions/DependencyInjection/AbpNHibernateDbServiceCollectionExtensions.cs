using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using Volo.Abp.NHibernate;
using Volo.Abp.NHibernate.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AbpNHibernateDbServiceCollectionExtensions
    {
        public static IServiceCollection AddAbpNhibernateDbContext<TAbpNHibernateDbContext>(
            this IServiceCollection services,
            Action<IDbContextRegistrationOptionsBuilder> optionsBuilder = null) //Created overload instead of default parameter
            where TAbpNHibernateDbContext : DbContext<TAbpNHibernateDbContext>
        {
            var options = new DbContextRegistrationOptionsBuilder(typeof(TAbpNHibernateDbContext), services);
            
            optionsBuilder?.Invoke(options);

            services.TryAddTransient(x => {
                return DbContextOptionsFactory.Create<TAbpNHibernateDbContext>(x, options.FluentConfiguration);
            });

            foreach (var dbContextType in options.ReplacedDbContextTypes)
            {
                services.Replace(ServiceDescriptor.Transient(dbContextType, typeof(TAbpNHibernateDbContext)));
            }
           
            new DbRepositoryRegistrar(options).AddRepositories();

            return services;
        }
    }
}
