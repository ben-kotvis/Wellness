using Microsoft.AspNetCore.SignalR;
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
            IMap mapper,
            IClientNotifier clientNotifier)
        {
            PersistanceService = persistanceService;
            Validator = validator;
            this.Mapper = mapper;
            this.ClientNotifier = clientNotifier;
        }
        public IPersistanceService<T> PersistanceService { get; }

        public IValidate<T> Validator { get; }

        public IMap Mapper { get; }

        public IClientNotifier ClientNotifier { get; }
    }
}
