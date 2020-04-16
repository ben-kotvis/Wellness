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
            services.AddSingleton<IActivityManagementService>(MockDataGenerator.CreateActivityManagement());
            services.AddSingleton<IActivityParticipationService>(MockDataGenerator.CreateActivityParticipation());
            services.AddSingleton<IEventManagementService>(MockDataGenerator.CreateEventManagement());
            services.AddSingleton<IEventParticipationService>(MockDataGenerator.CreateEventParticipation());
            services.AddScoped<MainViewModel>();
            services.AddScoped<ActivityManagementViewModel>();
            services.AddScoped<EventManagementViewModel>();
        }
    }
}
