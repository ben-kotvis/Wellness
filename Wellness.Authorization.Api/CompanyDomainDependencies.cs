using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Wellness.Model;

namespace Wellness.Api
{
    public class CompanyDomainDependencies<T> : IDomainDependencies<T>
        where T : IIdentifiable
    {
        public CompanyDomainDependencies(
            ICompoundPersistanceService<T> persistanceService,
            IMap mapper,
            IClientNotifier clientNotifier)
        {
            PersistanceService = persistanceService;
            this.Mapper = mapper;
            this.ClientNotifier = clientNotifier;
        }
        public ICompoundPersistanceService<T> PersistanceService { get; }

        public IMap Mapper { get; }

        public IClientNotifier ClientNotifier { get; }
    }
}
