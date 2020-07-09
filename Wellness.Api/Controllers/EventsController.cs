using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Wellness.Domain;
using Wellness.Model;

namespace Wellness.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase, IAccessUser
    {
        private readonly IDomainService<Event> _domainServiceBase;
        
        public EventsController(IDomainDependencies<Event> domainDependencies)
        {
            _domainServiceBase = new DomainServiceBase<Event>(domainDependencies, this);
        }

        [HttpGet]
        public async Task<IEnumerable<PersistenceWrapper<Event>>> Get(CancellationToken cancellationToken)
        {
            return (await _domainServiceBase.GetAll(cancellationToken));
        }

        [HttpGet("{eventId}")]
        public async Task<IEnumerable<PersistenceWrapper<Event>>> Get([FromRoute] Guid eventId, CancellationToken cancellationToken)
        {
            return (await _domainServiceBase.GetAll(cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> Post(Event Event, CancellationToken cancellationToken)
        {
            await _domainServiceBase.Create(Event, cancellationToken);
            return Accepted();
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> Put([FromRoute] Guid EventId, Event Event, CancellationToken cancellationToken)
        {
            await _domainServiceBase.Update(Event, cancellationToken);
            return Accepted();
        }
    }
}
