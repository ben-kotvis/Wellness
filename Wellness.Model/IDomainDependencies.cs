using System.Security.Claims;

namespace Wellness.Model
{
    public interface IDomainDependencies<T> where T : IIdentifiable
    {
        ICompoundPersistanceService<T> PersistanceService { get; }
        IMap Mapper { get; }
        IClientNotifier ClientNotifier { get; }
    }
}
