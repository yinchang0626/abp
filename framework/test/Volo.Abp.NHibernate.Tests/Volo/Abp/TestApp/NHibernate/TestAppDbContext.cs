using System.Collections.Generic;
using System.Linq;
using Volo.Abp.NHibernate;
using Volo.Abp.TestApp.Domain;

namespace Volo.Abp.TestApp.NHibernate
{
    public class TestAppDbContext : DbContext<TestAppDbContext>//, IThirdDbContext
    {
        public IQueryable<Person> People => this.Session.Query<Person>();

        public IQueryable<City> Cities => this.Session.Query<City>();

        //public IList<ThirdDbContextDummyEntity> DummyEntities { get; set; }

        public TestAppDbContext()
        {

        }
    }
}
