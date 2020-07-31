using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.ViewModels
{
    public class DashboardViewModel : IViewModelBase, IActivityParticipationViewModel, IEventParticipationViewModel
    {
        public bool IsSaving { get; set; }
        public Guid Id { get; set; }
        public IEnumerable<Activity> Activities { get; set; }
        public IEnumerable<Event> Events { get; set; }

        public bool LoadingActivities { get; set; }
        public bool LoadingEvents { get; set; }

        public IEnumerable<PersistenceWrapper<ActivityParticipation>> ActivityParticipations { get; set; }
        public IEnumerable<PersistenceWrapper<EventParticipation>> EventParticipations { get; set; }

        private IActivityParticipationService _activityParticipationService;
        private IActivityManagementService _activityManagementService;

        private IEventParticipationService _eventParticipationService;
        private IEventManagementService _eventManagementService;

        public DashboardViewModel(
            IActivityParticipationService activityParticipationService,
            IActivityManagementService activityManagementService,
            IEventParticipationService eventParticipationService,
            IEventManagementService eventManagementService)
        {
            _activityParticipationService = activityParticipationService;
            _activityManagementService = activityManagementService;
            _eventParticipationService = eventParticipationService;
            _eventManagementService = eventManagementService; ;
            NewEventParticipation = new EventParticipation();
            NewActivityParticipation = new ActivityParticipation();
        }

        public EventParticipation NewEventParticipation { get; set; }
        public ActivityParticipation NewActivityParticipation { get; set; }
        public string PreviewFileType { get; set; }
        public string PreviewDataUrl { get; set; }
        public bool PreviewDialogIsOpen { get; set; }
        public string EventAttachmentFileLocation { get; set; }

        public string IconClass { get; set; } = "d-none";

        public int ActivityTabIndex { get; set; } = 1;
        public int EventTabIndex { get; set; } = 1;

        private IActivityManagementService ActivityManagement { get; set; }

        public string SelectedId { set; get; } = Guid.Empty.ToString();

        public int SelectedRelativeIndex { get; set; } = 0;

        public async Task ActivityParticipationDeleted(Guid id)
        {
            IconClass = "spinning-icon";
            await _activityParticipationService.Delete(id);
            await SetActivityParticipations();
            IconClass = "d-none";
        }

        public async Task EventParticipationDeleted(Guid id)
        {
            IconClass = "spinning-icon";
            await _eventParticipationService.Delete(id);
            await SetEventParticipations();
            IconClass = "d-none";
        }

        public async Task OnInit()
        {
            Activities = (await _activityManagementService.GetAll(CancellationToken.None)).Select(i => i.Model).Where(i => i.Active).ToList();
            Events = (await _eventManagementService.GetAll(CancellationToken.None)).Select(i => i.Model).Where(i => i.Active).ToList();
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
            await Task.WhenAll(SetActivityParticipations(), SetEventParticipations());
        }

        private async Task SetActivityParticipations()
        {
            LoadingActivities = true;
            ActivityParticipations = await _activityParticipationService.GetByRelativeMonthIndex(SelectedRelativeIndex, Id);
            LoadingActivities = false;
        }

        private async Task SetEventParticipations()
        {
            LoadingEvents = true;
            EventParticipations = await _eventParticipationService.GetByRelativeMonthIndex(SelectedRelativeIndex, Id);
            LoadingEvents = false;
        }

        public async Task PreviewAttachment(Guid id)
        {
            var eventParticipation = EventParticipations.FirstOrDefault(i => i.Model.Id == id);
            var bytes = await _eventParticipationService.DownloadFile(eventParticipation.Model.Attachment.FilePath);
            PreviewDialogIsOpen = true;
            PreviewDataUrl =$"data:{eventParticipation.Model.Attachment?.ContentType};base64,{Convert.ToBase64String(bytes)}";
            PreviewFileType = eventParticipation.Model.Attachment?.ContentType;
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
            IsSaving = true;
            IconClass = "spinning-icon";
            NewActivityParticipation.Id = Guid.NewGuid();
            NewActivityParticipation.PointsEarned = Math.Round(NewActivityParticipation.Minutes * 0.166666666667m);
            NewActivityParticipation.UserId = Id;

            await _activityParticipationService.Create(NewActivityParticipation);

            SelectedRelativeIndex = (DateTimeOffset.UtcNow.Month - NewActivityParticipation.SubmissionDate.Month);

            //clear out UI
            NewActivityParticipation = new ActivityParticipation();

            await SetActivityParticipations();
            IconClass = "d-none";
            IsSaving = false;
        }

        public async Task SaveEvent()
        {
            IsSaving = true;
            IconClass = "spinning-icon";
            var selectedEvent = Events.FirstOrDefault(i => i.Id == NewEventParticipation.Event.Id);

            NewEventParticipation.Id = Guid.NewGuid();
            NewEventParticipation.PointsEarned = selectedEvent.Points;
            NewEventParticipation.UserId = Id;

            await _eventParticipationService.Create(NewEventParticipation);

            SelectedRelativeIndex = (DateTimeOffset.UtcNow.Month - NewEventParticipation.SubmissionDate.Month);

            await SetEventParticipations();

            //clear out UI
            NewEventParticipation = new EventParticipation();

            IconClass = "d-none";
            IsSaving = false;
        }
    }
}
