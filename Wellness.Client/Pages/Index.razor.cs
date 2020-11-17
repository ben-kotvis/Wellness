using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Threading.Tasks;
using Wellness.Client.Components;
using Wellness.Client.ViewModels;

namespace Wellness.Client.Pages
{
    public class IndexComponent : ComponentBase
    {
        [Inject] public AuthenticationStateProvider context { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var state = await context.GetAuthenticationStateAsync();
            if (state.User.Identity.IsAuthenticated)
            {
                string redirectUri = $"{Navigation.Uri}/ProfileInfo";
                Navigation.NavigateTo(redirectUri);
            }
        }
    }
}
