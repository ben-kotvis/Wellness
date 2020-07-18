using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.Services.Mock
{
    public class ActivityParticipationMock : IActivityParticipationService
    {
        private IActivityParticipationService _proxy;
        private IMapper _mapper;
        public ActivityParticipationMock(IMapper mapper)
        {
            _mapper = mapper;
            _proxy = CreateActivityParticipation();
        }

        public Task Create(ActivityParticipation activityParticipation)
        {
            return _proxy.Create(activityParticipation);
        }

        public Task Delete(Guid id)
        {
            return _proxy.Delete(id);
        }

        public Task<IEnumerable<PersistenceWrapper<ActivityParticipation>>> GetByRelativeMonthIndex(int relativeMonthIndex, Guid userId)
        {
            return _proxy.GetByRelativeMonthIndex(relativeMonthIndex, userId);
        }

        public async Task<IEnumerable<PersistenceWrapper<ActivityParticipation>>> GetByRelativeIndex(int relativeIndex)
        {
            var relativeDate = DateTimeOffset.UtcNow.AddMonths(relativeIndex);
            return MockDataGenerator.ActivityParticipations.Where(i => i.Model.SubmissionDate.Year == relativeDate.Year && i.Model.SubmissionDate.Month == relativeDate.Month);
        }

        public IActivityParticipationService CreateActivityParticipation()
        {
            var activityParticipationMock = new Mock<IActivityParticipationService>();
            activityParticipationMock.Setup(ams => ams.GetByRelativeMonthIndex(It.IsAny<int>(), It.IsAny<Guid>()))
                .Returns(async (int i, Guid id) => new List<PersistenceWrapper<ActivityParticipation>>(await GetByRelativeIndex(i)).AsEnumerable());

            activityParticipationMock.Setup(ams => ams.Create(It.IsAny<ActivityParticipation>())).Returns((ActivityParticipation ap) =>
            {
                var model = _mapper.Map<ActivityParticipation, PersistenceWrapper<ActivityParticipation>>(ap);
                model.Common = MockDataGenerator.CreateCommon();
                MockDataGenerator.ActivityParticipations.Add(model);
                return Task.FromResult(true);
            });

            activityParticipationMock.Setup(ams => ams.Delete(It.IsAny<Guid>())).Returns((Guid id) =>
            {
                MockDataGenerator.ActivityParticipations.RemoveAll(ap => ap.Model.Id == id);
                return Task.FromResult(true);
            });
            return activityParticipationMock.Object;
        }

    }
}
