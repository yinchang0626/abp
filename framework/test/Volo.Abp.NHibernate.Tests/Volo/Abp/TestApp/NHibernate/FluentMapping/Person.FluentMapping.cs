using FluentNHibernate.Mapping;
using System;
using System.Collections.ObjectModel;
using Volo.Abp.AutoMapper;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TestApp.Domain;

namespace Volo.Abp.TestApp.NHibernate.FluentMapping
{
    
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Table(@"Person");
            LazyLoad();
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.TenantId);
            Map(x => x.CityId);
            Map(x => x.Name);
            Map(x => x.Age);
            HasMany<Phone>(x => x.Phones)
              .Access.Property()
              .AsBag()
              //at create Product,create ProductTranslations together
              .Cascade.AllDeleteOrphan().Inverse()
              .LazyLoad()
              // .OptimisticLock.Version() /*bug (or missing feature) in Fluent NHibernate*/
              .Generic()
              .KeyColumns.Add("PersonId", mapping => mapping.Name("PersonId").Not.Nullable());
        }
    }
}