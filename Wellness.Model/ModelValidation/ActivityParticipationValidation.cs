using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model.ModelValidation
{
    public class ActivityParticipationValidation : AbstractValidator<ActivityParticipation>
    {
        private readonly IEnumerable<Activity> _events;

        public ActivityParticipationValidation(IEnumerable<Activity> events)
        {
            this._events = events;
            RuleFor(e => e.Activity).NotNull();
            RuleFor(e => e.SubmissionDate).NotEqual(default(DateTime));
            RuleFor(e => e.Minutes).GreaterThanOrEqualTo(15);
            RuleFor(e => e.PointsEarned).Must((f, a, token) => PointsAreCorrect(f, a));
        }

        private bool PointsAreCorrect(ActivityParticipation activityParticipation, decimal points)
        {
            if (activityParticipation.Activity == default)
            {
                return true;
            }
            return points == Math.Round(activityParticipation.Minutes * 0.166666666667m);
        }
    }

}
