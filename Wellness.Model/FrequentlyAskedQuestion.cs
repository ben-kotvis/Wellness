using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public class FrequentlyAskedQuestion :ModelBase
    {
        public string Title { get; set; }
        public string Answer { get; set; }
        public bool Active { get; set; } 
    }
}
