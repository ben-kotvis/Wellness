using Wellness.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wellness.Client
{
    public class AppState
    {
        public static Guid CompanyId = Guid.Parse("6f783ddd-2c46-4ba2-8c20-5c641daa6f36");
        public List<Activity> Activities { get; private set; }
        public IList<ActivityParticipation> UserActivities { get; private set; }

        public AppState()
        {
            Activities = new List<Activity>();
            Activities.Add(new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Walking", Common = CreateCommon() });
            Activities.Add(new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Running", Common = CreateCommon() });
            Activities.Add(new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Biking", Common = CreateCommon() });
            Activities.Add(new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Golfing", Common = CreateCommon() });
            Activities.Add(new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Basketball", Common = CreateCommon() });
            Activities.Add(new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Resistance Training", Common = CreateCommon() });
            Activities.Add(new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Hot Yoga", Common = CreateCommon() });
            Activities.Add(new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Martial Arts", Common = CreateCommon() });

            BootstrapUserActivities();
        }

        private void BootstrapUserActivities()
        {
            UserActivities = new List<ActivityParticipation>();

            for (var i = 0; i < 50; i++)
            {
                UserActivities.Add(CreateActivity());
            }

        }
        
        private ActivityParticipation CreateActivity()
        {
            var random = new Random();
            return new ActivityParticipation()
            {
                Id = Guid.NewGuid(),
                Activity = GetActivity(),
                Points = random.Next(1, 12),
                Minutes = new MinuteOption() { OptionValue = random.Next(15, 120) },
                Date = DateTimeOffset.UtcNow.AddDays(random.Next(0, 150) * -1)
            };
        }

        private Activity GetActivity()
        {
            var random = new Random();
            var index = random.Next(0, 7);

            return Activities[index];
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
