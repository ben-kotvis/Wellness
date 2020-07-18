using Microsoft.AspNetCore.Components;
using Wellness.Client.Components;
using Wellness.Client.ViewModels;

namespace Wellness.Client.Pages
{
    public class EventManagementComponent : WellnessComponentBase<EventManagementViewModel>
    {
        [Inject] public override EventManagementViewModel ViewModel { get; set; }

    }
}
