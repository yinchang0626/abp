using System;
using FluentNHibernate.Cfg;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp.NHibernate.DependencyInjection
{
    public class AbpNHibernateDbContextRegistrationOptions : AbpCommonDbContextRegistrationOptions, IAbpNHibernateDbContextRegistrationOptionsBuilder
    {
        public FluentConfiguration FluentConfiguration { get; }
        public AbpNHibernateDbContextRegistrationOptions(Type originalDbContextType, IServiceCollection services)
            : base(originalDbContextType, services)
        {
            FluentConfiguration = Fluently
                .Configure();

        }
    }
}