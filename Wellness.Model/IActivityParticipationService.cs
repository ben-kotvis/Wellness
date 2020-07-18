using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IActivityParticipationService
    {
        Task Create(ActivityParticipation activityParticipation);

        Task<IEnumerable<PersistenceWrapper<ActivityParticipation>>> GetByRelativeMonthIndex(int relativeMonthIndex, Guid userId);

        Task Delete(Guid id);
    }
}
