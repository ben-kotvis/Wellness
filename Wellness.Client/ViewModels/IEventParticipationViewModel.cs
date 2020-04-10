using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wellness.Client.ViewModels
{
    public interface IEventParticipationViewModel
    {
        string SelectedEventName { get; set; }
        string EventAttachmentId { get; set; }
        DateTime SelectedEventDate { get; set; } 
    }
}
