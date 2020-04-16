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

        private IEventManagementService _eventManagementService;
        
        public EventManagementViewModel(IEventManagementService eventManagementService)
        {
            _eventManagementService = eventManagementService;
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
            EventName = existingItem.Name;
            Active = existingItem.Active;

            EditModalOpen = true;
        }

        public async Task Save()
        {
            var eventObj = Events.FirstOrDefault(i => i.Id == DialogId);

            Action<Event> action = (ac) =>
            {
                ac.Active = Active;
                ac.Name = EventName;
                ac.AnnualMaximum = AnnualMaximumPoints;
                ac.Points = Points;
            };

            if(eventObj == default)
            {
                eventObj = new Event();                
                eventObj.Id = DialogId;
                action.Invoke(eventObj);
                await _eventManagementService.Create(eventObj);
            }
            else
            {
                action.Invoke(eventObj);
                await _eventManagementService.Update(eventObj);
            }
            
            EditModalOpen = false;
            Events = await _eventManagementService.GetAll();
        }
    }
}
