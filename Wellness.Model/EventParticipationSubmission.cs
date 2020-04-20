using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{ 
    public class EventParticipationSubmission 
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public Guid AttachmentId { get; set; }
        public DateTimeOffset Date { get; set; }
        public decimal Points { get; set; }
    }
}
