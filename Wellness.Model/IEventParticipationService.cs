using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IEventParticipationService
    {
        Task Create(EventParticipation activityParticipation);

        Task<IEnumerable<EventParticipation>> GetByRelativeMonthIndex(int relativeMonthIndex, Guid userId);

        Task Delete(Guid id);

        Task<Guid> UploadFile(string name, string contentType, Func<Stream, Task> streamTask);

        Task<EventAttachment> GetAttachment(Guid fileId);
    }
}
