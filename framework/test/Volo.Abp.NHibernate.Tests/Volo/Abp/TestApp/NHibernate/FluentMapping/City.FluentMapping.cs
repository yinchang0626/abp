using FluentNHibernate.Mapping;
using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.TestApp.Domain;

namespace Volo.Abp.TestApp.NHibernate.FluentMapping
{
    public class CityMap : ClassMap<City>
    {
        public CityMap()
        {
            Table(@"City");
            LazyLoad();
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.Name);
        }
        public string Name { get; set; }
    }
}
