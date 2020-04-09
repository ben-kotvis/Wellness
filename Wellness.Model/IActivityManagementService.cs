using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IActivityManagementService
    {
        Task Create(Activity activity);
        Task Update(Activity activity);

        Task<IEnumerable<Activity>> GetAll();

        Task Disable(Guid activityId);
    }
}
