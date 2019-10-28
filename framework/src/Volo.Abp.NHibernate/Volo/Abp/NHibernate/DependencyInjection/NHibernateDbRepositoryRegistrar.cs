using FluentNHibernate;
using FluentNHibernate.Cfg.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.NHibernate;

namespace Volo.Abp.NHibernate.DependencyInjection
{
    public class NHibernateDbRepositoryRegistrar : RepositoryRegistrarBase<AbpNHibernateDbContextRegistrationOptions>
    {
        public NHibernateDbRepositoryRegistrar(AbpNHibernateDbContextRegistrationOptions options)
            : base(options)
        {

        }

        protected override IEnumerable<Type> GetEntityTypes(Type dbContextType)
        {
            //return new List<Type>();
            IEnumerable<Type> result = Options.FluentConfiguration
                //todo:remove. just for get mappedclass
                .Database(SQLiteConfiguration.Standard.InMemory())
                .BuildConfiguration()
                .ClassMappings.Select(x => x.MappedClass);
            
            return result.ToList();
        }

        protected override Type GetRepositoryType(Type dbContextType, Type entityType)
        {
            return typeof(NHibernateRepository<,>).MakeGenericType(dbContextType, entityType);
        }

        protected override Type GetRepositoryType(Type dbContextType, Type entityType, Type primaryKeyType)
        {
            return typeof(NHibernateRepository<,,>).MakeGenericType(dbContextType, entityType, primaryKeyType);
        }
    }
}