using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Api
{
    public class DomainDependencies<T> : IDomainDependencies<T>
        where T : IIdentifiable
    {
        public DomainDependencies(
            IPersistanceService<T> persistanceService,
            IValidator<T> validator,
            IMapper mapper)
        {
            PersistanceService = persistanceService;
            Validator = validator;
            this.Mapper = mapper;
        }
        public IPersistanceService<T> PersistanceService { get; }

        public IValidator<T> Validator { get; }

        public IMapper Mapper { get; }
    }
}
