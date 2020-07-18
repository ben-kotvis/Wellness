using System.Security.Claims;

namespace Wellness.Model
{
    public interface IDomainDependencies<T> where T : IIdentifiable
    {
        IValidate<T> Validator { get; }
        ClaimsPrincipal Principal { get; }
        IPersistanceService<T> PersistanceService { get; }
        IMap Mapper { get; }
    }
}
