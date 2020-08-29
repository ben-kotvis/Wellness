using System;
using System.Security.Claims;
using System.Threading;

namespace Wellness.Model
{
    public interface IRequestDependencies<T> where T : IIdentifiable
    {
        IValidate<T> Validator { get; }
        Guid CompanyId { get; }
        ClaimsPrincipal Principal { get; }
        CancellationToken CancellationToken { get; }
    }
}
