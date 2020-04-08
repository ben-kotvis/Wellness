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
        [Parameter] public IEnumerable<Activity> Activities { get; set; }
        [Parameter] public string SelectedActivityName { get; set; }
        [Parameter] public int NumberOfMinutes { get; set; }
        [Parameter] public DateTime SelectedActivityDate { get; set; } = DateTime.MinValue;
        [Parameter] public EventCallback OnSaveSelected { get; set; }
    }
}
