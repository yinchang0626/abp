using FluentNHibernate.Mapping;
using NHibernate;

namespace Volo.Abp.NHibernate.Filters
{
    /// <summary>
    /// Add filter SoftDelete 
    /// </summary>
    public class SoftDeleteFilter : FilterDefinition
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SoftDeleteFilter()
        {
            var name = nameof(ISoftDelete);
            var columnName = nameof(ISoftDelete.IsDeleted);
            WithName(name)
                .AddParameter(columnName, NHibernateUtil.Boolean)
                .WithCondition($"{columnName} = :{columnName}");
        }
    }
}