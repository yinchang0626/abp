using Volo.Abp.NHibernate;

namespace Volo.Abp.EntityFrameworkCore
{
    public abstract class NHibernateTestBase : AbpIntegratedTest<AbpNHibernateTestModule>
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}
