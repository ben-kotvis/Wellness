﻿using FluentValidation;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Domain.ModelValidation
{
    public class EventValidation : AbstractValidator<Event>
    {
        private readonly ICompanyPersistanceReaderService<Event> _eventManagementService;
        private readonly Guid _companyId;
        public EventValidation(ICompanyPersistanceReaderService<Event> eventManagementService, ClaimsPrincipal claimsPrincipal)
        {
            _eventManagementService = eventManagementService;
            _companyId = Guid.Parse(claimsPrincipal.FindFirst("companyId").Value);
            RuleFor(e => e.Name).NotEmpty().MaximumLength(50).WithMessage("Please provide a value that is less than 50 characters");
                       RuleFor(e => e.Name).Must(EventNameIsUnique).WithMessage("Event name must be unique");
            RuleFor(e => e.Points).GreaterThanOrEqualTo(5).LessThanOrEqualTo(300);
            RuleFor(e => e.AnnualMaximum).GreaterThanOrEqualTo(e => e.Points).WithMessage("Annual maximum must be greater than or eqaul to than the points for the single event.");
        }

        private bool EventNameIsUnique(Event eventObj, string eventName)
        {
            var events = _eventManagementService.GetAll(_companyId, CancellationToken.None).GetAwaiter().GetResult();
            return !events.Any(i => i.Model.Id != eventObj.Id && i.Model.Name.ToLower() == eventName.ToLower());
        }

        private async Task<bool> EventNameIsUniqueAsync(Event eventObj, string eventName, CancellationToken cancellationToken)
        {
            var events = await _eventManagementService.GetAll(_companyId, cancellationToken);
            return !events.Any(i => i.Model.Id != eventObj.Id && i.Model.Name.ToLower() == eventName.ToLower());
        }
    }
}
