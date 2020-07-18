using System;
using System.Collections.Generic;

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

        public IEnumerable<EventParticipation> Events { get; set; }
        public IEnumerable<ActivityParticipation> Activities { get; set; }
    }
}
