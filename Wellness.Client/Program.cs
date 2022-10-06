using FluentValidation;
using Markdig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Security.Claims;
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

            bool useMockData = true;
            
            bool.TryParse(builder.Configuration["UseMockData"], out useMockData);
            Console.WriteLine(useMockData);
            builder.Services.AddSingleton(MappingConfigurator.Configure());
            builder.Services.AddSingleton<MarkdownPipeline>(new MarkdownPipelineBuilder().UseAdvancedExtensions().Build());
            builder.Services.BuildWellness(useMockData);
            builder.Services.AddScoped<IValidator<Model.Event>, EventValidation>();
            builder.Services.AddScoped<IValidator<EventParticipation>, EventParticipationValidation>();
            builder.Services.AddScoped<IValidator<Model.Activity>, AsyncActivityValidation>();
            builder.Services.AddScoped<IValidator<ActivityParticipation>, ActivityParticipationValidation>();
            builder.Services.AddScoped<IValidator<User>, UserValidation>();
            builder.Services.AddScoped<IValidator<Activity>, ActivityValidation>();
            //builder.Services.AddScoped<ModelValidators<Activity>>(implementationFactory => new ModelValidators<Activity>(new ActivityValidation(), implementationFactory.GetService<IValidator<Model.Activity>>()));
            builder.Services.AddSingleton<IClientState, ClientState>();

            builder.Services.AddMsalAuthentication(options =>
            {
                
                options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/User.Read");
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
            });

            builder.Services.AddTransient<ClaimsPrincipal>(sp =>
            {
                var provider = sp.GetService<AuthenticationStateProvider>();
                var state = provider.GetAuthenticationStateAsync().GetAwaiter().GetResult();
                return state.User;
            });
            
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped<CustomAuthorizationMessageHandler>();
            builder.Services.AddHttpClient("Default",
                    client =>
                    {
                        client.BaseAddress = new Uri(builder.Configuration["ServerAddress"]);
                    })
                .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();
            
            builder.Services.AddScoped<HubConnection>((sp) => new HubConnectionBuilder()
                .WithUrl($"{builder.Configuration["ServerAddress"]}/notificationhub").Build());

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Default"));

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
                authorizedUrls: new[] { configuration.GetValue<string>("ServerAddress") });
        }

    }
}
