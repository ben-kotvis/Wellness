using MatBlazor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Client.ViewModels;
using Wellness.Model;

namespace Wellness.Client.Components.UserParticipation.Events.Add
{
    public class NewEventComponent : ComponentBase
    {
        [Parameter] public IEventParticipationViewModel ViewModel { get; set; }
        [Parameter] public EventCallback OnSaveSelected { get; set; }
        [Parameter] public IEnumerable<Event> Events { get; set; }

         public void FilesReady(IMatFileUploadEntry[] files)
        {
            foreach (var file in files)
            {
                                
            }
        }
    }
}
