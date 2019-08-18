using FluentNHibernate.Cfg;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Volo.Abp.Data;
using Volo.Abp.NHibernate;

namespace Volo.Abp.NHibernate.DependencyInjection
{

    internal static class DbContextOptionsFactory
    {
        public static DbContextOptions<TDbContext> Create<TDbContext>(IServiceProvider serviceProvider, FluentConfiguration fluentConfiguration)
            where TDbContext : DbContext<TDbContext>
        {
            var result = serviceProvider.GetRequiredService<IOptions<DbContextOptions<TDbContext>>>().Value;
            result.InitializeFluentConfiguration(fluentConfiguration);
            return result;
        }
    }
}