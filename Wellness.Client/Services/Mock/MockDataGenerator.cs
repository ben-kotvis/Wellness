﻿using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.Services.Mock
{
    public static class MockDataGenerator
    {
        public static List<PersistenceWrapper<Activity>> Activities;
        public static List<PersistenceWrapper<Event>> Events;
        public static List<PersistenceWrapper<ActivityParticipation>> ActivityParticipations;
        public static List<PersistenceWrapper<EventParticipation>> EventParticipations;
        public static List<EventAttachment> EventAttachments;
        public static List<PersistenceWrapper<User>> Users;
        public static List<PersistenceWrapper<FrequentlyAskedQuestion>> FrequentlyAskedQuestions;
        public static PersistenceWrapper<User> CurrentUser;
        static MockDataGenerator()
        {
            Activities = GetActivities();
            ActivityParticipations = BootstrapUserActivities();
            Events = GetEvents();
            EventParticipations = BootstrapUserEvents();
            EventAttachments = new List<EventAttachment>();
            Users = BootstrapUsers();
            FrequentlyAskedQuestions = BootstrapFAQs();
            CurrentUser = default;
        }

        public static Guid CompanyId = Guid.Parse("6f783ddd-2c46-4ba2-8c20-5c641daa6f36");
        public static Guid CurrentUserId = Guid.Parse("6f783ddd-2c46-4ba2-8c20-5c641daa6f37");

        public static IProfileService CreateProfile()
        {
            var profileServiceMock = new Mock<IProfileService>();
            profileServiceMock.Setup(ams => ams.GetCurrent(It.IsAny<CancellationToken>()))
                .Returns((CancellationToken cancellationToken) => Task.FromResult(CurrentUser));

            profileServiceMock.Setup(ams => ams.Create(It.IsAny<User>()))
                .Returns((User u) => { CurrentUser = new PersistenceWrapper<User>() { Model = u, Common = CreateCommon() }; return Task.FromResult(true); });

            profileServiceMock.Setup(ams => ams.Update(It.IsAny<User>()))
                .Returns((User u) => { CurrentUser = new PersistenceWrapper<User>() { Model = u, Common = CreateCommon() }; return Task.FromResult(true); });

            profileServiceMock.Setup(ams => ams.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns((Guid id, CancellationToken cancellationToken) => Task.FromResult(Users.First(i => i.Model.Id == id)));

            profileServiceMock.Setup(ams => ams.Find(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns((string searchText, CancellationToken cancellationToken) => Task.FromResult(Users.Where(i => i.Model.FirstName.ToLower().Contains(searchText.ToLower()) || 
                i.Model.LastName.ToLower().Contains(searchText.ToLower()))));

            return profileServiceMock.Object;
        }

        private static List<PersistenceWrapper<FrequentlyAskedQuestion>> BootstrapFAQs()
        {
            var faqs = new List<PersistenceWrapper<FrequentlyAskedQuestion>>();

            faqs.Add(new PersistenceWrapper<FrequentlyAskedQuestion>()
            {
                Common = CreateCommon(),
                Model = new FrequentlyAskedQuestion()
                {
                    Title = "How does this work?",
                    Active = true,
                    Id = Guid.NewGuid(),
                    Answer = @"
### An h3 header ###

Now a nested list:

 1. First, get these ingredients:

      * carrots
      * celery
      * lentils

 2. Boil some water.

 3. Dump everything in the pot and follow
    this algorithm:

        find wooden spoon
        uncover pot
        stir
        cover pot
        balance wooden spoon precariously on pot handle
        wait 10 minutes
        goto first step (or shut off burner when done)

    Do not bump wooden spoon or it will fall.

Notice again how text always lines up on 4-space indents (including
that last line which continues item 3 above).

Here's a link to [a website](http://foo.bar), to a [local
doc](local-doc.html), and to a [section heading in the current
doc](#an-h2-header). Here's a footnote [^1].

[^1]: Some footnote text.
"
                }
            });

            faqs.Add(new PersistenceWrapper<FrequentlyAskedQuestion>()
            {
                Common = CreateCommon(),
                Model = new FrequentlyAskedQuestion()
                {
                    Title = "What if there are two FAQs?",
                    Active = true,
                    Id = Guid.NewGuid(),
                    Answer = @"
### An h3 header ###

Now a nested list:

 1. First, get these ingredients:

      * carrots
      * celery
      * lentils

 2. Boil some water.

 3. Dump everything in the pot and follow
    this algorithm:

        find wooden spoon
        uncover pot
        stir
        cover pot
        balance wooden spoon precariously on pot handle
        wait 10 minutes
        goto first step (or shut off burner when done)

    Do not bump wooden spoon or it will fall.

Notice again how text always lines up on 4-space indents (including
that last line which continues item 3 above).

Here's a link to [a website](http://foo.bar), to a [local
doc](local-doc.html), and to a [section heading in the current
doc](#an-h2-header). Here's a footnote [^1].

[^1]: Some footnote text.
"
                }
            });

            return faqs;
        }

        private static List<PersistenceWrapper<User>> BootstrapUsers()
        {
            var users = new List<PersistenceWrapper<User>>();

            users.Add(CreateUser(Guid.NewGuid().ToString(), "Test", "User1"));
            users.Add(CreateUser(Guid.NewGuid().ToString(), "Test", "User2"));
            users.Add(CreateUser(Guid.NewGuid().ToString(), "Test", "User3"));
            users.Add(CreateUser(Guid.NewGuid().ToString(), "Test", "User4"));

            return users;
        }

        private static List<PersistenceWrapper<ActivityParticipation>> BootstrapUserActivities()
        {
            var userActivities = new List<PersistenceWrapper<ActivityParticipation>>();

            for (var i = 0; i < 50; i++)
            {
                userActivities.Add(CreateActivity());
            }

            return userActivities;
        }

        public static PersistenceWrapper<User> CreateUser(string id, string firstName, string lastName)
        {
            var random = new Random();

            var user = new PersistenceWrapper<User>()
            {
                Model = new User()
                {
                    ProviderObjectId = id,
                    FirstName = firstName,
                    LastName = lastName,
                    AnnualTotal = random.Next(30, 150)
                },
                Common = CreateCommon()
            };

            user.Model.AveragePointsPerMonth = Math.Floor(user.Model.AnnualTotal / DateTimeOffset.UtcNow.Month);
            return user;
        }


        public static PersistenceWrapper<ActivityParticipation> CreateActivity()
        {
            var random = new Random();
            return new PersistenceWrapper<ActivityParticipation>()
            {
                Model = new ActivityParticipation()
                {
                    Id = Guid.NewGuid(),
                    Activity = GetActivity(),
                    PointsEarned = random.Next(1, 12),
                    Minutes = random.Next(15, 120),
                    SubmissionDate = DateTime.UtcNow.AddDays(random.Next(0, 150) * -1)
                },
                Common = CreateCommon()
            };
        }

        public static Activity GetActivity()
        {
            var random = new Random();
            var index = random.Next(0, 9);

            return Activities.Select(i => i.Model).ElementAt(index);
        }

        public static List<PersistenceWrapper<EventParticipation>> BootstrapUserEvents()
        {
            var userEvents = new List<PersistenceWrapper<EventParticipation>>();

            for (var i = 0; i < 22; i++)
            {
                userEvents.Add(CreateEvent());
            }

            return userEvents;
        }
        public static PersistenceWrapper<EventParticipation> CreateEvent()
        {
            var random = new Random();
            var eventObj = GetEvent();
            return new PersistenceWrapper<EventParticipation>()
            {
                Model = new EventParticipation()
                {
                    Id = Guid.NewGuid(),
                    Event = eventObj,
                    PointsEarned = eventObj.Points,
                    SubmissionDate = DateTime.UtcNow.AddDays(random.Next(0, 60) * -1),
                    UserId = CurrentUserId
                },
                Common = CreateCommon()
            };
        }

        public static Event GetEvent()
        {
            var random = new Random();
            var index = random.Next(0, 3);

            return Events.Select(i => i.Model).ElementAt(index);
        }

        public static List<PersistenceWrapper<Event>> GetEvents()
        {
            var events = new List<PersistenceWrapper<Event>>();
            events.Add(new PersistenceWrapper<Event>() { Model = new Event() { Id = Guid.NewGuid(), Points = 10, AnnualMaximum = 100, Active = true, Name = "5K Run" }, Common = CreateCommon() });
            events.Add(new PersistenceWrapper<Event>() { Model = new Event() { Id = Guid.NewGuid(), Points = 50, AnnualMaximum = 100, Active = true, Name = "Annual Health Assessment" }, Common = CreateCommon() });
            events.Add(new PersistenceWrapper<Event>() { Model = new Event() { Id = Guid.NewGuid(), Points = 25, AnnualMaximum = 100, Active = true, Name = "Dental Cleaning" }, Common = CreateCommon() });
            events.Add(new PersistenceWrapper<Event>() { Model = new Event() { Id = Guid.NewGuid(), Points = 20, AnnualMaximum = 100, Active = true, Name = "10K Run" }, Common = CreateCommon() });
            return events;
        }

        public static List<PersistenceWrapper<Activity>> GetActivities()
        {
            var activities = new List<PersistenceWrapper<Activity>>();
            activities.Add(new PersistenceWrapper<Activity>() { Model = new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Walking" }, Common = CreateCommon() });
            activities.Add(new PersistenceWrapper<Activity>() { Model = new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Running" }, Common = CreateCommon() });
            activities.Add(new PersistenceWrapper<Activity>() { Model = new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Biking" }, Common = CreateCommon() });
            activities.Add(new PersistenceWrapper<Activity>() { Model = new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Golfing" }, Common = CreateCommon() });
            activities.Add(new PersistenceWrapper<Activity>() { Model = new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Basketball" }, Common = CreateCommon() });
            activities.Add(new PersistenceWrapper<Activity>() { Model = new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Resistance Training" }, Common = CreateCommon() });
            activities.Add(new PersistenceWrapper<Activity>() { Model = new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Yoga/Pilates/Stretching" }, Common = CreateCommon() });
            activities.Add(new PersistenceWrapper<Activity>() { Model = new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Racquetball" }, Common = CreateCommon() });
            activities.Add(new PersistenceWrapper<Activity>() { Model = new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Rock Climbing" }, Common = CreateCommon() });
            activities.Add(new PersistenceWrapper<Activity>() { Model = new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Chopping/splitting wood" }, Common = CreateCommon() });
            activities.Add(new PersistenceWrapper<Activity>() { Model = new Activity() { Id = Guid.NewGuid(), Active = true, Name = "Roller Blading/Roller Skating/Ice Skating" }, Common = CreateCommon() });
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
