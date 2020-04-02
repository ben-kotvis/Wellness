using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public class User : ModelBase
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProviderId { get; set; }
        public decimal AnnualTotal { get; set; }
        public decimal AveragePointsPerMonth { get; set; }

        public List<UserParticipation> Participations { get; set; }
        
    }
}
