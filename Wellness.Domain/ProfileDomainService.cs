using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;
using System.Security.Claims;

namespace Wellness.Domain
{
    public class ProfileDomainService : DomainServiceBase<User>, IProfileDomainService
    {
        public ProfileDomainService(IDomainDependencies<User> domainDependencies)
            :base(domainDependencies)
        {
        }

        public async Task<PersistenceWrapper<User>> GetCurrentUser(IRequestDependencies<User> requestDependencies)
        {
            var id = requestDependencies.Principal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            var queryable = _domainDependencies.PersistanceService.Reader.Query(requestDependencies.CompanyId).Where(i => i.Model.ProviderObjectId == id);
            return await queryable.FirstOrDefaultAsync(requestDependencies.CancellationToken);
        }

    }
}
