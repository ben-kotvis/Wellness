using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wellness.Client.ViewModels
{
    public interface IActivityParticipationViewModel
    {
        string SelectedActivityName { get; set; }
        int NumberOfMinutes { get; set; }
        DateTime SelectedActivityDate { get; set; } 
    }
}
