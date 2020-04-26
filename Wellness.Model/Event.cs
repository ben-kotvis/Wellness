using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public class Event : NamedEntity, IHaveCommon, IIdentifiable
    {
        public decimal Points { get; set; }
        public decimal AnnualMaximum { get; set; }
        public bool Active { get; set; }
        public bool RequireAttachment { get; set; }
        public Common Common { get; set; }
    }
}
