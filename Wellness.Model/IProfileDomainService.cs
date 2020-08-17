using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IProfileDomainService : IDomainService<User> 
    {
        Task<PersistenceWrapper<User>> GetCurrentUser(IRequestDependencies requestDependencies);
    }
}
