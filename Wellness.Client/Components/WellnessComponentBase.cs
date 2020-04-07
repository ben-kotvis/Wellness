using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Client.ViewModels;

namespace Wellness.Client.Components
{
    public abstract class WellnessComponentBase<T> : ComponentBase where T : IViewModelBase
    {
        protected override async Task OnInitializedAsync()
        {
            await ViewModel.OnInit();
        }
        public abstract T ViewModel { get; set; }
    }
}
