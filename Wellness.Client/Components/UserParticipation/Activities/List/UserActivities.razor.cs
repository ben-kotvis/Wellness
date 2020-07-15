using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Client.ViewModels;
using Wellness.Model;

namespace Wellness.Client.Components.UserParticipation.Activities.List
{
    public class UserActivitiesComponent : ComponentBase
    {
        [Parameter] public IActivityParticipationViewModel ViewModel { get; set; }

        [Parameter] public IEnumerable<PersistenceWrapper<ActivityParticipation>> ActivityParticipations { get; set; }

        [Parameter] public EventCallback<Guid> OnConfirmDelete { get; set; }

        public bool dialogIsOpen = false;

        public Guid SelectedId { get; set; }
    }
}
