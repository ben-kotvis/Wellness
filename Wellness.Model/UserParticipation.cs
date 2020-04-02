using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public class UserParticipation : ModelBase
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public bool Approved { get; set; }

        public Guid? ApprovedByUserId { get; set; }
        public User ApprovedByUser { get; set; }

        public List<EventParticipation> Events { get; set; }
        public List<ActivityParticipation> Activities { get; set; }
    }
}
