using MatBlazor;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.ViewModels
{    
    public class MainViewModel : IViewModelBase, IActivityParticipationViewModel, IEventParticipationViewModel
    {
        public IEnumerable<Activity> Activities { get; private set; }
        public IEnumerable<Event> Events { get; private set; }

        public IEnumerable<ActivityParticipation> ActivityParticipations { get; set; }
        public IEnumerable<EventParticipation> EventParticipations { get; set; }

        private IActivityParticipationService _activityParticipationService;
        private IActivityManagementService _activityManagementService;
        
        private IEventParticipationService _eventParticipationService;
        private IEventManagementService _eventManagementService;

        public MainViewModel(
            IActivityParticipationService activityParticipationService, 
            IActivityManagementService activityManagementService,
            IEventParticipationService eventParticipationService,
            IEventManagementService eventManagementService)
        {
            _activityParticipationService = activityParticipationService;
            _activityManagementService = activityManagementService;
            _eventParticipationService = eventParticipationService;
            _eventManagementService = eventManagementService;
        }

        public int ActivityTabIndex { get; set; }
        public int EventTabIndex { get; set; }

        private IActivityManagementService ActivityManagement { get; set; }

        public string SelectedId { set; get; } = Guid.Empty.ToString();

        public int SelectedRelativeIndex { get; set; } = 0;
        public string SelectedActivityName { get; set; }
        public int NumberOfMinutes { get; set; }
        public DateTime SelectedActivityDate { get; set; } = DateTime.MinValue;

        public string SelectedEventName { get; set; }
        public string EventAttachmentId { get; set; }
        public DateTime SelectedEventDate { get; set; } 

        public async Task ActivityParticipationDeleted(Guid id)
        {
            await _activityParticipationService.Delete(id);
            await SetActivityParticipations();
        }

        public async Task EventParticipationDeleted(Guid id)
        {
            await _eventParticipationService.Delete(id);
            await SetEventParticipations();
        }

        public async Task OnInit()
        {
            await SetActivityParticipations();
            await SetEventParticipations();
            Activities = await _activityManagementService.GetAll();            
            Events = await _eventManagementService.GetAll();
        }

        public async Task MonthChanged(MonthChangedEventArgs args)
        {
            SelectedRelativeIndex = args.Month.RelativeIndex;
            await SetActivityParticipations();
        }

        private async Task SetActivityParticipations()
        {
            ActivityParticipations = await _activityParticipationService.GetByRelativeMonthIndex(SelectedRelativeIndex);            
        }

        private async Task SetEventParticipations()
        {
            EventParticipations = await _eventParticipationService.GetByRelativeMonthIndex(SelectedRelativeIndex);            
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

        public async Task SaveEvent()
        {
            await _eventParticipationService.Create(new EventParticipation()
            {
                Id = Guid.NewGuid(),
                EventName = SelectedActivityName,
                Points = NumberOfMinutes,
                Date = SelectedActivityDate                
            });

            SelectedRelativeIndex = (DateTimeOffset.UtcNow.Month - SelectedActivityDate.Month);

            await SetEventParticipations();

            EventTabIndex = 0;
        }
    }
}
