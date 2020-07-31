using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;
using Microsoft.AspNetCore.Components.Authorization;

namespace Wellness.Client.ViewModels
{
    public class HomeViewModel : IViewModelBase
    {
        public string IconClass { get; set; } = "d-none";
        public bool IsSaving { get; set; }
        public User NewOrEditUser { get; set; }
        private IProfileService _profileService;
        private IClientState _clientState;
        private NavigationManager _navigation;
        private AuthenticationStateProvider _authenticationStateProvider;

        public HomeViewModel(IProfileService profileService, IClientState clientState, NavigationManager navigation, AuthenticationStateProvider authenticationStateProvider)
        {
            _profileService = profileService;
            _clientState = clientState;
            _navigation = navigation;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task OnInit()
        {
            NewOrEditUser = new User();
            if (_clientState.CurrentUser != default)
            {
                _navigation.NavigateTo("/Dashboard");
            }
            else
            {
                var currentUser = await _profileService.GetCurrent(CancellationToken.None);
                if (currentUser != default)
                {
                    _clientState.CurrentUser = currentUser.Model;
                    _navigation.NavigateTo("/Dashboard");
                }
                var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
                NewOrEditUser.ProviderObjectId = state.User.FindFirst("oid").Value;
            }
        }


        public async Task Save()
        {
            IsSaving = true;
            IconClass = "spinning-icon";

            if (_clientState.CurrentUser == default)
            {
                await _profileService.Create(NewOrEditUser);
                _clientState.CurrentUser = NewOrEditUser;
            }
            else
            {
                await _profileService.Update(NewOrEditUser);
            }

            NewOrEditUser = new User();
            IconClass = "d-none";
            IsSaving = false;
            _navigation.NavigateTo("/Dashboard");
        }

    }
}
