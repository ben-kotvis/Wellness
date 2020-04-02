using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public class ActivityParticipation :  ModelBase, IParticipate
    {
        public Guid UserParticipationId { get; set; }
        public UserParticipation UserParticipation { get; set; }

        public Guid MinuteOptionId { get; set; }
        public MinuteOption Minutes { get; set; }

        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; }

        public DateTimeOffset Date { get; set; }
        public decimal Points { get; set; }
    }
}
