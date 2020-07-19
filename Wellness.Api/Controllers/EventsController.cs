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
    public class EventsController : WellnessControllerBase<Event>
    {
        public EventsController(IDomainService<Event> domainServiceBase)
            :base(domainServiceBase)
        {
        }

    }
}
