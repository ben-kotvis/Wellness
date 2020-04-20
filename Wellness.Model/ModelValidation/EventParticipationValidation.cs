using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model.ModelValidation
{
    public class EventParticipationValidation : AbstractValidator<EventParticipationSubmission>
    {
        public EventParticipationValidation()
        {
            RuleFor(e => e.UserId).NotEmpty();
            RuleFor(e => e.EventId).NotEmpty();
            RuleFor(e => e.Date).NotEqual(default(DateTimeOffset));
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

        private async Task<bool> AttachmentIsMissing(EventParticipationSubmission eventParticipation)
        {
            var events = await _eventManagementService.GetAll();
            var selectedEvent = events.First(e => e.Id == eventParticipation.EventId);
            return !(selectedEvent.RequireAttachment && eventParticipation.AttachmentId == default);
        }
    }
}
