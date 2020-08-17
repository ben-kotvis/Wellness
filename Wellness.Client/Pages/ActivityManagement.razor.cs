using Microsoft.AspNetCore.Components;
using Wellness.Client.Components;
using Wellness.Client.ViewModels;

namespace Wellness.Client.Pages
{
    public class ActivityManagementComponent : WellnessComponentBase<ActivityManagementViewModel>
    {
        [Inject] public override ActivityManagementViewModel ViewModel { get; set; }
    }
}
