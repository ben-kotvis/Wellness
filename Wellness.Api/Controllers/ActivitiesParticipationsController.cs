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
    public class ActivityParticipationsController : WellnessControllerBase<ActivityParticipation>
    {
        public ActivityParticipationsController(IParticipationDomainService<ActivityParticipation> domainServiceBase)
            :base(domainServiceBase)
        {
        }

        [HttpGet("users/{userId}/relativeIndex/{relativeIndex}")]
        public async Task<IEnumerable<PersistenceWrapper<ActivityParticipation>>> Get(Guid userId, int relativeIndex, [FromServices] IRequestDependencies<ActivityParticipation> requestDependencies)
        {
            return await (_domainServiceBase as IParticipationDomainService<ActivityParticipation>).GetBySelectedIndex(userId, relativeIndex, requestDependencies);
        }
    }
}
