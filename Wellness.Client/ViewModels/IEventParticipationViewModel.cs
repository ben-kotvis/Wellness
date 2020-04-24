using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;
using Wellness.Model.ModelValidation;

namespace Wellness.Client.ViewModels
{
    public interface IEventParticipationViewModel
    {
        EventParticipationValidation EventValidation { get; set; }
        IEnumerable<Event> Events { get; set; }
        string PreviewFileType { get; set; }
        string PreviewDataUrl { get; set; }
        EventParticipation NewEventParticipation { get; set; }
        bool PreviewDialogIsOpen { get; set; }
        Event SelectedEvent { get; set; }
        DateTime SelectedEventDate { get; set; }
        Task EventFileAttached(EventAttachmentArgs args);
        Task PreviewAttachment(Guid id);
        Task SaveEvent();
    }
}
