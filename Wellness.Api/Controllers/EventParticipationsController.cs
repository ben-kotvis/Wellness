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
    public class EventParticipationsController : WellnessControllerBase<EventParticipation>
    {

        public EventParticipationsController(IParticipationDomainService<EventParticipation> domainServiceBase)
            :base(domainServiceBase)
        {
        }

        [HttpGet("users/{userId}/relativeIndex/{relativeIndex}")]
        public async Task<IEnumerable<PersistenceWrapper<EventParticipation>>> Get(
            Guid userId, 
            int relativeIndex, 
            [FromServices] IRequestDependencies<EventParticipation> requestDependencies)
        {
            return await (_domainServiceBase as IParticipationDomainService<EventParticipation>).GetBySelectedIndex(userId, relativeIndex, requestDependencies);
        }
    }
}
