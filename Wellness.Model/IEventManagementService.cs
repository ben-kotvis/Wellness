using System;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IEventManagementService : IReaderService<Event>
    {
        Task Create(Event eventObj);
        Task Update(Event eventObj);

        Task Disable(Guid eventId);
    }
}
