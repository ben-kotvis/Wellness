using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public class Activity : NamedEntity, IIdentifiable
    {        
        public bool Active { get; set; }
    }
}
