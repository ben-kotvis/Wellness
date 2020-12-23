using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.Services.Mock
{
    public class ActivityManagementMock : IActivityManagementService
    {
        private IActivityManagementService _proxy;
        private IMapper _mapper;
        public ActivityManagementMock(IMapper mapper)
        {
            _mapper = mapper;
            _proxy = CreateActivityManagement();
        }

        public ICompanyModelQueryable<PersistenceWrapper<Activity>> Query(Guid companyId) => throw new NotImplementedException();

        public Task Create(Activity activity)
        {
            return _proxy.Create(activity);
        }

        public IActivityManagementService CreateActivityManagement()
        {
            var activityManagementMock = new Mock<IActivityManagementService>();
            activityManagementMock.Setup(ams => ams.GetAll(Guid.Empty, It.IsAny<CancellationToken>()))
            .Returns(() =>
            {
                return Task.FromResult(new List<PersistenceWrapper<Activity>>(MockDataGenerator.Activities).AsEnumerable());
            });

            activityManagementMock.Setup(ams => ams.Create(It.IsAny<Activity>())).Returns((Activity a) =>
            {
                var output = _mapper.Map<Activity, PersistenceWrapper<Activity>>(a);
                output.Common = MockDataGenerator.CreateCommon();

                MockDataGenerator.Activities.Add(output);
                return Task.FromResult(true);
            });

            activityManagementMock.Setup(ams => ams.Update(It.IsAny<Activity>())).Returns((Activity a) =>
            {
                MockDataGenerator.Activities.ForEach(ap =>
                {
                    if (ap.Model.Id == a.Id)
                    {
                        ap.Model.Active = a.Active;
                        ap.Model.Name = a.Name;
                    }
                });
                return Task.FromResult(true);
            });

            activityManagementMock.Setup(ams => ams.Disable(It.IsAny<Guid>())).Returns((Guid id) =>
            {
                MockDataGenerator.Activities.ForEach(ap =>
                {
                    if (ap.Model.Id == id)
                    {
                        ap.Model.Active = false;
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

        public async Task<PersistenceWrapper<Activity>> Get(Guid companyId, Guid id, CancellationToken cancellationToken)
        {
            return (await GetAll(companyId, cancellationToken)).FirstOrDefault(i => i.Model.Id == id);
        }

        public Task<IEnumerable<PersistenceWrapper<Activity>>> GetAll(Guid companyId, CancellationToken cancellationToken)
        {
            return _proxy.GetAll(Guid.Empty, cancellationToken);
        }

        public Task Update(Activity activity)
        {
            return _proxy.Update(activity);
        }
    }
}
