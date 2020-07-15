using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Wellness.Client.Components.Shared
{
    public class ConfirmationDialogComponent : ComponentBase
    {
        [Parameter]
        public bool IsOpen { get; set; }
        
        [Parameter]
        public Guid SelectedId { get; set; }
                
        [Parameter]
        public EventCallback<Guid> OnConfirmDelete { get; set; }


        public string IconClass { get; set; } = "d-none";
        public async Task Confirm()
        {
            IconClass = "spinning-icon";
            await OnConfirmDelete.InvokeAsync(SelectedId);
            IsOpen = false;
            IconClass = "d-none";
        }
    }
}
