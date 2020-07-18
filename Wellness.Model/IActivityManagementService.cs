using System;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IActivityManagementService : IPersistanceReaderService<Activity>
    {
        Task Create(Activity activity);
        Task Update(Activity activity);

        Task Disable(Guid activityId);
    }
}
