using FluentValidation;
using System;
using System.Collections.Generic;
using Wellness.Model;

namespace Wellness.Domain.ModelValidation
{
    public class ActivityParticipationValidation : AbstractValidator<ActivityParticipation>
    {
        private readonly IEnumerable<Activity> _events;
        private readonly IRequestDependencies<ActivityParticipation> _requestDependencies;
        public ActivityParticipationValidation(IEnumerable<Activity> events, IRequestDependencies<ActivityParticipation> requestDependencies)
        {
            this._requestDependencies = requestDependencies;
            this._events = events;
            RuleFor(e => e.Activity).NotNull();
            RuleFor(e => e.SubmissionDate).NotEqual(default(DateTime));
            RuleFor(e => e.Minutes).GreaterThanOrEqualTo(15);
        }

    }

}
