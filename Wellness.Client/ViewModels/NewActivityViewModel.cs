using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Client.ViewModels;
using Wellness.Model;

namespace Wellness.Client.ViewModels
{
    public class NewActivityViewModel : IViewModelBase
    {
        private IActivityManagementService _activityManagement;
        private IActivityParticipationService _activityPartcipation;
        public NewActivityViewModel(IActivityManagementService activityManagement, IActivityParticipationService activityPartcipation)
        {
            this._activityManagement = activityManagement;
            this._activityPartcipation = activityPartcipation;
        }


        public async Task OnInit()
        {
            activities = await _activityManagement.GetAll();
        }

        public IEnumerable<Activity> activities { get; private set; }


        public string SelectedId { set; get; } = Guid.Empty.ToString();

        public string SelectedActivityName { get; set; }
        public int NumberOfMinutes { get; set; }
        public DateTime SelectedDate { get; set; } = DateTime.MinValue;

        public async Task Save()
        {
            await _activityPartcipation.Create(new ActivityParticipation() 
            { 
                Id = Guid.NewGuid(), 
                ActivityName = SelectedActivityName, 
                Minutes = NumberOfMinutes, 
                ParticipationDate = SelectedDate 
            });
        }
    }
}
