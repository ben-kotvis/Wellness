using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public class ActivityParticipation :  ModelBase
    {
        public Guid UserId { get; set; }

        public int Minutes { get; set; }

        public string ActivityName { get; set; }

        public DateTimeOffset ParticipationDate { get; set; }
        public decimal Points { get; set; }
    }
}
