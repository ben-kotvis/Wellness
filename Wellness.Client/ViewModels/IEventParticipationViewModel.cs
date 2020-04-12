using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.ViewModels
{
    public interface IEventParticipationViewModel
    {
        Event SelectedEvent { get; set; }
        Guid EventAttachmentId { get; set; }
        DateTime SelectedEventDate { get; set; }
        Task EventFileAttached(EventAttachmentArgs args);
    }
}
