using MatBlazor;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;
using Wellness.Model.ModelValidation;

namespace Wellness.Client.ViewModels
{    
    public class HomeViewModel : IViewModelBase, IActivityParticipationViewModel, IEventParticipationViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<Activity> Activities { get; set; }
        public IEnumerable<Event> Events { get; set; }

        public IEnumerable<ActivityParticipation> ActivityParticipations { get; set; }
        public IEnumerable<EventParticipation> EventParticipations { get; set; }

        private IActivityParticipationService _activityParticipationService;
        private IActivityManagementService _activityManagementService;
        
        private IEventParticipationService _eventParticipationService;
        private IEventManagementService _eventManagementService;

        public HomeViewModel(
            IActivityParticipationService activityParticipationService,
            IActivityManagementService activityManagementService,
            IEventParticipationService eventParticipationService,
            IEventManagementService eventManagementService,
            EventParticipationValidation eventParticipationValidation)
        {
            _activityParticipationService = activityParticipationService;
            _activityManagementService = activityManagementService;
            _eventParticipationService = eventParticipationService;
            _eventManagementService = eventManagementService;
            EventValidation = eventParticipationValidation;
            NewEventParticipation = new EventParticipation();
        }

        public EventParticipationValidation EventValidation { get; set; }
        public EventParticipation NewEventParticipation { get; set; }
        public string PreviewFileType { get; set; }
        public string PreviewDataUrl { get; set; }
        public bool PreviewDialogIsOpen { get; set; }

        public int ActivityTabIndex { get; set; } = 1;
        public int EventTabIndex { get; set; } = 1;

        private IActivityManagementService ActivityManagement { get; set; }

        public string SelectedId { set; get; } = Guid.Empty.ToString();

        public int SelectedRelativeIndex { get; set; } = 0;
        public Activity SelectedActivity { get; set; }
        public int NumberOfMinutes { get; set; }
        public DateTime SelectedActivityDate { get; set; } = DateTime.MinValue;

        public Event SelectedEvent { get; set; }
        public string EventAttachmentFileLocation { get; set; }
        public DateTime SelectedEventDate { get; set; } = DateTime.MinValue;

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

            Activities = (await _activityManagementService.GetAll()).Where(i => i.Active);            
            Events = (await _eventManagementService.GetAll()).Where(i => i.Active);            
        }

        public async Task EventFileAttached(EventAttachmentArgs args)
        {
            EventAttachmentFileLocation = await _eventParticipationService.UploadFile(args.Name, args.Type, args.WriteToStreamAsync);
            NewEventParticipation.Attachment = new EventAttachment()
            {
                ContentType = args.Type,
                FilePath = EventAttachmentFileLocation,
                FileSize = args.Size,
                Name = args.Name
            };
        }

        public async Task MonthChanged(MonthChangedEventArgs args)
        {
            SelectedRelativeIndex = args.Month.RelativeIndex;
            await SetActivityParticipations();
            await SetEventParticipations();
        }

        private async Task SetActivityParticipations()
        {
            ActivityParticipations = await _activityParticipationService.GetByRelativeMonthIndex(SelectedRelativeIndex, Id);            
        }

        private async Task SetEventParticipations()
        {
            EventParticipations = await _eventParticipationService.GetByRelativeMonthIndex(SelectedRelativeIndex, Id);            
        }

        public async Task PreviewAttachment(Guid id)
        {
            var eventParticipation = EventParticipations.FirstOrDefault(i => i.Id == id);

            var bytes = await File.ReadAllBytesAsync(eventParticipation.Attachment.FilePath);
            PreviewDialogIsOpen = true; 
            PreviewDataUrl = $"data:{eventParticipation.Attachment?.ContentType};base64,{Convert.ToBase64String(bytes)}"; 
            PreviewFileType = eventParticipation.Attachment?.ContentType;            
        }

        public async Task SetUser(Guid id)
        {
            Console.WriteLine(id);
            Id = id;
            await SetActivityParticipations();
            await SetEventParticipations();
        }

        public async Task SaveActivity()
        {
            await _activityParticipationService.Create(new ActivityParticipation()
            {
                Id = Guid.NewGuid(),
                ActivityName = SelectedActivity.Name,
                Minutes = NumberOfMinutes,
                ParticipationDate = SelectedActivityDate,
                UserId = Id
            });

            SelectedRelativeIndex = (DateTimeOffset.UtcNow.Month - SelectedActivityDate.Month);
            
            await SetActivityParticipations();

            ActivityTabIndex = 1;

            //clear out UI
            SelectedActivity = default;
            SelectedActivityDate = default;
            NumberOfMinutes = 0;
        }

        public async Task SaveEvent()
        {
            var selectedEvent = Events.FirstOrDefault(i => i.Id == NewEventParticipation.Event.Id);

            NewEventParticipation.Id = Guid.NewGuid();            
            NewEventParticipation.PointsEarned = selectedEvent.Points;
            NewEventParticipation.UserId = Id;

            await _eventParticipationService.Create(NewEventParticipation);                
                        
            SelectedRelativeIndex = (DateTimeOffset.UtcNow.Month - SelectedEventDate.Month);

            await SetEventParticipations();

            EventTabIndex = 1;

            //clear out UI
            SelectedEvent = default;
            SelectedEventDate = default;            
        }
    }
}
