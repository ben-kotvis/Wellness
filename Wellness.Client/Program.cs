using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Wellness.Client.Services;
using FluentValidation;
using Wellness.Model.ModelValidation;
using System.Globalization;
using Microsoft.JSInterop;
using AutoMapper;
using Wellness.Model;
using Markdig;
using Microsoft.Extensions.Localization;
using Wellness.Client.Pages;
using Microsoft.AspNetCore.Builder;
using System.Net.Http;
using System.Linq;

namespace Wellness.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IConfigurationProvider config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EventParticipation, PersistenceWrapper<EventParticipation>>()
                    .ForMember(i => i.Model, opt => opt.MapFrom(src => src));
                cfg.CreateMap<PersistenceWrapper<EventParticipation>, EventParticipation>();
                cfg.CreateMap<ActivityParticipation, PersistenceWrapper<ActivityParticipation>>()
                    .ForMember(i => i.Model, opt => opt.MapFrom(src => src));
                cfg.CreateMap<PersistenceWrapper<ActivityParticipation>, ActivityParticipation>();


                cfg.CreateMap<FrequentlyAskedQuestion, PersistenceWrapper<FrequentlyAskedQuestion>>()
                    .ForMember(i => i.Model, opt => opt.MapFrom(src => src));
            });

            
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddSingleton<AppState>();
            builder.Services.AddSingleton<IConfigurationProvider>(config);
            builder.Services.AddSingleton<MarkdownPipeline>(new MarkdownPipelineBuilder().UseAdvancedExtensions().Build());
            builder.Services.BuildWellness(true);
            builder.Services.AddValidatorsFromAssemblyContaining<EventValidation>();
            builder.Services.AddValidatorsFromAssemblyContaining<EventParticipationValidation>();


            builder.RootComponents.Add<App>("app");
            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddLocalization();

            var host = builder.Build();
            
            var supportedCulteres = new List<CultureInfo>() { new CultureInfo("en") };
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
                options.SupportedUICultures = supportedCulteres;
            });


            await host.RunAsync();
            


            //CreateHostBuilder(args).Build().Run();
        }

        /*
        public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
        */
    }
}
