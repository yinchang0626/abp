using NHibernate;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp.NHibernate
{
    public interface IDbContext: ITransientDependency
    {
        ISession Session { get; }
        void InitializeSession(ISession session);
    }
}