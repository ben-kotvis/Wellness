using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using Wellness.Client.ViewModels;
using Wellness.Model;

namespace Wellness.Client.Components.UserParticipation.Activities.List
{
    public class UserActivitiesComponent : ComponentBase
    {
        [Parameter] public IActivityParticipationViewModel ViewModel { get; set; }

        [Parameter] public IEnumerable<PersistenceWrapper<ActivityParticipation>> ActivityParticipations { get; set; }

        [Parameter] public EventCallback<Guid> OnDeleteRequested { get; set; }
        [Parameter] public EventCallback OnConfirmDelete { get; set; }

        public Guid SelectedId { get; set; }
    }
}
