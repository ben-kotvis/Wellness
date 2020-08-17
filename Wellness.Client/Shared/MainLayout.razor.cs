using MatBlazor;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Wellness.Client.ViewModels;

namespace Wellness.Client.Shared
{
    public class MainLayoutComponent : LayoutComponentBase
    {
        public BaseMatIconButton MenuButton;
        public BaseMatMenu Menu;

        [Inject] public MainViewModel ViewModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await ViewModel.OnInit();
        }
    }
}
