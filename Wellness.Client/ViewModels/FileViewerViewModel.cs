using MatBlazor;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.ViewModels
{    
    public class FileViewerViewModel : IViewModelBase
    {
        private IEventParticipationService _eventParticipationService;
        
        public FileViewerViewModel(IEventParticipationService eventParticipationService)
        {
            _eventParticipationService = eventParticipationService;
        }

        public async Task OnInit()
        {
            
        }
    }
}
