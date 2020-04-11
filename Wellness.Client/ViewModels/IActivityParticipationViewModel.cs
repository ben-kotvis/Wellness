using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.ViewModels
{
    public interface IActivityParticipationViewModel
    {
        Activity SelectedActivity { get; set; }
        int NumberOfMinutes { get; set; }
        DateTime SelectedActivityDate { get; set; } 
    }
}
