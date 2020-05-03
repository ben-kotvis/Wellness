using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;
using Moq;
using System.IO;
using AutoMapper;

namespace Wellness.Client.Services.Mock
{
    public class FAQManagmentMock : IFrequentlyAskedQuestionService
    {
        private IFrequentlyAskedQuestionService _proxy;
        private IConfigurationProvider _configurationProvider;
        private IMapper _mapper;

        public FAQManagmentMock(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
            _mapper = _configurationProvider.CreateMapper();
            _proxy = CreateEventManagement();
        }

        public Task Create(FrequentlyAskedQuestion frequentlyAskedQuestion)
        {
            return _proxy.Create(frequentlyAskedQuestion);
        }
        public Task Update(FrequentlyAskedQuestion frequentlyAskedQuestion)
        {
            return _proxy.Update(frequentlyAskedQuestion);
        }

        public Task<IEnumerable<PersistenceWrapper<FrequentlyAskedQuestion>>> GetAll()
        {
            return _proxy.GetAll();
        }

        public Task Delete(Guid id)
        {
            return _proxy.Delete(id);
        }
        public Task<string> UploadFile(string name, string contentType, Func<Stream, Task> streamTask)
        {
            return _proxy.UploadFile(name, contentType, streamTask);
        }

        public IFrequentlyAskedQuestionService CreateEventManagement()
        {
            var eventManagementMock = new Mock<IFrequentlyAskedQuestionService>();
            eventManagementMock.Setup(ams => ams.GetAll())
            .Returns(() => {
                return Task.FromResult(new List<PersistenceWrapper<FrequentlyAskedQuestion>>(MockDataGenerator.FrequentlyAskedQuestions).AsEnumerable());
            });

            eventManagementMock.Setup(ams => ams.Create(It.IsAny<FrequentlyAskedQuestion>())).Returns((FrequentlyAskedQuestion a) =>
            {
                var output = _mapper.Map<FrequentlyAskedQuestion, PersistenceWrapper<FrequentlyAskedQuestion>>(a);
                output.Common = MockDataGenerator.CreateCommon();
                MockDataGenerator.FrequentlyAskedQuestions.Add(output);
                return Task.FromResult(true);
            });

            eventManagementMock.Setup(ams => ams.Update(It.IsAny<FrequentlyAskedQuestion>())).Returns((FrequentlyAskedQuestion a) =>
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

            eventManagementMock.Setup(ams => ams.UploadFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Func<Stream, Task>>())).Returns(async (string n, string ct, Func<Stream, Task> f) =>
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

            eventManagementMock.Setup(ams => ams.Delete(It.IsAny<Guid>())).Returns((Guid id) =>
            {
                MockDataGenerator.FrequentlyAskedQuestions.RemoveAll(ap => ap.Model.Id == id);
                return Task.FromResult(true);
            });

            return eventManagementMock.Object;
        }

    }
}
