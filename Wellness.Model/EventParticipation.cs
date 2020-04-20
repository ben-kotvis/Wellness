using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public class EventParticipation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public EventBase Event { get; set; }
        public EventAttachment Attachment { get; set; }
        public decimal PointsEarned { get; set; }
        public DateTimeOffset SubmissionDate { get; set; }
    }

    public class EventParticipationDataModel : EventParticipation, IHaveCommon, IIdentifiable 
    {
        public Common Common { get; set; }
    }

    public class EventBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
