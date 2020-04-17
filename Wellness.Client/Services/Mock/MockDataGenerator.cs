using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;
using Moq;
using System.IO;

namespace Wellness.Client.Services.Mock
{
    public class MockDataGenerator
    {
        static List<Activity> Activities;
        static List<Event> Events;
        static List<ActivityParticipation> ActivityParticipations;
        static List<EventParticipation> EventParticipations;
        static List<EventAttachment> EventAttachments;
        static List<User> Users;

        static MockDataGenerator()
        {
            Activities = GetActivities();
            ActivityParticipations = BootstrapUserActivities();
            Events = GetEvents();
            EventParticipations = BootstrapUserEvents();
            EventAttachments = new List<EventAttachment>();
            Users = BootstrapUsers();
        }

        public static Guid CompanyId = Guid.Parse("6f783ddd-2c46-4ba2-8c20-5c641daa6f36");
        public static Guid CurrentUserId = Guid.Parse("6f783ddd-2c46-4ba2-8c20-5c641daa6f37");
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

        public static IEventManagementService CreateEventManagement()
        {
            var eventManagementMock = new Mock<IEventManagementService>();
            eventManagementMock.Setup(ams => ams.GetAll())
            .Returns(() => {
                return Task.FromResult(new List<Event>(Events).AsEnumerable());
            });

            eventManagementMock.Setup(ams => ams.Create(It.IsAny<Event>())).Returns((Event a) =>
            {
                a.Common = CreateCommon();                               
                Events.Add(a);                
                return Task.FromResult(true);
            });

            eventManagementMock.Setup(ams => ams.Update(It.IsAny<Event>())).Returns((Event a) =>
            {
                Events.ForEach(ap =>
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
                Activities.ForEach(ap =>
                {
                    if(ap.Id == id)
                    {
                        ap.Active = false;
                    }
                });
                return Task.FromResult(true);
            });

            return eventManagementMock.Object;
        }


        public static IProfileService CreateProfile()
        {
            var profileServiceMock = new Mock<IProfileService>();
            profileServiceMock.Setup(ams => ams.GetCurrent())
                .Returns(() => Task.FromResult(Users.First(i => i.Id == CurrentUserId)));

            profileServiceMock.Setup(ams => ams.Get(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(Users.First(i => i.Id == id)));

            profileServiceMock.Setup(ams => ams.Find(It.IsAny<string>()))
                .Returns((string searchText) => Task.FromResult(Users.Where(i => i.FirstName.ToLower().Contains(searchText.ToLower()) || i.LastName.ToLower().Contains(searchText.ToLower()))));

            return profileServiceMock.Object;
        }


        public static IActivityParticipationService CreateActivityParticipation()
        {
            var activityParticipationMock = new Mock<IActivityParticipationService>();
            activityParticipationMock.Setup(ams => ams.GetByRelativeMonthIndex(It.IsAny<int>(), It.IsAny<Guid>()))
                .Returns((int i, Guid id) => Task.FromResult(new List<ActivityParticipation>(GetByRelativeIndex(i)).AsEnumerable()));

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

        public static IEventParticipationService CreateEventParticipation()
        {
            var eventParticipationMock = new Mock<IEventParticipationService>();
            eventParticipationMock.Setup(ams => ams.GetByRelativeMonthIndex(It.IsAny<int>(), It.IsAny<Guid>()))
                .Returns((int i, Guid id) => 
                {                    
                    var relativeDate = DateTimeOffset.UtcNow.AddMonths(i);
                    var filtered = EventParticipations.Where(i => i.Date.Year == relativeDate.Year && i.Date.Month == relativeDate.Month);
                    return Task.FromResult(new List<EventParticipation>(filtered).AsEnumerable());
                });

            eventParticipationMock.Setup(ams => ams.GetAttachment(It.IsAny<Guid>()))
                .Returns((Guid id) =>
                {
                    return Task.FromResult(EventAttachments.FirstOrDefault(i => i.Id == id));
                });

            eventParticipationMock.Setup(ams => ams.Create(It.IsAny<EventParticipation>())).Returns((EventParticipation ap) =>
            {
                ap.Common = CreateCommon();
                EventParticipations.Add(ap);
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

                EventAttachments.Add(new EventAttachment()
                {
                    Id = id,
                    ContentType = ct,
                    FilePath = $"file://{fileName}",
                    LocalPath = fileName,
                    Name = n
                });

                return id;
            });

            eventParticipationMock.Setup(ams => ams.Delete(It.IsAny<Guid>())).Returns((Guid id) =>
            {
                EventParticipations.RemoveAll(ap => ap.Id == id);
                return Task.FromResult(true);
            });

            return eventParticipationMock.Object;
        }
        private static IEnumerable<ActivityParticipation> GetByRelativeIndex(int relativeIndex)
        {
            var relativeDate = DateTimeOffset.UtcNow.AddMonths(relativeIndex);
            return ActivityParticipations.Where(i => i.ParticipationDate.Year == relativeDate.Year && i.ParticipationDate.Month == relativeDate.Month);
        }

        private static List<User> BootstrapUsers()
        {
            var users = new List<User>();

            users.Add(CreateUser(CurrentUserId, "Current", "User"));
            users.Add(CreateUser(Guid.NewGuid(), "Test", "User1"));
            users.Add(CreateUser(Guid.NewGuid(), "Test", "User2"));
            users.Add(CreateUser(Guid.NewGuid(), "Test", "User3"));
            users.Add(CreateUser(Guid.NewGuid(), "Test", "User4"));

            return users;
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

        private static User CreateUser(Guid id, string firstName, string lastName)
        {
            var random = new Random();
            var user =  new User()
            {
                Id = id,
                Common = CreateCommon(),
                FirstName = firstName,
                LastName = lastName,
                AnnualTotal = random.Next(30, 150)                
            };

            user.AveragePointsPerMonth = Math.Floor(user.AnnualTotal / DateTimeOffset.UtcNow.Month);
            return user;
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
            var index = random.Next(0, 9);

            return Activities.ElementAt(index);
        }

        private static List<EventParticipation> BootstrapUserEvents()
        {
            var userEvents = new List<EventParticipation>();

            for (var i = 0; i < 22; i++)
            {
                userEvents.Add(CreateEvent());
            }

            return userEvents;
        }
        private static EventParticipation CreateEvent()
        {
            var random = new Random();
            var eventObj = GetEvent();
            return new EventParticipation()
            {
                Id = Guid.NewGuid(),
                EventName = eventObj.Name,
                Points = eventObj.Points,                
                Date = DateTimeOffset.UtcNow.AddDays(random.Next(0, 60) * -1)
            };
        }
                       
        private static Event GetEvent()
        {
            var random = new Random();
            var index = random.Next(0, 3);

            return Events.ElementAt(index);
        }

        private static List<Event> GetEvents()
        {
            var events = new List<Event>();
            events.Add(new Event() { Id = Guid.NewGuid(), Points = 10, AnnualMaximum = 100, Active = true, Name = "5K Run", Common = CreateCommon() });
            events.Add(new Event() { Id = Guid.NewGuid(), Points = 50, AnnualMaximum = 100, Active = true, Name = "Annual Health Assessment", Common = CreateCommon() });
            events.Add(new Event() { Id = Guid.NewGuid(), Points = 25, AnnualMaximum = 100, Active = true, Name = "Dental Cleaning", Common = CreateCommon() });
            events.Add(new Event() { Id = Guid.NewGuid(), Points = 20, AnnualMaximum = 100, Active = true, Name = "10K Run", Common = CreateCommon() });
            return events;
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
            activities.Add(new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Yoga/Pilates/Stretching", Common = CreateCommon() });
            activities.Add(new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Racquetball", Common = CreateCommon() });
            activities.Add(new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Rock Climbing", Common = CreateCommon() });
            activities.Add(new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Chopping/splitting wood", Common = CreateCommon() });
            activities.Add(new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Roller Blading/Roller Skating/Ice Skating", Common = CreateCommon() });
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
