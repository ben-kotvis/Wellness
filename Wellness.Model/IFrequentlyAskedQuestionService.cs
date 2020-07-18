using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IFrequentlyAskedQuestionService
    {
        Task Create(FrequentlyAskedQuestion frequentlyAskedQuestion);

        Task<IEnumerable<PersistenceWrapper<FrequentlyAskedQuestion>>> GetAll();

        Task Delete(Guid id);

        Task Update(FrequentlyAskedQuestion frequentlyAskedQuestion);

        Task<string> UploadFile(string name, string contentType, Func<Stream, Task> streamTask);
    }
}
