using FluentNHibernate.Mapping;
using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.TestApp.Domain;

namespace Volo.Abp.TestApp.NHibernate.FluentMapping
{
    public class EntityWithIntPkMap : ClassMap<EntityWithIntPk>
    {
        public EntityWithIntPkMap()
        {
            Table(@"EntityWithIntPk");
            LazyLoad();
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.Name);
        }
    }
}
