using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Client.ViewModels;
using Wellness.Model;

namespace Wellness.Client.Components.UserParticipation.Events.List
{
    public class UserEventsComponent : ComponentBase
    { 

        [Inject] IJSRuntime JSRuntime { get; set; }
        [Parameter] public IEnumerable<EventParticipation> EventParticipations { get; set; }

        [Parameter] public EventCallback<Guid> OnConfirmDelete { get; set; }

        public bool dialogIsOpen = false;

        public Guid SelectedId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            foreach(var p in EventParticipations)
            {
                if (p.Attachment != default)
                {
                    Console.WriteLine(p.Attachment.ContentType);

                    var bytes = await File.ReadAllBytesAsync(p.Attachment.LocalPath);
                    var text = await JSRuntime.InvokeAsync<string>("createUrlFromArray", bytes, p.Attachment.ContentType);
                    Console.WriteLine(text);
                }
            }
        }
    }
}
