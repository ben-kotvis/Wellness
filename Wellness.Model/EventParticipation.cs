using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public class EventParticipation : ModelBase
    {
        public Guid UserId { get; set; }
        public string EventName { get; set; }
        public EventAttachment Attachment { get; set; }
        public DateTimeOffset Date { get; set; }
        public decimal Points { get; set; }
    }
}
