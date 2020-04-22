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
    public static class MockDataGenerator
    {
        public static List<Activity> Activities;
        public static List<Event> Events;
        public static List<ActivityParticipation> ActivityParticipations;
        public static List<EventParticipationDataModel> EventParticipations;
        public static List<EventAttachment> EventAttachments;
        public static List<User> Users;

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

        public static User CreateUser(Guid id, string firstName, string lastName)
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


        public static ActivityParticipation CreateActivity()
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

        public static Activity GetActivity()
        {
            var random = new Random();
            var index = random.Next(0, 9);

            return Activities.ElementAt(index);
        }

        public static List<EventParticipationDataModel> BootstrapUserEvents()
        {
            var userEvents = new List<EventParticipationDataModel>();

            for (var i = 0; i < 22; i++)
            {
                userEvents.Add(CreateEvent());
            }

            return userEvents;
        }
        public static EventParticipationDataModel CreateEvent()
        {
            var random = new Random();
            var eventObj = GetEvent();
            return new EventParticipationDataModel()
            {
                Id = Guid.NewGuid(),
                Common = CreateCommon(),
                Event = eventObj,
                PointsEarned = eventObj.Points,
                SubmissionDate = DateTime.UtcNow.AddDays(random.Next(0, 60) * -1),
                UserId = CurrentUserId
            };
        }

        public static Event GetEvent()
        {
            var random = new Random();
            var index = random.Next(0, 3);

            return Events.ElementAt(index);
        }

        public static List<Event> GetEvents()
        {
            var events = new List<Event>();
            events.Add(new Event() { Id = Guid.NewGuid(), Points = 10, AnnualMaximum = 100, Active = true, Name = "5K Run", Common = CreateCommon() });
            events.Add(new Event() { Id = Guid.NewGuid(), Points = 50, AnnualMaximum = 100, Active = true, Name = "Annual Health Assessment", Common = CreateCommon() });
            events.Add(new Event() { Id = Guid.NewGuid(), Points = 25, AnnualMaximum = 100, Active = true, Name = "Dental Cleaning", Common = CreateCommon() });
            events.Add(new Event() { Id = Guid.NewGuid(), Points = 20, AnnualMaximum = 100, Active = true, Name = "10K Run", Common = CreateCommon() });
            return events;
        }

        public static List<Activity> GetActivities()
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

        public static Common CreateCommon()
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
