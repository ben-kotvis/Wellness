using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.Services.Mock
{
    public class FAQManagmentMock : IFrequentlyAskedQuestionService
    {
        private IFrequentlyAskedQuestionService _proxy;
        private IMapper _mapper;

        public ICompanyModelQueryable<PersistenceWrapper<FrequentlyAskedQuestion>> Query => throw new NotImplementedException();

        public FAQManagmentMock(IMapper mapper)
        {
            _mapper = mapper;
            _proxy = CreateEventManagement();
        }

        public Task Create(FrequentlyAskedQuestion frequentlyAskedQuestion, CancellationToken cancellationToken)
        {
            return _proxy.Create(frequentlyAskedQuestion, cancellationToken);
        }
        public Task Update(FrequentlyAskedQuestion frequentlyAskedQuestion, CancellationToken cancellationToken)
        {
            return _proxy.Update(frequentlyAskedQuestion, cancellationToken);
        }

        public async Task<PersistenceWrapper<FrequentlyAskedQuestion>> Get(Guid id, CancellationToken cancellationToken)
        {
            var all = await _proxy.GetAll(cancellationToken);
            return all.FirstOrDefault(i => i.Model.Id == id);
        }

        public Task<IEnumerable<PersistenceWrapper<FrequentlyAskedQuestion>>> GetAll(CancellationToken cancellationToken)
        {
            return _proxy.GetAll(cancellationToken);
        }

        public Task Delete(Guid id, CancellationToken cancellationToken)
        {
            return _proxy.Delete(id, cancellationToken);
        }
        public Task<string> UploadFile(string name, string contentType, Func<Stream, Task> streamTask, CancellationToken cancellationToken)
        {
            return _proxy.UploadFile(name, contentType, streamTask, cancellationToken);
        }

        public IFrequentlyAskedQuestionService CreateEventManagement()
        {
            var eventManagementMock = new Mock<IFrequentlyAskedQuestionService>();
            eventManagementMock.Setup(ams => ams.GetAll(It.IsAny<CancellationToken>()))
            .Returns((CancellationToken token) =>
            {
                return Task.FromResult(new List<PersistenceWrapper<FrequentlyAskedQuestion>>(MockDataGenerator.FrequentlyAskedQuestions).AsEnumerable());
            });

            eventManagementMock.Setup(ams => ams.Create(It.IsAny<FrequentlyAskedQuestion>(), It.IsAny<CancellationToken>()))
                .Returns((FrequentlyAskedQuestion a, CancellationToken token) =>
            {
                var output = _mapper.Map<FrequentlyAskedQuestion, PersistenceWrapper<FrequentlyAskedQuestion>>(a);
                output.Common = MockDataGenerator.CreateCommon();
                MockDataGenerator.FrequentlyAskedQuestions.Add(output);
                return Task.FromResult(true);
            });

            eventManagementMock.Setup(ams => ams.Update(It.IsAny<FrequentlyAskedQuestion>(), It.IsAny<CancellationToken>()))
                .Returns((FrequentlyAskedQuestion a, CancellationToken token) =>
            {
                MockDataGenerator.FrequentlyAskedQuestions.ForEach(ap =>
                {
                    if (ap.Model.Id == a.Id)
                    {
                        _mapper.Map<FrequentlyAskedQuestion, PersistenceWrapper<FrequentlyAskedQuestion>>(a, ap);
                        ap.Common.UpdatedOn = DateTimeOffset.UtcNow;
                    }
                });
                return Task.FromResult(true);
            });

            eventManagementMock.Setup(ams => ams.UploadFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Func<Stream, Task>>(), It.IsAny<CancellationToken>()))
                .Returns(async (string n, string ct, Func<Stream, Task> f, CancellationToken token) =>
            {
                Guid id = Guid.NewGuid();
                var fileName = Path.GetTempFileName();
                Console.WriteLine(fileName);

                using (var file = File.Create(fileName))
                {
                    await f(file);
                }
                return fileName;
            });

            eventManagementMock.Setup(ams => ams.Delete(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Returns((Guid id, CancellationToken token) =>
            {
                MockDataGenerator.FrequentlyAskedQuestions.RemoveAll(ap => ap.Model.Id == id);
                return Task.FromResult(true);
            });

            return eventManagementMock.Object;
        }

    }
}
