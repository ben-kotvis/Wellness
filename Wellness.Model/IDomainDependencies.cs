using System.Security.Claims;

namespace Wellness.Model
{
    public interface IDomainDependencies<T> where T : IIdentifiable
    {
        IPersistanceService<T> PersistanceService { get; }
        IMap Mapper { get; }
        IClientNotifier ClientNotifier { get; }
    }
}
