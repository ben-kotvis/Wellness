﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;
using Moq;
using System.IO;
using AutoMapper;

namespace Wellness.Client.Services.Mock
{
    public class ActivityParticipationMock : IActivityParticipationService
    {
        private IActivityParticipationService _proxy;
        private IConfigurationProvider _configurationProvider;
        private IMapper _mapper;

        public ActivityParticipationMock(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
            _mapper = _configurationProvider.CreateMapper();
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

        public IActivityParticipationService CreateActivityParticipation()
        {
            var activityParticipationMock = new Mock<IActivityParticipationService>();
            activityParticipationMock.Setup(ams => ams.GetByRelativeMonthIndex(It.IsAny<int>(), It.IsAny<Guid>()))
                .Returns((int i, Guid id) => Task.FromResult(new List<PersistenceWrapper<ActivityParticipation>>(GetByRelativeIndex(i)).AsEnumerable()));

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

        private IEnumerable<PersistenceWrapper<ActivityParticipation>> GetByRelativeIndex(int relativeIndex)
        {
            var relativeDate = DateTimeOffset.UtcNow.AddMonths(relativeIndex);
            return MockDataGenerator.ActivityParticipations.Where(i => i.Model.SubmissionDate.Year == relativeDate.Year && i.Model.SubmissionDate.Month == relativeDate.Month);
        }


    }
}
