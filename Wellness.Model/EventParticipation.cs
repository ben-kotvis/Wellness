using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public class EventParticipation : IParticipate, IIdentifiable
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public NamedEntity Event { get; set; }
        public EventAttachment Attachment { get; set; }
        public decimal PointsEarned { get; set; }
        public DateTime SubmissionDate { get; set; }
    }
}
