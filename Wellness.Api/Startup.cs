using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using System;
using System.Security.Claims;
using System.Threading;
using Wellness.Api.Authorization;
using Wellness.Domain;
using Wellness.Domain.ModelValidation;
using Wellness.Model;
using Wellness.Persistance.Mongo;
using System.Linq;
using Microsoft.AspNetCore.ResponseCompression;

namespace Wellness.Api
{
    public class Startup
    {
        public const string CorsPolicyName = "CorsPolicyName";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSignalR();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            services.Configure<DatabaseSettings>(Configuration.GetSection("DatabaseSettings"));
            services.AddOptions();

            services.AddMvc().AddFluentValidation();

            services.AddSingleton(MappingConfigurator.Configure());
            services.AddSingleton<IMap, Mapping>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(s =>
            {
                IHttpContextAccessor contextAccessor = s.GetService<IHttpContextAccessor>();
                ClaimsPrincipal user = contextAccessor?.HttpContext?.User;
                return user ?? throw new System.Exception("User not resolved");
            });

            services.AddTransient(typeof(IRequestDependencies<>), typeof(RequestDependencies<>)); 

            services.AddSingleton<IClientNotifier, ClientNotificationHub>();
            services.AddSingleton(typeof(IPersistanceService<>), typeof(MongoCompanyPersistanceService<>));


            //ICompanyPersistanceReaderService
            services.AddSingleton(sp => (ICompanyPersistanceReaderService<User>)sp.GetService<IPersistanceService<User>>());
            services.AddSingleton(sp => (ICompanyPersistanceReaderService<Event>)sp.GetService<IPersistanceService<Event>>());
            services.AddSingleton(sp => (ICompanyPersistanceReaderService<Activity>)sp.GetService<IPersistanceService<Activity>>());
            services.AddSingleton(sp => (ICompanyPersistanceReaderService<EventParticipation>)sp.GetService<IPersistanceService<EventParticipation>>());
            services.AddSingleton(sp => (ICompanyPersistanceReaderService<ActivityParticipation>)sp.GetService<IPersistanceService<ActivityParticipation>>());


            services.AddTransient(typeof(IValidate<>), typeof(Validation<>));
            services.AddSingleton(typeof(ICompanyDomainDependencies<>), typeof(DomainDependencies<>));
            services.AddSingleton(typeof(IDomainService<>), typeof(DomainServiceBase<>));
            services.AddSingleton<IProfileDomainService, ProfileDomainService>();
            services.AddSingleton(typeof(IParticipationDomainService<>), typeof(ParticipationDomainService<>));
            services.AddSingleton(typeof(ICompoundCompanyPersistanceService<>), typeof(CompoundPersistanceService<>));
            
            services.AddTransient<IValidator<Activity>, ActivityValidation>();
            services.AddTransient<IValidator<Event>, EventValidation>();
            services.AddTransient<IValidator<ActivityParticipation>, ActivityParticipationValidation>();
            services.AddTransient<IValidator<EventParticipation>, EventParticipationValidation>();
            services.AddTransient<IValidator<FrequentlyAskedQuestion>, FAQValidation>();
            services.AddTransient<IValidator<User>, UserValidation>();

            Console.WriteLine(Configuration.GetValue<string>("ClientSource"));

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                                  builder =>
                                  {
                                      builder
                                      .WithOrigins(Configuration.GetValue<string>("ClientSource"))
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowCredentials();
                                  });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddMicrosoftIdentityWebApi(options =>
                    {
                        Configuration.Bind("AzureAdB2C", options);

                        options.TokenValidationParameters.NameClaimType = "name";
                    },
            options => { Configuration.Bind("AzureAdB2C", options); });

            services.AddAuthorization(options =>
            {
                // Create policy to check for the scope 'read'
                options.AddPolicy("ReadScope", policy => policy.Requirements.Add(new ScopesRequirement("Auth.Standard")));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationHub>("/notificationhub");
            });
        }
    }
}
