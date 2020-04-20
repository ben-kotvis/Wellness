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
    public class ActivityManagementMock : IActivityManagementService
    {
        private IActivityManagementService _proxy;
        public ActivityManagementMock()
        {
            _proxy = CreateActivityManagement();
        }

        public Task Create(Activity activity)
        {
            return _proxy.Create(activity);
        }

        public IActivityManagementService CreateActivityManagement()
        {
            var activityManagementMock = new Mock<IActivityManagementService>();
            activityManagementMock.Setup(ams => ams.GetAll())
            .Returns(() =>
            {
                return Task.FromResult(new List<Activity>(MockDataGenerator.Activities).AsEnumerable());
            });

            activityManagementMock.Setup(ams => ams.Create(It.IsAny<Activity>())).Returns((Activity a) =>
            {
                a.Common = MockDataGenerator.CreateCommon();
                MockDataGenerator.Activities.Add(a);
                return Task.FromResult(true);
            });

            activityManagementMock.Setup(ams => ams.Update(It.IsAny<Activity>())).Returns((Activity a) =>
            {
                MockDataGenerator.Activities.ForEach(ap =>
                {
                    if (ap.Id == a.Id)
                    {
                        ap.Active = a.Active;
                        ap.Name = a.Name;
                    }
                });
                return Task.FromResult(true);
            });

            activityManagementMock.Setup(ams => ams.Disable(It.IsAny<Guid>())).Returns((Guid id) =>
            {
                MockDataGenerator.Activities.ForEach(ap =>
                {
                    if (ap.Id == id)
                    {
                        ap.Active = false;
                    }
                });
                return Task.FromResult(true);
            });

            return activityManagementMock.Object;
        }

        public Task Disable(Guid activityId)
        {
            return _proxy.Disable(activityId);
        }

        public Task<IEnumerable<Activity>> GetAll()
        {
            return _proxy.GetAll();
        }

        public Task Update(Activity activity)
        {
            return _proxy.Update(activity);
        }
    }
}
