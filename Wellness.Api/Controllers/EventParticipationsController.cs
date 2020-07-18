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
    public class EventParticipationsController : ControllerBase, IAccessUser
    {
        private readonly IParticipationDomainService<EventParticipation> _domainServiceBase;

        public EventParticipationsController(IParticipationDomainService<EventParticipation> domainServiceBase)
        {
            _domainServiceBase = domainServiceBase;
        }

        [HttpGet("users/{userId}/relativeIndex/{relativeIndex}")]
        public async Task<IEnumerable<PersistenceWrapper<EventParticipation>>> Get(Guid userId, int relativeIndex, CancellationToken cancellationToken)
        {
            return await _domainServiceBase.GetBySelectedIndex(userId, relativeIndex, cancellationToken);
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventParticipation eventParticipation, CancellationToken cancellationToken)
        {
            await _domainServiceBase.Create(eventParticipation, cancellationToken);
            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await _domainServiceBase.Delete(id, cancellationToken);
            return NoContent();
        }
    }
}
