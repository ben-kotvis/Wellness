using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Api.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase, IAccessUser
    {
        private readonly IDomainService<Event> _domainServiceBase;

        public EventsController(IDomainService<Event> domainServiceBase)
        {
            _domainServiceBase = domainServiceBase;
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

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid eventId, CancellationToken cancellationToken)
        {
            await _domainServiceBase.Delete(eventId, cancellationToken);
            return NoContent();
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> Put([FromRoute] Guid EventId, Event Event, CancellationToken cancellationToken)
        {
            await _domainServiceBase.Update(Event, cancellationToken);
            return Accepted();
        }
    }
}
