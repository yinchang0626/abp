using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.NHibernate;
using Volo.Abp.NHibernate;
using Volo.Abp.TestApp.Domain;
using Volo.Abp.TestApp.NHibernate;

namespace Volo.Abp.TestApp.NHibernate
{
    public class CityRepository : NHibernateRepository<TestAppDbContext, City, Guid>, ICityRepository
    {
        public CityRepository(IDbContextProvider<TestAppDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public Task<City> FindByNameAsync(string name)
        {
            return Task.FromResult(this.FirstOrDefault(c => c.Name == name));
        }

        public async Task<List<Person>> GetPeopleInTheCityAsync(string cityName)
        {
            var city = await FindByNameAsync(cityName);
            var result = DbContext.People.Where(p => p.CityId == city.Id).ToList();
            return await Task.FromResult(result);
        }
    }
}
