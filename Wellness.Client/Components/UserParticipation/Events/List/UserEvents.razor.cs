using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wellness.Client.ViewModels;
using Wellness.Model;

namespace Wellness.Client.Components.UserParticipation.Events.List
{
    public class UserEventsComponent : ComponentBase
    {
        [Parameter] public IEventParticipationViewModel ViewModel { get; set; }

        [Parameter] public IEnumerable<PersistenceWrapper<EventParticipation>> EventParticipations { get; set; }

        [Parameter] public EventCallback<Guid> OnConfirmDelete { get; set; }

        public bool dialogIsOpen = false;

        public Guid SelectedId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
    }
}
