using FluentNHibernate.Cfg;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp.NHibernate.DependencyInjection
{
    public interface IAbpNHibernateDbContextRegistrationOptionsBuilder : IAbpCommonDbContextRegistrationOptionsBuilder
    {
        FluentConfiguration FluentConfiguration { get; }

    }
}