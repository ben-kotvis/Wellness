using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task Create(T model, IRequestDependencies<T> requestDependencies)
        {

            await requestDependencies.Validator.ValidateAndThrowAsync(model);

            var wrapper = new PersistenceWrapper<T>(model, new Common()
            {
                CreatedBy = requestDependencies.Principal.Identity.Name,
                CreatedOn = DateTimeOffset.UtcNow,
                UpdatedBy = requestDependencies.Principal.Identity.Name,
                UpdatedOn = DateTimeOffset.UtcNow,
                CompanyId = requestDependencies.CompanyId
            });
            await _domainDependencies.PersistanceService.Create(wrapper, requestDependencies.CancellationToken);
        }
        public async Task Update(T model, IRequestDependencies<T> requestDependencies)
        {
            await requestDependencies.Validator.ValidateAndThrowAsync(model);

            var existing = await _domainDependencies.PersistanceService.Get(model.Id, requestDependencies.CompanyId, requestDependencies.CancellationToken);
            existing.Common.UpdatedBy = requestDependencies.Principal.Identity.Name;
            existing.Common.UpdatedOn = DateTimeOffset.UtcNow;
            existing.Common.CompanyId = requestDependencies.CompanyId;

            _domainDependencies.Mapper.Map(model, existing.Model);

            await _domainDependencies.PersistanceService.Update(existing, requestDependencies.CancellationToken);
        }

        public async Task<IEnumerable<PersistenceWrapper<T>>> GetAll(IRequestDependencies<T> requestDependencies)
        {
            var result = await _domainDependencies.PersistanceService.GetAll(requestDependencies.CompanyId, requestDependencies.CancellationToken);
            if(result == null)
            {
                return Enumerable.Empty<PersistenceWrapper<T>>();
            }

            return result;
        }

        public async Task<PersistenceWrapper<T>> Get(Guid id, IRequestDependencies<T> requestDependencies)
        {
            return await _domainDependencies.PersistanceService.Get(id, requestDependencies.CompanyId, requestDependencies.CancellationToken);
        }

        public async Task Delete(Guid id, IRequestDependencies<T> requestDependencies)
        {
            await _domainDependencies.PersistanceService.Delete(id, requestDependencies.CancellationToken);
            await _domainDependencies.ClientNotifier.SendNotification("Test");
        }

        public static Expression<Func<PersistenceWrapper<T>, bool>> CreateTenantFilter(IRequestDependencies<T> requestDependencies)
        {
            return x => x.Common.CompanyId == requestDependencies.CompanyId;
        }
    }
}
