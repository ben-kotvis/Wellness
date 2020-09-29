using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Wellness.Model;

namespace Wellness.Api
{
    public class DomainDependencies<T> : ICompanyDomainDependencies<T>
        where T : IIdentifiable
    {
        public DomainDependencies(
            ICompoundCompanyPersistanceService<T> persistanceService,
            IMap mapper,
            IClientNotifier clientNotifier)
        {
            PersistanceService = persistanceService;
            this.Mapper = mapper;
            this.ClientNotifier = clientNotifier;
        }
        public ICompoundCompanyPersistanceService<T> PersistanceService { get; }

        public IMap Mapper { get; }

        public IClientNotifier ClientNotifier { get; }
    }
}
