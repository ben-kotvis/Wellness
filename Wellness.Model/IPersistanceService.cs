using System;
using System.Threading;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IPersistanceService<T> : IPersistanceReaderService<T> where T : IIdentifiable
    {
        IModelQueryable<PersistenceWrapper<T>> Query { get; }
        Task Create(PersistenceWrapper<T> wrapped, CancellationToken cancellationToken);
        Task Update(PersistenceWrapper<T> wrapped, CancellationToken cancellationToken);
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}
