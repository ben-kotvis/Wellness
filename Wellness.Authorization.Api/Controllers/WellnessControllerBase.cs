using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Api.Controllers
{
    [Authorize]
    [ApiController]
    public abstract class WellnessControllerBase<T> : ControllerBase
        where T : IIdentifiable
    {
        protected readonly IDomainService<T> _domainServiceBase;

        public WellnessControllerBase(IDomainService<T> domainService)
        {
            _domainServiceBase = domainService;
        }

        [HttpGet]
        public async Task<IEnumerable<PersistenceWrapper<T>>> Get([FromServices] IRequestDependencies<T> requestDependencies)
        {
            return (await _domainServiceBase.GetAll(requestDependencies));
        }

        [HttpGet("{id}")]
        public async Task<PersistenceWrapper<T>> Get([FromRoute] Guid id, [FromServices] IRequestDependencies<T> requestDependencies)
        {
            return (await _domainServiceBase.Get(id, requestDependencies));
        }

        [HttpPost]
        public async Task<IActionResult> Post(T activity, [FromServices] IRequestDependencies<T> requestDependencies)
        {
            await _domainServiceBase.Create(activity, requestDependencies);
            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, [FromServices] IRequestDependencies<T> requestDependencies)
        {
            await _domainServiceBase.Delete(id, requestDependencies);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, T activity, [FromServices] IRequestDependencies<T> requestDependencies)
        {
            await _domainServiceBase.Update(activity, requestDependencies);
            return Accepted();
        }
    }
}
