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
        public BaseMatIconButton accountButton;
        public BaseMatMenu accountMenu;
        public UserParticipation participation;
               
        private IActivityParticipationService _activityParticipationService;
        public MainViewModel(IActivityParticipationService activityParticipationService)
        {
            _activityParticipationService = activityParticipationService;            
        }

        public IEnumerable<Activity> activities { get; private set; }

        private IActivityManagementService ActivityManagement { get; set; }

        public string SelectedId { set; get; } = Guid.Empty.ToString();

        public string SelectedActivity { get; set; }
        public string NumberOfMinutes { get; set; }
        public DateTime SelectedDate { get; set; } = DateTime.MinValue;

        public async Task OnInit()
        {
            participation = new UserParticipation()
            {
                Activities = await _activityParticipationService.GetByRelativeMonthIndex(0)
            };
        }

        public async Task MonthChanged(MonthChangedEventArgs args)
        {
            participation.Activities = await _activityParticipationService.GetByRelativeMonthIndex(args.Month.RelativeIndex);
        }
        
        public async Task OnClick(MouseEventArgs e)
        {
            await this.accountMenu.OpenAsync(accountButton.Ref);
        }
    }
}
