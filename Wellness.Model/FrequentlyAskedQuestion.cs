using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public class FrequentlyAskedQuestion : IIdentifiable
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Answer { get; set; }
        public bool Active { get; set; }
        public List<EventAttachment> Images { get; set; }
    }
}
