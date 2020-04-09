using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Client.ViewModels;
using Wellness.Model;

namespace Wellness.Client.Components.UserParticipation.Activities.Add
{
    public class NewActivityComponent : ComponentBase
    {
        [Parameter] public IActivityParticipationViewModel ViewModel { get; set; }
        [Parameter] public EventCallback OnSaveSelected { get; set; }
        [Parameter] public IEnumerable<Activity> Activities { get; set; }
    }
}
