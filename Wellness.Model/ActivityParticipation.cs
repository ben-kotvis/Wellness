using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public class ActivityParticipation : IIdentifiable
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public int Minutes { get; set; }

        public NamedEntity Activity { get; set; }
        public decimal PointsEarned { get; set; }
        public DateTime SubmissionDate { get; set; }
    }

}
