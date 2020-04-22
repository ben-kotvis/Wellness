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
        private IEventManagementService _eventManagementService;

        public EventParticipationValidation(IEventManagementService eventManagementService)
        {
            _eventManagementService = eventManagementService;

            //RuleFor(e => e.UserId).NotEmpty();
            RuleFor(e => e.Event).NotNull();
            RuleFor(e => e.SubmissionDate).NotEqual(default(DateTime));
            RuleFor(e => e.Attachment).Must((f, a, token) => AttachmentIsMissing(f, a));
        }

        private bool AttachmentIsMissing(EventParticipation eventParticipation, EventAttachment attachment)
        {
            return !(eventParticipation.Event.RequireAttachment && attachment == default);
        }
    }

}
