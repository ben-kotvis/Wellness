using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading;
using Wellness.Model;

namespace Wellness.Api
{
    public class RequestDependencies : IRequestDependencies
    {
        public RequestDependencies(IHttpContextAccessor contextAccessor, ClaimsPrincipal claimsPrincipal)
        {
            this.Principal = claimsPrincipal;
            this.CancellationToken = contextAccessor.HttpContext.RequestAborted;
        }
        public ClaimsPrincipal Principal { get; }

        public CancellationToken CancellationToken { get; }
    }
}
