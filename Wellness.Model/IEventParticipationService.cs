using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IEventParticipationService
    {
        Task Create(EventParticipation activityParticipation);

        Task<IEnumerable<EventParticipation>> GetByRelativeMonthIndex(int relativeMonthIndex);

        Task Delete(Guid id);
    }
}
