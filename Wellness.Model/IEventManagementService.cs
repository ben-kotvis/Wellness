using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IEventManagementService
    {
        Task Create(Event eventObj);
        Task Update(Event eventObj);

        Task<IEnumerable<PersistenceWrapper<Event>>> GetAll();

        Task Disable(Guid eventId);        
    }
}
