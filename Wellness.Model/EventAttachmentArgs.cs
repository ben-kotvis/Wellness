using System;
using System.IO;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public class EventAttachmentArgs
    {
        public DateTime LastModified { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public string Type { get; set; }
        public Func<Stream, Task> WriteToStreamAsync { get; set; }
    }
}
