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
    public class EventParticipationMock : IEventParticipationService
    {
        private IEventParticipationService _proxy;
        private IConfigurationProvider _configurationProvider;
        private IMapper _mapper;

        public EventParticipationMock(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
            _mapper = _configurationProvider.CreateMapper();
            _proxy = CreateEventParticipation();
        }

        public Task Create(EventParticipation eventParticipation)
        {
            return _proxy.Create(eventParticipation);
        }
        public Task<IEnumerable<PersistenceWrapper<EventParticipation>>> GetByRelativeMonthIndex(int relativeMonthIndex, Guid userId)
        {
            return _proxy.GetByRelativeMonthIndex(relativeMonthIndex, userId);
        }
        public Task Delete(Guid id)
        {
            return _proxy.Delete(id);
        }
        public Task<string> UploadFile(string name, string contentType, Func<Stream, Task> streamTask)
        {
            return _proxy.UploadFile(name, contentType, streamTask);
        }

        public IEventParticipationService CreateEventParticipation()
        {
            var eventParticipationMock = new Mock<IEventParticipationService>();
            eventParticipationMock.Setup(ams => ams.GetByRelativeMonthIndex(It.IsAny<int>(), It.IsAny<Guid>()))
                .Returns((int i, Guid id) =>
                {
                    var relativeDate = DateTimeOffset.UtcNow.AddMonths(i);
                    var filtered = MockDataGenerator.EventParticipations.Where(i => i.Model.SubmissionDate.Year == relativeDate.Year && i.Model.SubmissionDate.Month == relativeDate.Month);
                    return Task.FromResult(new List<PersistenceWrapper<EventParticipation>>(filtered).AsEnumerable());
                });

            eventParticipationMock.Setup(ams => ams.Create(It.IsAny<EventParticipation>())).Returns((EventParticipation ap) =>
            {
                var model = _mapper.Map<EventParticipation, PersistenceWrapper<EventParticipation>>(ap);

                Console.WriteLine(model.Model.Id);
                Console.WriteLine(model.Model.Event.Name);
                model.Common = MockDataGenerator.CreateCommon();
                MockDataGenerator.EventParticipations.Add(model);
                return Task.FromResult(true);
            });

            eventParticipationMock.Setup(ams => ams.UploadFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Func<Stream, Task>>())).Returns(async (string n, string ct, Func<Stream, Task> f) =>
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

            eventParticipationMock.Setup(ams => ams.Delete(It.IsAny<Guid>())).Returns((Guid id) =>
            {
                MockDataGenerator.EventParticipations.RemoveAll(ap => ap.Model.Id == id);
                return Task.FromResult(true);
            });

            return eventParticipationMock.Object;
        }


    }
}
