using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wellness.Client.Components.Shared
{
    public class MaterialMenuComponent : ComponentBase
    {
        [Parameter]
        public string Icon { get; set; }

        [Parameter]
        public Dictionary<string, string> Items { get; set; }

        [Parameter]
        public EventCallback<string> OnItemSelected { get; set; }

        public BaseMatIconButton MenuButton;
        public BaseMatMenu Menu;

        public async Task OnMenuButtonClicked(MouseEventArgs mouseEventArgs)
        {
            await this.Menu.OpenAsync(MenuButton.Ref);
        }

        public async Task Select(string selectedKey)
        {
            await OnItemSelected.InvokeAsync(selectedKey);
        }
    }
}
