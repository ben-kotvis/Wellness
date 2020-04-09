using MatBlazor;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.ViewModels
{    
    public class MainViewModel : IViewModelBase, IActivityParticipationViewModel
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

        public int SelectedRelativeIndex { get; set; } = 0;
        public string SelectedActivityName { get; set; }
        public int NumberOfMinutes { get; set; }
        public DateTime SelectedActivityDate { get; set; } = DateTime.MinValue;

        public async Task ActivityParticipationDeleted(Guid id)
        {
            await _activityParticipationService.Delete(id);
            await SetActivityParticipations();
        }

        public async Task OnInit()
        {
            await SetActivityParticipations();
            Activities = await _activityManagementService.GetAll();            
        }

        public async Task MonthChanged(MonthChangedEventArgs args)
        {
            SelectedRelativeIndex = args.Month.RelativeIndex;
            await SetActivityParticipations();
        }

        private async Task SetActivityParticipations()
        {
            ActivityParticipations = await _activityParticipationService.GetByRelativeMonthIndex(SelectedRelativeIndex);
            Console.WriteLine($"Activity Count: {ActivityParticipations.Count()}");
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

            SelectedRelativeIndex = (DateTimeOffset.UtcNow.Month - SelectedActivityDate.Month);
            
            await SetActivityParticipations();

            ActivityTabIndex = 0;
        }

    }
}
