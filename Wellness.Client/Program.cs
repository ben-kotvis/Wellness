using FluentValidation;
using Markdig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Client.Services;
using Wellness.Domain;
using Wellness.Domain.ModelValidation;
using Wellness.Model;

namespace Wellness.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddSingleton(MappingConfigurator.Configure());
            builder.Services.AddSingleton<MarkdownPipeline>(new MarkdownPipelineBuilder().UseAdvancedExtensions().Build());
            builder.Services.BuildWellness(false);
            builder.Services.AddTransient<IValidator<Model.Event>, EventValidation>();
            builder.Services.AddTransient<IValidator<EventParticipation>, EventParticipationValidation>();
            builder.Services.AddTransient<IValidator<Model.Activity>, ActivityValidation>();
            builder.Services.AddTransient<IValidator<ActivityParticipation>, ActivityParticipationValidation>();

            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped<CustomAuthorizationMessageHandler>();
            builder.Services.AddHttpClient("Default",
                    client =>
                    {
                        client.BaseAddress = new Uri(builder.Configuration["ServerAddress"]);
                    })
                .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();
            
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Default"));
            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
                options.ProviderOptions.AdditionalScopesToConsent.Add("https://corporatewellnessmanager.onmicrosoft.com/api/Auth.Standard");
            });

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
    public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public CustomAuthorizationMessageHandler(IAccessTokenProvider provider,
            NavigationManager navigationManager, IConfiguration configuration)
            : base(provider, navigationManager)
        {
            ConfigureHandler(
                authorizedUrls: new[] { configuration.GetValue<string>("ServerAddress") },
                scopes: new[] { "https://corporatewellnessmanager.onmicrosoft.com/api/Auth.Standard", "openid", "profile" });
        }

    }
}
