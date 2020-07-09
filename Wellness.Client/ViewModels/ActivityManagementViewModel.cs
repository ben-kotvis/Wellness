using MatBlazor;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Client.Services.Mock;
using Wellness.Model;

namespace Wellness.Client.ViewModels
{    
    public class ActivityManagementViewModel : IViewModelBase
    {
        public bool EditModalOpen { get; set; } = false;
        public string ActivityName { get; set; }
        public bool Active { get; set; } = true;
        public bool Debug { get; set; } = false;
        public Guid DialogId { get; set; } = Guid.Empty;

        public IEnumerable<PersistenceWrapper<Activity>> Activities { get; private set; }

        private IActivityManagementService _activityManagementService;
        
        public ActivityManagementViewModel(IActivityManagementService activityManagementService)
        {
            _activityManagementService = activityManagementService;

#if DEBUG
            Debug = true;
#endif
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

        public async Task Load()
        {
            foreach (var item in MockDataGenerator.GetActivities())
            {
                await _activityManagementService.Create(item.Model);
            }
        }

        public async Task Edit(Guid id)
        {
            DialogId = id;
            var existingItem = Activities.FirstOrDefault(i => i.Model.Id == id);
            ActivityName = existingItem.Model.Name;
            Active = existingItem.Model.Active;

            EditModalOpen = true;
        }

        public async Task Save()
        {
            var activity = Activities.FirstOrDefault(i => i.Model.Id == DialogId)?.Model;

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
