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
    public class ActivitiesController : ControllerBase, IAccessUser
    {
        private readonly IDomainService<Activity> _domainServiceBase;
        
        public ActivitiesController(IDomainDependencies<Activity> domainDependencies)
        {
            _domainServiceBase = new DomainServiceBase<Activity>(domainDependencies, this);
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

        [HttpPut("{activityId}")]
        public async Task<IActionResult> Put([FromRoute] Guid activityId, Activity activity, CancellationToken cancellationToken)
        {
            await _domainServiceBase.Update(activity, cancellationToken);
            return Accepted();
        }
    }
}
