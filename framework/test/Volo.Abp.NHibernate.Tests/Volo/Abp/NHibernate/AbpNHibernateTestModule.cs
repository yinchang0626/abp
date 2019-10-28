using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Data.SQLite;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.TestApp;
using Volo.Abp.TestApp.NHibernate;

namespace Volo.Abp.NHibernate
{
    [DependsOn(typeof(AbpNHibernateModule))]
    [DependsOn(typeof(TestAppModule))]
    [DependsOn(typeof(AbpAutofacModule))]
    public class AbpNHibernateTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var sqliteConnection = CreateDatabaseAndGetConnection();
            Configure<DbContextOptions<TestAppDbContext>>(options =>
            {
                options.SetDataBase(SQLiteConfiguration.Standard.InMemory());
                options.DbConnection = sqliteConnection;
            });

            
            context.Services.AddAbpNhibernateDbContext<TestAppDbContext>(options =>
            {
                options.AddDefaultRepositories(true);
                options.FluentConfiguration
                .Database(SQLiteConfiguration.Standard.InMemory())
                .Mappings(m =>
                {
                    m.FluentMappings.AddFromAssemblyOf<TestAppDbContext>();
                })
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Execute(true, true, false, sqliteConnection, Console.Out));
                options.AddDefaultRepositories<TestAppDbContext>();
                options.AddRepository<TestApp.Domain.City, CityRepository>();
            });

        }

        public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
        {
            var t = context.ServiceProvider.GetRequiredService<TestAppDbContext>();
        }
        private static SQLiteConnection CreateDatabaseAndGetConnection()
        {
            var connection = new SQLiteConnection(@"Data Source=:memory:");
            connection.Open(); 
            return connection;
        }
    }
}
