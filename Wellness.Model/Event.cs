using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public class Event : ModelBase
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public int AnnualMaximum { get; set; }
        public bool Active { get; set; }
        public bool RequireAttachment { get; set; }
    }
}
