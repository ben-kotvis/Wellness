using System.Security.Claims;

namespace Wellness.Model
{
    public interface IDomainDependencies<T> where T : IIdentifiable
    {
        ICompoundCompanyPersistanceService<T> PersistanceService { get; }
        IMap Mapper { get; }
        IClientNotifier ClientNotifier { get; }
    }
}
