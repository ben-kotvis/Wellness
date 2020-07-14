﻿using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Api
{
    public class DomainDependencies<T> : IDomainDependencies<T>
        where T : IIdentifiable
    {
        public DomainDependencies(
            IPersistanceService<T> persistanceService,
            IValidate<T> validator,
            IMap mapper, 
            ClaimsPrincipal claimsPrincipal)
        {
            PersistanceService = persistanceService;
            Validator = validator;
            this.Mapper = mapper;
            this.Principal = claimsPrincipal;
        }
        public ClaimsPrincipal Principal { get; }
        public IPersistanceService<T> PersistanceService { get; }

        public IValidate<T> Validator { get; }

        public IMap Mapper { get; }

    }
}
