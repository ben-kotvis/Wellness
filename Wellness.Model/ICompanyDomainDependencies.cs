using System.Security.Claims;

namespace Wellness.Model
{
    public interface ICompanyDomainDependencies<T> where T : IIdentifiable
    {
        ICompoundCompanyPersistanceService<T> PersistanceService { get; }
        IMap Mapper { get; }
        IClientNotifier ClientNotifier { get; }
    }
}
