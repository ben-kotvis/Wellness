using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Wellness.Model
{
    public interface IDomainDependencies<T> where T : IIdentifiable
    {
        IValidator<T> Validator { get; }
        ClaimsPrincipal Principal { get; }
        IPersistanceService<T> PersistanceService { get; }
        IMapper Mapper { get; }
    }
}
