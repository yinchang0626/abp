using System;
using FluentNHibernate.Cfg;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using Volo.Abp.NHibernate.Filters;

namespace Volo.Abp.NHibernate.DependencyInjection
{
    public interface IDbContextRegistrationOptionsBuilder : ICommonDbContextRegistrationOptionsBuilder
    {
        FluentConfiguration FluentConfiguration { get; }

    }
    public class DbContextRegistrationOptionsBuilder : CommonDbContextRegistrationOptions, IDbContextRegistrationOptionsBuilder
    {
        public FluentConfiguration FluentConfiguration { get; }
        public DbContextRegistrationOptionsBuilder(Type originalDbContextType, IServiceCollection services)
            : base(originalDbContextType, services)
        {
            FluentConfiguration = Fluently
                .Configure();

        }
    }
}