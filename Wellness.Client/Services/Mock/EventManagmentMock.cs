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
    public class EventManagmentMock : IEventManagementService
    {
        private IEventManagementService _proxy;
        public EventManagmentMock()
        {
            _proxy = CreateEventManagement();
        }

        public Task Create(Event eventObj)
        {
            return _proxy.Create(eventObj);
        }
        public Task Update(Event eventObj)
        {
            return _proxy.Update(eventObj);
        }

        public Task<IEnumerable<Event>> GetAll()
        {
            return _proxy.GetAll();
        }

        public Task Disable(Guid eventId)
        {
            return _proxy.Disable(eventId);
        }

        public IEventManagementService CreateEventManagement()
        {
            var eventManagementMock = new Mock<IEventManagementService>();
            eventManagementMock.Setup(ams => ams.GetAll())
            .Returns(() => {
                return Task.FromResult(new List<Event>(MockDataGenerator.Events).AsEnumerable());
            });

            eventManagementMock.Setup(ams => ams.Create(It.IsAny<Event>())).Returns((Event a) =>
            {
                a.Common = MockDataGenerator.CreateCommon();
                MockDataGenerator.Events.Add(a);
                return Task.FromResult(true);
            });

            eventManagementMock.Setup(ams => ams.Update(It.IsAny<Event>())).Returns((Event a) =>
            {
                MockDataGenerator.Events.ForEach(ap =>
                {
                    if (ap.Id == a.Id)
                    {
                        ap.Active = a.Active;
                        ap.Name = a.Name;
                        ap.AnnualMaximum = a.AnnualMaximum;
                        ap.Points = a.Points;
                    }
                });
                return Task.FromResult(true);
            });

            eventManagementMock.Setup(ams => ams.Disable(It.IsAny<Guid>())).Returns((Guid id) =>
            {
                MockDataGenerator.Events.ForEach(ap =>
                {
                    if (ap.Id == id)
                    {
                        ap.Active = false;
                    }
                });
                return Task.FromResult(true);
            });

            return eventManagementMock.Object;
        }

    }
}
