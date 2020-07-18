using System;
using System.Threading;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IDomainService<T> : IPersistanceReaderService<T> where T : IIdentifiable
    {
        Task Create(T model, CancellationToken cancellationToken);
        Task Update(T model, CancellationToken cancellationToken);
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}
