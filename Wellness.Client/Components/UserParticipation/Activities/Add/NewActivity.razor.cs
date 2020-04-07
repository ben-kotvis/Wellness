using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Client.ViewModels;
using Wellness.Model;

namespace Wellness.Client.Components.UserParticipation.Activities.Add
{
    public class NewActivityComponent : WellnessComponentBase<NewActivityViewModel>
    {
        [Inject] public override NewActivityViewModel ViewModel { get; set; }
    }
}
