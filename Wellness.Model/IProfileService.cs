using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IProfileService
    {
        Task Create(User user);
        Task<PersistenceWrapper<User>> GetCurrent(CancellationToken cancellationToken);
        Task<PersistenceWrapper<User>> Get(Guid id, CancellationToken cancellationToken);
        Task Update(User user);
        Task<IEnumerable<PersistenceWrapper<User>>> Find(string searchText, CancellationToken cancellationToken);
    }
}
