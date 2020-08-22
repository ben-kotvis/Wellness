using System.Collections.Generic;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.ViewModels
{
    public interface IActivityParticipationViewModel
    {
        string IconClass { get; set; }
        bool IsSaving { get; set; }
        ActivityParticipation NewActivityParticipation { get; set; }
        IEnumerable<Activity> Activities { get; set; }
        Task SaveActivity();
        bool CreateActivityIsOpen { get; set; }
        Task CancelActivitySubmission();
    }
}
