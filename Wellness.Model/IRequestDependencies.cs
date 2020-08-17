using System.Security.Claims;
using System.Threading;

namespace Wellness.Model
{
    public interface IRequestDependencies
    {
        ClaimsPrincipal Principal { get; }
        CancellationToken CancellationToken { get; }
    }
}
