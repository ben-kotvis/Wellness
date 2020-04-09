using MatBlazor;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.ViewModels
{    
    public class ActivityManagementViewModel : IViewModelBase
    {
        public bool EditModalOpen { get; set; } = false;
        public string ActivityName { get; set; }
        public bool Active { get; set; }

        public Guid DialogId { get; set; } = Guid.Empty;

        public IEnumerable<Activity> Activities { get; private set; }

        private IActivityManagementService _activityManagementService;
        
        public ActivityManagementViewModel(IActivityManagementService activityManagementService)
        {
            _activityManagementService = activityManagementService;
        }

        public async Task OnInit()
        {
            Activities = await _activityManagementService.GetAll();
        }

        public async Task Delete(Guid id)
        {
            await _activityManagementService.Disable(id);            
            Activities = await _activityManagementService.GetAll();
        }

        public async Task New()
        {
            DialogId = Guid.NewGuid();            
            EditModalOpen = true;
        }

        public async Task Edit(Guid id)
        {
            DialogId = id;
            var existingItem = Activities.FirstOrDefault(i => i.Id == id);
            ActivityName = existingItem.Name;
            Active = existingItem.Active;

            EditModalOpen = true;
        }

        public async Task Save()
        {
            var activity = Activities.FirstOrDefault(i => i.Id == DialogId);

            Action<Activity> action = (ac) =>
            {
                ac.Active = Active;
                ac.Name = ActivityName;
            };

            if(activity == default)
            {
                activity = new Activity();                
                activity.Id = DialogId;
                action.Invoke(activity);
                await _activityManagementService.Create(activity);
            }
            else
            {
                action.Invoke(activity);
                await _activityManagementService.Update(activity);
            }
            
            EditModalOpen = false;
            Activities = await _activityManagementService.GetAll();
        }
    }
}
