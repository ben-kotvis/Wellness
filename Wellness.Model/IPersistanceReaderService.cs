using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IPersistanceReaderService<T> where T : IIdentifiable
    {
        Task<IEnumerable<PersistenceWrapper<T>>> GetAll(Guid companyId, CancellationToken cancellationToken);
        Task<PersistenceWrapper<T>> Get(Guid id, Guid companyId, CancellationToken cancellationToken);
    }
}
