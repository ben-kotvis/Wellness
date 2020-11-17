using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Client.Services.Mock;
using Wellness.Model;

namespace Wellness.Client.ViewModels
{
    public class EventManagementViewModel : IViewModelBase
    {
        public string IconClass { get; set; } = "d-none";
        public bool IsSaving { get; set; } = false;
        public bool EditModalOpen { get; set; } = false;
        public bool DeleteDialogIsOpen { get; set; } = false;
        public Guid SelectedDeleteId { get; set; }

        public bool Debug { get; set; } = false;
        public Guid DialogId { get; set; } = Guid.Empty;

        public IEnumerable<PersistenceWrapper<Event>> Events { get; private set; }

        public Event NewOrEditEvent { get; set; }

        public IEnumerable<string> MatIconNames { get; set; }

        private IEventManagementService _eventManagementService;

        public EventManagementViewModel(IEventManagementService eventManagementService)
        {
            _eventManagementService = eventManagementService;
            NewOrEditEvent = new Event()
            {
                Active = true
            };
#if DEBUG
            Debug = true;
#endif
        }

        public async Task OnInit()
        {
            Events = await _eventManagementService.GetAll(Guid.Empty, CancellationToken.None);
            MatIconNames = GetConstants(typeof(MatBlazor.MatIconNames));
        }
        private List<string> GetConstants(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Static)
                      .Where(f => f.PropertyType == typeof(string))
                      .Select(f => (string)f.GetValue(null)).ToList();
        }

        public async Task Load()
        {
            foreach (var item in MockDataGenerator.GetEvents())
            {
                await _eventManagementService.Create(item.Model);
            }
        }

        public void Delete(Guid id)
        {
            DeleteDialogIsOpen = true;
            SelectedDeleteId = id;
        }

        public async Task OnDeleteConfirmed(Guid id)
        {
            await _eventManagementService.Disable(id);
            Events = await _eventManagementService.GetAll(Guid.Empty, CancellationToken.None);

            DeleteDialogIsOpen = false;
            SelectedDeleteId = default;
        }

        public void New()
        {
            DialogId = Guid.NewGuid();
            EditModalOpen = true;
        }

        public void Edit(Guid id)
        {
            DialogId = id;
            var existingItem = Events.FirstOrDefault(i => i.Model.Id == id);

            NewOrEditEvent.Id = id;
            NewOrEditEvent.Name = existingItem.Model.Name;
            NewOrEditEvent.IconName = existingItem.Model.IconName;
            NewOrEditEvent.Active = existingItem.Model.Active;
            NewOrEditEvent.AnnualMaximum = existingItem.Model.AnnualMaximum;
            NewOrEditEvent.RequireAttachment = existingItem.Model.RequireAttachment;
            NewOrEditEvent.Points = existingItem.Model.Points;

            EditModalOpen = true;
        }

        public async Task Save()
        {
            IsSaving = true;
            IconClass = "spinning-icon";
            var eventObj = Events.FirstOrDefault(i => i.Model.Id == DialogId);

            if (eventObj == default)
            {
                NewOrEditEvent.Id = DialogId;
                await _eventManagementService.Create(NewOrEditEvent);
            }
            else
            {
                await _eventManagementService.Update(NewOrEditEvent);
            }

            Events = await _eventManagementService.GetAll(Guid.Empty, CancellationToken.None);

            EditModalOpen = false;
            NewOrEditEvent = new Event();
            DialogId = default;
            IconClass = "d-none";
            IsSaving = false;
        }
    }
}
