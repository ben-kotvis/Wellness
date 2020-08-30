using FluentValidation;
using System;
using System.Collections.Generic;
using Wellness.Model;

namespace Wellness.Domain.ModelValidation
{
    public class ActivityParticipationValidation : AbstractValidator<ActivityParticipation>
    {
        public ActivityParticipationValidation()
        {
            RuleFor(e => e.Activity).NotNull();
            RuleFor(e => e.SubmissionDate).NotEqual(default(DateTime));
            RuleFor(e => e.Minutes).GreaterThanOrEqualTo(15);
        }

    }

}
