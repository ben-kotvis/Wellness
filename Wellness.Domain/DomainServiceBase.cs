﻿using System;
using System.Collections.Generic;
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

        public async Task Create(T model, CancellationToken cancellationToken)
        {
            await _domainDependencies.Validator.ValidateAndThrowAsync(model);

            var wrapper = new PersistenceWrapper<T>(model, new Common()
            {
                CreatedBy = _domainDependencies.Principal.Identity.Name,
                CreatedOn = DateTimeOffset.UtcNow,
                UpdatedBy = _domainDependencies.Principal.Identity.Name,
                UpdatedOn = DateTimeOffset.UtcNow
            });
            await _domainDependencies.PersistanceService.Create(wrapper, cancellationToken);
        }
        public async Task Update(T model, CancellationToken cancellationToken)
        {
            await _domainDependencies.Validator.ValidateAndThrowAsync(model);

            var existing = await _domainDependencies.PersistanceService.Get(model.Id, cancellationToken);
            existing.Common.UpdatedBy = _domainDependencies.Principal.Identity.Name;
            existing.Common.UpdatedOn = DateTimeOffset.UtcNow;

            _domainDependencies.Mapper.Map(model, existing.Model);

            await _domainDependencies.PersistanceService.Update(existing, cancellationToken);
        }

        public async Task<IEnumerable<PersistenceWrapper<T>>> GetAll(CancellationToken cancellationToken)
        {
            return await _domainDependencies.PersistanceService.GetAll(cancellationToken);
        }

        public async Task<PersistenceWrapper<T>> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _domainDependencies.PersistanceService.Get(id, cancellationToken);
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            await _domainDependencies.PersistanceService.Delete(id, cancellationToken);
        }
    }
}