using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController : WellnessControllerBase<Activity>
    {

        public ActivitiesController(IDomainService<Activity> domainService)
            :base(domainService)
        {
        }
    }
}
