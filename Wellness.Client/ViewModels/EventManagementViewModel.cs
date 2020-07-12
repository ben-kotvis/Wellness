using MatBlazor;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Client.Services.Mock;
using Wellness.Model;

namespace Wellness.Client.ViewModels
{    
    public class EventManagementViewModel : IViewModelBase
    {
        public bool EditModalOpen { get; set; } = false;
        public string EventName { get; set; }
        public int Points { get; set; }
        public int AnnualMaximumPoints { get; set; }
        public bool Active { get; set; }

        public bool Debug { get; set; } = false;
        public Guid DialogId { get; set; } = Guid.Empty;
        
        public IEnumerable<PersistenceWrapper<Event>> Events { get; private set; }

        public Event NewOrEditEvent { get; set; }

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
            Events = await _eventManagementService.GetAll(CancellationToken.None);
        }

        public async Task Load()
        {
            foreach (var item in MockDataGenerator.GetEvents())
            {
                await _eventManagementService.Create(item.Model);
            }
        }

        public async Task Delete(Guid id)
        {
            await _eventManagementService.Disable(id);            
            Events = await _eventManagementService.GetAll(CancellationToken.None);
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
            NewOrEditEvent.Active = existingItem.Model.Active;
            NewOrEditEvent.AnnualMaximum = existingItem.Model.AnnualMaximum;
            NewOrEditEvent.RequireAttachment = existingItem.Model.RequireAttachment;
            NewOrEditEvent.Points = existingItem.Model.Points;            

            EditModalOpen = true;
        }

        public async Task Save()
        {
            var eventObj = Events.FirstOrDefault(i => i.Model.Id == DialogId);
            
            if(eventObj == default)
            {
                NewOrEditEvent.Id = DialogId;
                await _eventManagementService.Create(NewOrEditEvent);
            }
            else
            {
                await _eventManagementService.Update(NewOrEditEvent);
            }
            
            EditModalOpen = false;
            Events = await _eventManagementService.GetAll(CancellationToken.None);

            NewOrEditEvent = new Event();
        }
    }
}
