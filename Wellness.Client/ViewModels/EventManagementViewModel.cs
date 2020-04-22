using MatBlazor;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Guid DialogId { get; set; } = Guid.Empty;

        public IEnumerable<Event> Events { get; private set; }

        public Event NewOrEditEvent { get; set; }

        private IEventManagementService _eventManagementService;
        
        public EventManagementViewModel(IEventManagementService eventManagementService)
        {
            _eventManagementService = eventManagementService;
            NewOrEditEvent = new Event();
        }

        public async Task OnInit()
        {
            Events = await _eventManagementService.GetAll();
        }

        public async Task Delete(Guid id)
        {
            await _eventManagementService.Disable(id);            
            Events = await _eventManagementService.GetAll();
        }

        public void New()
        {
            DialogId = Guid.NewGuid();            
            EditModalOpen = true;
        }

        public void Edit(Guid id)
        {
            DialogId = id;
            var existingItem = Events.FirstOrDefault(i => i.Id == id);

            NewOrEditEvent.Id = id;
            NewOrEditEvent.Name = existingItem.Name;
            NewOrEditEvent.Active = existingItem.Active;
            NewOrEditEvent.AnnualMaximum = existingItem.AnnualMaximum;
            NewOrEditEvent.RequireAttachment = existingItem.RequireAttachment;
            NewOrEditEvent.Points = existingItem.Points;            

            EditModalOpen = true;
        }

        public async Task Save()
        {
            var eventObj = Events.FirstOrDefault(i => i.Id == DialogId);
            
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
            Events = await _eventManagementService.GetAll();
        }
    }
}
