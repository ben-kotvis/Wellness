﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IFrequentlyAskedQuestionService : IPersistanceReaderService<FrequentlyAskedQuestion>
    {
        Task Create(FrequentlyAskedQuestion frequentlyAskedQuestion, CancellationToken cancellationToken);

        Task<IEnumerable<PersistenceWrapper<FrequentlyAskedQuestion>>> GetAll(CancellationToken cancellationToken);

        Task Delete(Guid id, CancellationToken cancellationToken);

        Task Update(FrequentlyAskedQuestion frequentlyAskedQuestion, CancellationToken cancellationToken);

        Task<string> UploadFile(string name, string contentType, Func<Stream, Task> streamTask, CancellationToken cancellationToken);
    }
}
