using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IEventParticipationService
    {
        Task Create(EventParticipation eventParticipation);

        Task<IEnumerable<EventParticipation>> GetByRelativeMonthIndex(int relativeMonthIndex, Guid userId);

        Task Delete(Guid id);

        Task<string> UploadFile(string name, string contentType, Func<Stream, Task> streamTask);

    }
}
