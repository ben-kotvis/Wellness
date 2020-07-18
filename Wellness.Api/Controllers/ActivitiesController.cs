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
    public class ActivitiesController : ControllerBase
    {
        private readonly IDomainService<Activity> _domainServiceBase;

        public ActivitiesController(IDomainService<Activity> domainService)
        {
            _domainServiceBase = domainService;
        }

        [HttpGet]
        public async Task<IEnumerable<PersistenceWrapper<Activity>>> Get(CancellationToken cancellationToken)
        {
            return (await _domainServiceBase.GetAll(cancellationToken));
        }

        [HttpGet("{activityId}")]
        public async Task<IEnumerable<PersistenceWrapper<Activity>>> Get([FromRoute] Guid activityId, CancellationToken cancellationToken)
        {
            return (await _domainServiceBase.GetAll(cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> Post(Activity activity, CancellationToken cancellationToken)
        {
            await _domainServiceBase.Create(activity, cancellationToken);
            return Accepted();
        }

        [HttpDelete("{activityId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid activityId, CancellationToken cancellationToken)
        {
            await _domainServiceBase.Delete(activityId, cancellationToken);
            return NoContent();
        }

        [HttpPut("{activityId}")]
        public async Task<IActionResult> Put([FromRoute] Guid activityId, Activity activity, CancellationToken cancellationToken)
        {
            await _domainServiceBase.Update(activity, cancellationToken);
            return Accepted();
        }
    }
}
