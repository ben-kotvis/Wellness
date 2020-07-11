using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wellness.Model
{
    public interface IParticipate 
    {
        Guid UserId { get; set; }
        DateTime SubmissionDate { get; set; }
    }
}
