using System;

namespace Wellness.Model
{
    public class ActivityParticipation : IParticipate, IIdentifiable
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public int Minutes { get; set; }

        public NamedEntity Activity { get; set; }
        public decimal PointsEarned { get; set; }
        public DateTime SubmissionDate { get; set; }
    }

}
