using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Domain
{
    public class DomainServiceBase<T> : IDomainService<T>
        where T : IIdentifiable
    {
        protected readonly IDomainDependencies<T> _domainDependencies;
        public DomainServiceBase(IDomainDependencies<T> domainDependencies)
        {
            _domainDependencies = domainDependencies;
        }

        public async Task Create(T model, IRequestDependencies requestDependencies)
        {

            await _domainDependencies.Validator.ValidateAndThrowAsync(model);

            var wrapper = new PersistenceWrapper<T>(model, new Common()
            {
                CreatedBy = requestDependencies.Principal.Identity.Name,
                CreatedOn = DateTimeOffset.UtcNow,
                UpdatedBy = requestDependencies.Principal.Identity.Name,
                UpdatedOn = DateTimeOffset.UtcNow
            });
            await _domainDependencies.PersistanceService.Create(wrapper, requestDependencies.CancellationToken);
        }
        public async Task Update(T model, IRequestDependencies requestDependencies)
        {
            await _domainDependencies.Validator.ValidateAndThrowAsync(model);

            var existing = await _domainDependencies.PersistanceService.Get(model.Id, requestDependencies.CancellationToken);
            existing.Common.UpdatedBy = requestDependencies.Principal.Identity.Name;
            existing.Common.UpdatedOn = DateTimeOffset.UtcNow;

            _domainDependencies.Mapper.Map(model, existing.Model);

            await _domainDependencies.PersistanceService.Update(existing, requestDependencies.CancellationToken);
        }

        public async Task<IEnumerable<PersistenceWrapper<T>>> GetAll(IRequestDependencies requestDependencies)
        {
            var result = await _domainDependencies.PersistanceService.GetAll(requestDependencies.CancellationToken);
            if(result == null)
            {
                return Enumerable.Empty<PersistenceWrapper<T>>();
            }

            return result;
        }

        public async Task<PersistenceWrapper<T>> Get(Guid id, IRequestDependencies requestDependencies)
        {
            return await _domainDependencies.PersistanceService.Get(id, requestDependencies.CancellationToken);
        }

        public async Task Delete(Guid id, IRequestDependencies requestDependencies)
        {
            await _domainDependencies.PersistanceService.Delete(id, requestDependencies.CancellationToken);
        }
    }
}
