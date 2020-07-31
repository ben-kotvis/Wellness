using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Wellness.Client.Components;
using Wellness.Client.ViewModels;

namespace Wellness.Client.Pages
{
    public class DashboardComponent : WellnessComponentBase<DashboardViewModel>
    {
        [Inject] public override DashboardViewModel ViewModel { get; set; }
        [Parameter] public Guid SelectedUserId { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            await ViewModel.SetUser(SelectedUserId);
        }
    }
}
