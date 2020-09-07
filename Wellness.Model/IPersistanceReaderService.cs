using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IPersistanceReaderService<T> where T : IIdentifiable
    {
        ICompanyModelQueryable<PersistenceWrapper<T>> Query { get; }
        Task<IEnumerable<PersistenceWrapper<T>>> GetAll(CancellationToken cancellationToken);
        Task<PersistenceWrapper<T>> Get(Guid id, CancellationToken cancellationToken);
    }
}
