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
        public IEnumerable<ActivityParticipation> ActivityParticipations { get; set; }

        private IActivityParticipationService _activityParticipationService;
        private IActivityManagementService _activityManagementService;
        public MainViewModel(IActivityParticipationService activityParticipationService, IActivityManagementService activityManagementService)
        {
            _activityParticipationService = activityParticipationService;
            _activityManagementService = activityManagementService;
        }

        public IEnumerable<Activity> Activities { get; private set; }

        public int ActivityTabIndex { get; set; }

        private IActivityManagementService ActivityManagement { get; set; }

        public string SelectedId { set; get; } = Guid.Empty.ToString();

        public string SelectedActivityName { get; set; }
        public int NumberOfMinutes { get; set; }
        public DateTime SelectedActivityDate { get; set; } = DateTime.MinValue;

        public async Task ActivityParticipationDeleted(Guid id)
        {
            await _activityParticipationService.Delete(id);
        }

        public async Task OnInit()
        {
            ActivityParticipations = await _activityParticipationService.GetByRelativeMonthIndex(0);
            Activities = await _activityManagementService.GetAll();
        }

        public async Task MonthChanged(MonthChangedEventArgs args)
        {
            ActivityParticipations = await _activityParticipationService.GetByRelativeMonthIndex(args.Month.RelativeIndex);
        }

        public async Task SaveActivity()
        {
            await _activityParticipationService.Create(new ActivityParticipation()
            {
                Id = Guid.NewGuid(),
                ActivityName = SelectedActivityName,
                Minutes = NumberOfMinutes,
                ParticipationDate = SelectedActivityDate
            });

            ActivityTabIndex = 0;
        }

    }
}
