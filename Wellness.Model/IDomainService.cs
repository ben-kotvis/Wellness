using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IDomainService<T> where T : IIdentifiable
    {
        Task<IEnumerable<PersistenceWrapper<T>>> GetAll(IRequestDependencies requestDependencies);
        Task<PersistenceWrapper<T>> Get(Guid id, IRequestDependencies requestDependencies);
        Task Create(T model, IRequestDependencies requestDependencies);
        Task Update(T model, IRequestDependencies requestDependencies);
        Task Delete(Guid id, IRequestDependencies requestDependencies);
    }
}
