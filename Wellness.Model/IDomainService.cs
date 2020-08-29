using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IDomainService<T> where T : IIdentifiable
    {
        Task<IEnumerable<PersistenceWrapper<T>>> GetAll(IRequestDependencies<T> requestDependencies);
        Task<PersistenceWrapper<T>> Get(Guid id, IRequestDependencies<T> requestDependencies);
        Task Create(T model, IRequestDependencies<T> requestDependencies);
        Task Update(T model, IRequestDependencies<T> requestDependencies);
        Task Delete(Guid id, IRequestDependencies<T> requestDependencies);
    }
}
