using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wellness.Model
{
    public interface IParticipate : IHaveCommon
    {
        Guid Id { get; set; }
        Guid UserParticipationId { get; set; }
        UserParticipation UserParticipation { get; set; }
    }
}
