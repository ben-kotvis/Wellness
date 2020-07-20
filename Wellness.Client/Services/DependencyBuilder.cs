using Microsoft.Extensions.DependencyInjection;
using Wellness.Client.Services.Mock;
using Wellness.Client.ViewModels;
using Wellness.Model;

namespace Wellness.Client.Services
{
    public static class DependencyBuilder
    {
        public static void BuildWellness(this IServiceCollection services, bool useMock = true)
        {
            if (useMock)
            {
                services.AddScoped<IActivityManagementService, ActivityManagementMock>();
                services.AddScoped<IActivityParticipationService, ActivityParticipationMock>();
                services.AddScoped<IEventManagementService, EventManagmentMock>();
                services.AddScoped<IEventParticipationService, EventParticipationMock>();
                services.AddScoped<IFrequentlyAskedQuestionService, FAQManagmentMock>();
                services.AddSingleton<IProfileService>(MockDataGenerator.CreateProfile());
            }
            else
            {
                services.AddScoped<IActivityManagementService, ActivityManagment>();
                services.AddScoped<IActivityParticipationService, ActivityParticipationManagement>();
                services.AddScoped<IEventManagementService, EventManagment>();
                services.AddScoped<IEventParticipationService, EventParticipationManagement>();
                services.AddScoped<IFrequentlyAskedQuestionService, FrequentlyAskedQuestionManagment>();
                services.AddSingleton<IProfileService>(MockDataGenerator.CreateProfile());
            }

            services.AddScoped<IPersistanceReaderService<Event>>(sp => sp.GetService<IEventManagementService>());
            services.AddScoped<IPersistanceReaderService<FrequentlyAskedQuestion>>(sp => sp.GetService<IFrequentlyAskedQuestionService>());
            services.AddScoped<IPersistanceReaderService<Activity>>(sp => sp.GetService<IActivityManagementService>());

            services.AddScoped<HomeViewModel>();
            services.AddScoped<ActivityManagementViewModel>();
            services.AddScoped<EventManagementViewModel>();
            services.AddScoped<MainViewModel>();
            services.AddScoped<FAQManagementViewModel>();
        }
    }
}
