using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using JetBrains.Annotations;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Volo.Abp.NHibernate
{
    public class DbContextOptions<TDbContext>
        where TDbContext : IDbContext
    {
        private Type _configurationType { get; set; }
        private IPersistenceConfigurer _configuration { get; set; }
        private ISessionFactory _sessionFactory;

        public ISession Session
        {
            get
            {
                ISession result = null;
                if (_sessionFactory == null)
                {
                    _sessionFactory = FluentConfiguration.BuildSessionFactory();
                }

                result = DbConnection != null
                    ? _sessionFactory.WithOptions().Connection(DbConnection).OpenSession()
                    : _sessionFactory.OpenSession();

                return result;
            }
        }

        public FluentConfiguration FluentConfiguration { get; set; }
        /// <summary>
        /// This is usually set in tests.
        /// </summary>
        public DbConnection DbConnection { get; set; }

        public DbContextOptions()
        {
        }

        public void InitializeFluentConfiguration(FluentConfiguration fluentConfiguration)
        {
            this.FluentConfiguration = fluentConfiguration;
        }
        public void SetDataBase(IPersistenceConfigurer action)
        {
            _configurationType = action.GetType();
            _configuration = action;
        }
        public IPersistenceConfigurer UseDataBase(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                return _configuration;
            var result = _configurationType.GetMethod("ConnectionString", new Type[] { typeof(string) }).Invoke(_configuration, new object[] { connectionString });
            return (IPersistenceConfigurer)result;
        }


    }
}
