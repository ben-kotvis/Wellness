using FluentValidation;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Domain.ModelValidation
{
    public class EventParticipationValidation : AbstractValidator<EventParticipation>
    {
        private readonly IPersistanceReaderService<Event> _eventManagementService;

        public EventParticipationValidation(IPersistanceReaderService<Event> eventManagementService)
        {
            _eventManagementService = eventManagementService;
            RuleFor(e => e.Event).NotNull();
            RuleFor(e => e.SubmissionDate).NotEqual(default(DateTime));
            RuleFor(e => e.Attachment).MustAsync(AttachmentIsMissing);
        }


        private async Task<bool> AttachmentIsMissing(EventParticipation eventParticipation, EventAttachment attachment, CancellationToken cancellationToken)
        {
            if (eventParticipation.Event == default)
            {
                return true;
            }

            var events = await _eventManagementService.GetAll(cancellationToken);

            var eventObj = events.FirstOrDefault(i => i.Model.Id == eventParticipation.Event.Id);

            return !(eventObj.Model.RequireAttachment && attachment == default);
        }
    }

}
