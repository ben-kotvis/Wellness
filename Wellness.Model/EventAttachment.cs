using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public class EventAttachment : ModelBase
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }

        public string LocalPath 
        {
            get { return string.Format("/wellnessFiles/{0}", this.Id); }
            set { return; }
        }
    }
}
