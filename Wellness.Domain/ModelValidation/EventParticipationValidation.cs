using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Domain.ModelValidation
{
    public class EventParticipationValidation : AbstractValidator<EventParticipation>
    {
        private readonly IPersistanceReaderService<Event> _eventManagementService;
        private readonly IRequestDependencies<Event> _requestDependencies;

        public EventParticipationValidation(IPersistanceReaderService<Event> eventManagementService, IRequestDependencies<Event> requestDependencies)
        {
            _requestDependencies = requestDependencies;
            _eventManagementService = eventManagementService;
            RuleFor(e => e.Event).NotNull();
            RuleFor(e => e.SubmissionDate).NotEqual(default(DateTime));
            RuleFor(e => e.Attachment).MustAsync(AttachmentIsMissing);
            RuleFor(e => e.Attachment).Must(ValidateImageAttachment).WithMessage("File must be in an image format.");
        }

        private bool ValidateImageAttachment(EventAttachment eventAttachment)
        {
            if(eventAttachment != default)
            {
                return eventAttachment.ContentType.StartsWith("image");
            }
            return true;
        }

        private async Task<bool> AttachmentIsMissing(EventParticipation eventParticipation, EventAttachment attachment, CancellationToken cancellationToken)
        {
            if (eventParticipation.Event == default)
            {
                return true;
            }

            var events = await _eventManagementService.GetAll(_requestDependencies.CompanyId, cancellationToken);

            var eventObj = events.FirstOrDefault(i => i.Model.Id == eventParticipation.Event.Id);

            return !(eventObj.Model.RequireAttachment && attachment == default);
        }
    }

}
