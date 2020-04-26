using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public class EventParticipation : IIdentifiable
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public NamedEntity Event { get; set; }
        public EventAttachment Attachment { get; set; }
        public decimal PointsEarned { get; set; }
        public DateTime SubmissionDate { get; set; }
    }

    public class PersistenceWrapper<T> : IHaveCommon
        where T : IIdentifiable 
    {
        public T Model { get; set; }
        public Common Common { get; set; }
    }

}
