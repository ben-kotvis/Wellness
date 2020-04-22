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
        public EventParticipationValidation()
        {
            RuleFor(e => e.UserId).NotEmpty();
            RuleFor(e => e.Event).NotEmpty();
            RuleFor(e => e.SubmissionDate).NotEqual(default(DateTime));
        }
    }

    public class ServerEventParticipationValidation : EventParticipationValidation
    {
        private IEventManagementService _eventManagementService;

        public ServerEventParticipationValidation(IEventManagementService eventManagementService)
        {
            _eventManagementService = eventManagementService;
            RuleFor(e => e).MustAsync((f, token) => AttachmentIsMissing(f));
        }

        private async Task<bool> AttachmentIsMissing(EventParticipation eventParticipation)
        {
            var events = await _eventManagementService.GetAll();
            var selectedEvent = events.First(e => e.Id == eventParticipation.Event.Id);
            return !(selectedEvent.RequireAttachment && eventParticipation.Attachment == default);
        }
    }
}
