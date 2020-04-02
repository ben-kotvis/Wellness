using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public class EventParticipation : ModelBase, IParticipate
    {
        public Guid UserParticipationId { get; set; }
        public UserParticipation UserParticipation { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; }

        public Guid? EventAttachmentId { get; set; }
        public EventAttachment Attachment { get; set; }

        public DateTimeOffset Date { get; set; }
        public decimal Points { get; set; }

    }
}
