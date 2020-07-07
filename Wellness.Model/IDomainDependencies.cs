using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wellness.Model
{
    public interface IDomainDependencies<T> where T : IIdentifiable
    {
        IPersistanceService<T> PersistanceService { get; }
        IValidator<T> Validator { get; }
        IMapper Mapper { get; }
    }
}
