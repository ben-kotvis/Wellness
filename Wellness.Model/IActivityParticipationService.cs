using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IActivityParticipationService
    {
        Task Create(ActivityParticipation activityParticipation);

        Task<IEnumerable<ActivityParticipation>> GetByRelativeMonthIndex(int relativeMonthIndex, Guid userId);

        Task Delete(Guid id);
    }
}
