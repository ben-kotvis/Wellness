using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Wellness.Model;

namespace Wellness.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController : ControllerBase
    {
        private readonly IPersistanceService _persistanceService;
        public ActivitiesController(IPersistanceService persistanceService)
        {
            _persistanceService = persistanceService;
        }

        [HttpGet]
        public IEnumerable<Activity> Get(CancellationToken cancellationToken)
        {
            var list = new List<Activity>();
            return list;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Activity activity, CancellationToken cancellationToken)
        {
            var wrapper = new PersistenceWrapper<Activity>(activity, new Common()
            {
                CompanyId = Guid.NewGuid(),
                CreatedBy = "bkotvis",
                CreatedOn = DateTimeOffset.UtcNow,
                 UpdatedBy = "bkotvis",
                 UpdatedOn = DateTimeOffset.UtcNow
            });
            await _persistanceService.Create(wrapper);
            return Accepted();
        }
    }
}
