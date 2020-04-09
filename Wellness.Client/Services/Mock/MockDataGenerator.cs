using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;
using Moq;


namespace Wellness.Client.Services.Mock
{
    public class MockDataGenerator
    {
        static List<Activity> Activities;
        static List<ActivityParticipation> ActivityParticipations;

        static MockDataGenerator()
        {
            Activities = GetActivities();
            ActivityParticipations = BootstrapUserActivities();
        }

        public static Guid CompanyId = Guid.Parse("6f783ddd-2c46-4ba2-8c20-5c641daa6f36");
        public static IActivityManagementService CreateActivityManagement()
        {
            var activityManagementMock = new Mock<IActivityManagementService>();
            activityManagementMock.Setup(ams => ams.GetAll())
            .Returns(() => {
                return Task.FromResult(new List<Activity>(Activities).AsEnumerable());
            });

            activityManagementMock.Setup(ams => ams.Create(It.IsAny<Activity>())).Returns((Activity a) =>
            {
                a.Common = CreateCommon();                               
                Activities.Add(a);                
                return Task.FromResult(true);
            });

            activityManagementMock.Setup(ams => ams.Update(It.IsAny<Activity>())).Returns((Activity a) =>
            {
                Activities.ForEach(ap =>
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
                Activities.ForEach(ap =>
                {
                    if(ap.Id == id)
                    {
                        ap.Active = false;
                    }
                });
                return Task.FromResult(true);
            });

            return activityManagementMock.Object;
        }

        public static IActivityParticipationService CreateActivityParticipation()
        {
            var activityParticipationMock = new Mock<IActivityParticipationService>();
            activityParticipationMock.Setup(ams => ams.GetByRelativeMonthIndex(It.IsAny<int>()))
                .Returns((int i) => Task.FromResult(new List<ActivityParticipation>(GetByRelativeIndex(i)).AsEnumerable()));

            activityParticipationMock.Setup(ams => ams.Create(It.IsAny<ActivityParticipation>())).Returns((ActivityParticipation ap) =>
            {
                ap.Common = CreateCommon();
                ActivityParticipations.Add(ap);
                return Task.FromResult(true);
            });

            activityParticipationMock.Setup(ams => ams.Delete(It.IsAny<Guid>())).Returns((Guid id) =>
            {
                ActivityParticipations.RemoveAll(ap => ap.Id == id);
                return Task.FromResult(true);
            });
            return activityParticipationMock.Object;
        }

        private static IEnumerable<ActivityParticipation> GetByRelativeIndex(int relativeIndex)
        {
            var relativeDate = DateTimeOffset.UtcNow.AddMonths(relativeIndex);
            return ActivityParticipations.Where(i => i.ParticipationDate.Year == relativeDate.Year && i.ParticipationDate.Month == relativeDate.Month);
        }

        private static List<ActivityParticipation> BootstrapUserActivities()
        {
            var userActivities = new List<ActivityParticipation>();

            for (var i = 0; i < 50; i++)
            {
                userActivities.Add(CreateActivity());
            }

            return userActivities;
        }

        private static ActivityParticipation CreateActivity()
        {
            var random = new Random();
            return new ActivityParticipation()
            {
                Id = Guid.NewGuid(),
                ActivityName = GetActivity().Name,
                Points = random.Next(1, 12),
                Minutes = random.Next(15, 120),
                ParticipationDate = DateTimeOffset.UtcNow.AddDays(random.Next(0, 150) * -1)
            };
        }

        private static Activity GetActivity()
        {
            var random = new Random();
            var index = random.Next(0, 7);

            return Activities.ElementAt(index);
        }
               
        private static List<Activity> GetActivities()
        {
            var activities = new List<Activity>();
            activities.Add(new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Walking", Common = CreateCommon() });
            activities.Add(new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Running", Common = CreateCommon() });
            activities.Add(new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Biking", Common = CreateCommon() });
            activities.Add(new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Golfing", Common = CreateCommon() });
            activities.Add(new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Basketball", Common = CreateCommon() });
            activities.Add(new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Resistance Training", Common = CreateCommon() });
            activities.Add(new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Hot Yoga", Common = CreateCommon() });
            activities.Add(new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Martial Arts", Common = CreateCommon() });
            return activities;
        }
        
        private static Common CreateCommon()
        {
            return new Common()
            {
                CompanyId = CompanyId,
                CreatedBy = "bkotvis",
                UpdatedBy = "bkotvis",
                CreatedOn = DateTimeOffset.UtcNow,
                UpdatedOn = DateTimeOffset.UtcNow,
            };
        }
    }
}
