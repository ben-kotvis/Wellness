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
using System.Diagnostics;

namespace Wellness.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddSingleton(MappingConfigurator.Configure());
            builder.Services.AddSingleton<MarkdownPipeline>(new MarkdownPipelineBuilder().UseAdvancedExtensions().Build());
            builder.Services.BuildWellness(true);
            builder.Services.AddTransient<IValidator<Model.Event>,EventValidation>();
            builder.Services.AddTransient<IValidator<EventParticipation>,EventParticipationValidation>();
            builder.Services.AddTransient<IValidator<Model.Activity>, ActivityValidation>();
            builder.Services.AddTransient<IValidator<ActivityParticipation>,ActivityParticipationValidation>();


            builder.RootComponents.Add<App>("app");
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44354") });


            builder.Services.AddLocalization();

            var host = builder.Build();
            
            var supportedCulteres = new List<CultureInfo>() { new CultureInfo("en") };
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
                options.SupportedUICultures = supportedCulteres;
            });

            await host.RunAsync();            
        }
    }
}
