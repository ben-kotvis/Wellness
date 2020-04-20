using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public class EventAttachment
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }        
    }
}
