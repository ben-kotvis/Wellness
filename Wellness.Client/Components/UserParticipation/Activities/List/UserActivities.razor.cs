using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.Components.UserParticipation.Activities.List
{
    public class UserActivitiesComponent : ComponentBase
    {

        [Inject] IActivityParticipationService activityParticipationService { get; set; }

        [Parameter]
        public IEnumerable<ActivityParticipation> ActivityParticipations { get; set; }


        public BaseMatIconButton activityItemButton;

        public bool dialogIsOpen = false;
        public Guid selectedId;

        public void OpenDialog()
        {
            dialogIsOpen = true;
        }

        public async Task OkClick()
        {
            await activityParticipationService.Delete(selectedId);
            dialogIsOpen = false;
        }

        public void DeleteActivity(MouseEventArgs e, Guid id)
        {
            selectedId = id;
            OpenDialog();
        }
    }
}
