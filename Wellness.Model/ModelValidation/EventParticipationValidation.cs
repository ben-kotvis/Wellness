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
        private readonly IEventManagementService _eventManagementService;        

        public EventParticipationValidation(IEventManagementService eventManagementService)
        {
            _eventManagementService = eventManagementService;
            RuleFor(e => e.Event).NotNull();
            RuleFor(e => e.SubmissionDate).NotEqual(default(DateTime));
            RuleFor(e => e.Attachment).MustAsync(async (f, a, token) => await AttachmentIsMissing(f, a));
        }


        private async Task<bool> AttachmentIsMissing(EventParticipation eventParticipation, EventAttachment attachment)
        {
            if(eventParticipation.Event == default)
            {
                return true;
            }

            var events = await _eventManagementService.GetAll();

            var eventObj = events.FirstOrDefault(i => i.Model.Id == eventParticipation.Event.Id);

            return !(eventObj.Model.RequireAttachment && attachment == default);
        }
    }

}
