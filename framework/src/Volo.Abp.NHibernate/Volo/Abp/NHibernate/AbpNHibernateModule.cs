using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NHibernate;
using Volo.Abp.Data;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;
using Volo.Abp.Uow.NHibernate;

namespace Volo.Abp.NHibernate
{
    [DependsOn(typeof(AbpDddDomainModule))]
    public class AbpNHibernateModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            
            context.Services.TryAddTransient(
                typeof(IDbContextProvider<>),
                typeof(UnitOfWorkDbContextProvider<>)
            );

        }
    }
}
