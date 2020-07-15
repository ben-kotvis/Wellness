using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IEventManagementService : IPersistanceReaderService<Event>
    {
        Task Create(Event eventObj);
        Task Update(Event eventObj);

        Task Disable(Guid eventId);        
    }
}
