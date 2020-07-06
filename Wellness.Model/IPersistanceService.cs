using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IPersistanceService<T> where T : IIdentifiable
    {
        Task Create(PersistenceWrapper<T> wrapped, CancellationToken cancellationToken);
        Task Update(PersistenceWrapper<T> wrapped, CancellationToken cancellationToken);
        Task<IEnumerable<PersistenceWrapper<T>>> GetAll(CancellationToken cancellationToken);
        Task<PersistenceWrapper<T>> Get(Guid id, CancellationToken cancellationToken);
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}
