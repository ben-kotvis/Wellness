using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wellness.Client.Components.UserParticipation.Activities.Add
{
    public class NewActivityComponent : ComponentBase
    {
        public string SelectedId { set; get; } = Guid.Empty.ToString();

        public string SelectedActivity { get; set; }
        public string NumberOfMinutes { get; set; }
        public DateTime SelectedDate { get; set; } = DateTime.MinValue;
    }
}
