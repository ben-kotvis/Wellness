using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading;
using Wellness.Model;

namespace Wellness.Api
{
    public class RequestDependencies<T> : IRequestDependencies<T>
        where T : IIdentifiable
    {
        public RequestDependencies(IHttpContextAccessor contextAccessor, ClaimsPrincipal claimsPrincipal, IValidate<T> validator)
        {
            this.Validator = validator;
            this.Principal = claimsPrincipal;
            this.CancellationToken = contextAccessor.HttpContext.RequestAborted;
            this.CompanyId = Guid.Parse(claimsPrincipal.FindFirstValue("extn.companyId"));
        }
        public IValidate<T> Validator { get; }

        public Guid CompanyId { get; }

        public ClaimsPrincipal Principal { get; }

        public CancellationToken CancellationToken { get; }
    }
}
