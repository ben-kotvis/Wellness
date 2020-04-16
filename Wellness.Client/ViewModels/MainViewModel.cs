using MatBlazor;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.ViewModels
{    
    public class MainViewModel : IViewModelBase
    {
        public User CurrentUser { get; private set; }

        private IProfileService _profileService;
        
        public MainViewModel(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public async Task OnInit()
        {
            CurrentUser = await _profileService.GetCurrent();
        }

    }
}
