using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

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

        public Task<IEnumerable<PersistenceWrapper<Event>>> GetAll(Guid companyId, CancellationToken cancellationToken)
        {
            return _proxy.GetAll(MockDataGenerator.CompanyId, cancellationToken);
        }

        public async Task<PersistenceWrapper<Event>> Get(Guid id, Guid companyId, CancellationToken cancellationToken)
        {
            return (await _proxy.GetAll(companyId, cancellationToken)).FirstOrDefault(i => i.Model.Id == id);
        }

        public Task Disable(Guid eventId)
        {
            return _proxy.Disable(eventId);
        }

        public IEventManagementService CreateEventManagement()
        {
            var eventManagementMock = new Mock<IEventManagementService>();
            eventManagementMock.Setup(ams => ams.GetAll(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns(() =>
            {
                return Task.FromResult(new List<PersistenceWrapper<Event>>(MockDataGenerator.Events).AsEnumerable());
            });

            eventManagementMock.Setup(ams => ams.Create(It.IsAny<Event>())).Returns((Event a) =>
            {
                MockDataGenerator.Events.Add(new PersistenceWrapper<Event>() { Model = a, Common = MockDataGenerator.CreateCommon() });
                return Task.FromResult(true);
            });

            eventManagementMock.Setup(ams => ams.Update(It.IsAny<Event>())).Returns((Event a) =>
            {
                MockDataGenerator.Events.ForEach(ap =>
                {
                    if (ap.Model.Id == a.Id)
                    {
                        ap.Model.Active = a.Active;
                        ap.Model.Name = a.Name;
                        ap.Model.AnnualMaximum = a.AnnualMaximum;
                        ap.Model.Points = a.Points;
                        ap.Model.RequireAttachment = a.RequireAttachment;

                    }
                });
                return Task.FromResult(true);
            });

            eventManagementMock.Setup(ams => ams.Disable(It.IsAny<Guid>())).Returns((Guid id) =>
            {
                MockDataGenerator.Events.ForEach(ap =>
                {
                    if (ap.Model.Id == id)
                    {
                        ap.Model.Active = false;
                    }
                });
                return Task.FromResult(true);
            });

            return eventManagementMock.Object;
        }

    }
}
