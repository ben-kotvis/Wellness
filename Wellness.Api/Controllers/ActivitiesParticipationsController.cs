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
        private readonly IParticipationDomainService<ActivityParticipation> _domainServiceBase;

        public ActivityParticipationsController(IParticipationDomainService<ActivityParticipation> domainServiceBase)
            :base(domainServiceBase)
        {
            _domainServiceBase = domainServiceBase;
        }

        [HttpGet("users/{userId}/relativeIndex/{relativeIndex}")]
        public async Task<IEnumerable<PersistenceWrapper<ActivityParticipation>>> Get(Guid userId, int relativeIndex, [FromServices] IRequestDependencies requestDependencies)
        {
            return await _domainServiceBase.GetBySelectedIndex(userId, relativeIndex, requestDependencies.CancellationToken);
        }
    }
}
