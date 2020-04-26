using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;
using Wellness.Model.ModelValidation;

namespace Wellness.Client.ViewModels
{
    public interface IActivityParticipationViewModel
    {
        ActivityParticipationValidation ActivityValidation { get; set; }
        ActivityParticipation NewActivityParticipation { get; set; }
        IEnumerable<Activity> Activities { get; set; }
        Task SaveActivity();
    }
}
