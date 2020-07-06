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
    public class ActivitiesController : ControllerBase
    {
        private readonly IPersistanceService<Activity> _persistanceService;
        private readonly DomainServiceBase<Activity> _domainServiceBase;
        
        public ActivitiesController(IPersistanceService<Activity> persistanceService,
            DomainServiceBase<Activity> domainServiceBase)
        {
            _persistanceService = persistanceService;
            _domainServiceBase = domainServiceBase;
        }

        [HttpGet]
        public async Task<IEnumerable<PersistenceWrapper<Activity>>> Get(CancellationToken cancellationToken)
        {
            return (await _domainServiceBase.GetAll(cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> Post(Activity activity, CancellationToken cancellationToken)
        {
            await _domainServiceBase.Create(activity, cancellationToken);
            return Accepted();
        }
    }
}
