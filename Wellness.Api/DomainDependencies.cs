using System.Security.Claims;
using Wellness.Model;

namespace Wellness.Api
{
    public class DomainDependencies<T> : IDomainDependencies<T>
        where T : IIdentifiable
    {
        public DomainDependencies(
            IPersistanceService<T> persistanceService,
            IValidate<T> validator,
            IMap mapper)
        {
            PersistanceService = persistanceService;
            Validator = validator;
            this.Mapper = mapper;
        }
        public IPersistanceService<T> PersistanceService { get; }

        public IValidate<T> Validator { get; }

        public IMap Mapper { get; }

    }
}
