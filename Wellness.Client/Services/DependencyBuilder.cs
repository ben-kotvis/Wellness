using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;
using Wellness.Client.Services.Mock;
using Wellness.Client.ViewModels;

namespace Wellness.Client.Services
{
    public static class DependencyBuilder
    {
        public static void BuildWellness(this IServiceCollection services, bool useMock = true)
        {
            if (useMock)
            {                
                services.AddTransient<IActivityManagementService, ActivityManagementMock>();
                services.AddSingleton<IActivityParticipationService, ActivityParticipationMock>();
                services.AddSingleton<IEventManagementService, EventManagmentMock>();
                services.AddSingleton<IEventParticipationService, EventParticipationMock>();
                services.AddSingleton<IFrequentlyAskedQuestionService, FAQManagmentMock>();
                services.AddSingleton<IProfileService>(MockDataGenerator.CreateProfile());                
            }
            else
            {
                services.AddSingleton<IActivityManagementService, ActivityManagment>();
                services.AddSingleton<IActivityParticipationService, ActivityParticipationManagement>();
                services.AddSingleton<IEventManagementService, EventManagment>();
                services.AddSingleton<IEventParticipationService, EventParticipationManagement>();
                services.AddSingleton<IFrequentlyAskedQuestionService, FAQManagmentMock>();
                services.AddSingleton<IProfileService>(MockDataGenerator.CreateProfile());
            }

            services.AddSingleton<IDomainServiceReader<Event>>(sp => sp.GetService<IEventManagementService>());
            services.AddScoped<HomeViewModel>();
            services.AddScoped<ActivityManagementViewModel>();
            services.AddScoped<EventManagementViewModel>();
            services.AddScoped<MainViewModel>();
            services.AddScoped<FAQManagementViewModel>();
        }
    }
}
