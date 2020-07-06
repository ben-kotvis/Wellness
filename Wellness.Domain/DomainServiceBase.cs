using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Domain
{
    public class DomainServiceBase<T>
        where T : IIdentifiable
    {
        protected AbstractValidator<T> Validator { get; }
        protected IPersistanceService<T> PersistanceService { get; }
        protected User LoggedInUser { get; }
        protected IMapper Mapper { get; }

        public DomainServiceBase(AbstractValidator<T> validator, IPersistanceService<T> persistanceService, IMapper mapper, User loggedInUser)
        {
            Validator = validator;
            PersistanceService = persistanceService;
            LoggedInUser = loggedInUser;
            Mapper = mapper;
        }

        public async Task Create(T model, CancellationToken cancellationToken)
        {
            await Validator.ValidateAndThrowAsync(model);

            var wrapper = new PersistenceWrapper<T>(model, new Common()
            {
                CreatedBy = LoggedInUser.UserName,
                CreatedOn = DateTimeOffset.UtcNow,
                UpdatedBy = LoggedInUser.UserName,
                UpdatedOn = DateTimeOffset.UtcNow
            });
            await PersistanceService.Create(wrapper, cancellationToken);
        }
        public async Task Update(T model, CancellationToken cancellationToken)
        {
            await Validator.ValidateAndThrowAsync(model);

            var existing = await PersistanceService.Get(model.Id, cancellationToken);
            existing.Common.UpdatedBy = LoggedInUser.UserName;
            existing.Common.UpdatedOn = DateTimeOffset.UtcNow;

            Mapper.Map(model, existing.Model);

            await PersistanceService.Update(existing, cancellationToken);
        }

        public async Task<IEnumerable<PersistenceWrapper<T>>> GetAll(CancellationToken cancellationToken)
        {
            return await PersistanceService.GetAll(cancellationToken);
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            await PersistanceService.Delete(id, cancellationToken);
        }
    }
}
