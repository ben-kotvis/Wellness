using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;
using System.Text;
using System.Web;

namespace Wellness.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : WellnessControllerBase<User>
    {

        public ProfilesController(IProfileDomainService domainServiceBase)
            : base(domainServiceBase)
        {
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMe([FromServices] IRequestDependencies requestDependencies)
        {
            var item =  await (_domainServiceBase as IProfileDomainService).GetCurrentUser(requestDependencies);

            if(item == default)
            {
                return NotFound();
            }

            return Ok(item);
        }
    }
}
