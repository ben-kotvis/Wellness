using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IEventManagementService
    {
        Task Create(Event activity);
        Task Update(Event activity);

        Task<IEnumerable<Event>> GetAll();

        Task Disable(Guid eventId);
    }
}
