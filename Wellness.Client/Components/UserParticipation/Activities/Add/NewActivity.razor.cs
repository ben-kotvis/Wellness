using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Wellness.Client.ViewModels;
using Wellness.Model;

namespace Wellness.Client.Components.UserParticipation.Activities.Add
{
    public class NewActivityComponent : ComponentBase
    {
        [Parameter] public IActivityParticipationViewModel ViewModel { get; set; }
        [Parameter] public IEnumerable<Activity> Activities { get; set; }
        [Parameter] public EventCallback OnActivitySubmissionCompleted { get; set; }
    }
}
