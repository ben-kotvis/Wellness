using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IPersistanceService<T> where T : IIdentifiable
    {
        IModelQueryable<PersistenceWrapper<T>> Query { get; }
        Task Create(PersistenceWrapper<T> wrapped, CancellationToken cancellationToken);
        Task Update(PersistenceWrapper<T> wrapped, CancellationToken cancellationToken);
        Task<IEnumerable<PersistenceWrapper<T>>> GetAll(CancellationToken cancellationToken);
        Task<PersistenceWrapper<T>> Get(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<PersistenceWrapper<T>>> Get(Expression<Func<PersistenceWrapper<T>, bool>> filter, CancellationToken cancellationToken);        
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}
