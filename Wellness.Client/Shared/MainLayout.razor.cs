using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wellness.Client.Shared
{
    public class MainLayoutComponent : LayoutComponentBase
    {
        public Dictionary<string, string> MainMenuItems { get; set; }


        protected override async Task OnInitializedAsync()
        {
            MainMenuItems = new Dictionary<string, string>();
            MainMenuItems.Add("profile", "Profile");
            MainMenuItems.Add("logout", "Logout");
        }

        public async Task AccountMenuItemSelected(string selectedMenuItem)
        {
            Console.WriteLine(selectedMenuItem);
        }
    }
}
