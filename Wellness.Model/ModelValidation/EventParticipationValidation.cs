using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model.ModelValidation
{
    public class EventParticipationValidation : AbstractValidator<EventParticipation>
    {
        private readonly IEnumerable<Event> _events;        

        public EventParticipationValidation(IEnumerable<Event> events)
        {
            this._events = events;
            RuleFor(e => e.Event).NotNull();
            RuleFor(e => e.SubmissionDate).NotEqual(default(DateTime));
            RuleFor(e => e.Attachment).Must((f, a, token) => AttachmentIsMissing(f, a));
            RuleFor(e => e.PointsEarned).Must((f, a, token) => PointsAreCorrect(f, a));
        }

        private bool PointsAreCorrect(EventParticipation eventParticipation, decimal points)
        {
            if (eventParticipation.Event == default)
            {
                return true;
            }

            var eventObj = _events.FirstOrDefault(i => i.Id == eventParticipation.Event.Id);

            return points == eventObj.Points;
        }

        private bool AttachmentIsMissing(EventParticipation eventParticipation, EventAttachment attachment)
        {
            if(eventParticipation.Event == default)
            {
                return true;
            }

            var eventObj = _events.FirstOrDefault(i => i.Id == eventParticipation.Event.Id);

            return !(eventObj.RequireAttachment && attachment == default);
        }
    }

}
