using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Api
{
    public class ReaderService<T> : IReaderService<T>
        where T : IIdentifiable
    {
        private readonly IPersistanceReaderService<T> _persistanceReaderService;
        private readonly ClaimsPrincipal _claimsPrincipal;
        private readonly Guid _companyId;
        public ReaderService(
            IPersistanceReaderService<T> persistanceReaderService, ClaimsPrincipal claimsPrincipal)
        {
            _persistanceReaderService = persistanceReaderService;
            _claimsPrincipal = claimsPrincipal;
            _companyId = Guid.Parse(claimsPrincipal.FindFirstValue("companyId"));
        }

        public Task<PersistenceWrapper<T>> Get(Guid id, CancellationToken cancellationToken)
        {
            return _persistanceReaderService.Get(id, _companyId, cancellationToken);
        }

        public Task<IEnumerable<PersistenceWrapper<T>>> GetAll(CancellationToken cancellationToken)
        {
            return _persistanceReaderService.GetAll(_companyId, cancellationToken);
        }
    }
}
