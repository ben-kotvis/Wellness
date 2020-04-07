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
        [Parameter]
        public IEnumerable<ActivityParticipation> ActivityParticipations { get; set; }


        public BaseMatIconButton activityItemButton;

        public bool dialogIsOpen = false;
        public string name = null;
        public string animal = null;
        public string dialogAnimal = null;

        public void OpenDialog()
        {
            dialogAnimal = null;
            dialogIsOpen = true;
        }

        public void OkClick()
        {
            animal = dialogAnimal;
            dialogIsOpen = false;
        }

        public void DeleteActivity(MouseEventArgs e, Guid id)
        {
            OpenDialog();
        }
    }
}
