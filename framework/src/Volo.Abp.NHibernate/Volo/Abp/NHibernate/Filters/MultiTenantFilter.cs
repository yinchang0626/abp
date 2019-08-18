using FluentNHibernate.Mapping;
using NHibernate;
using Volo.Abp.MultiTenancy;

namespace Volo.Abp.NHibernate.Filters
{
    /// <summary>
    /// Add filter MustHaveTenant 
    /// </summary>
    public class MultiTenantFilter : FilterDefinition
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MultiTenantFilter()
        {
            var name = nameof(IMultiTenant);
            var columnName = nameof(IMultiTenant.TenantId);
            WithName(name)
                .AddParameter(columnName, NHibernateUtil.Guid)
                .WithCondition($"{columnName} = :{columnName}");
        }
    }
}