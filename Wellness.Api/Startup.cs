using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wellness.Domain;
using Wellness.Domain.ModelValidation;
using Wellness.Model;
using Wellness.Persistance.Mongo;

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

            services.AddSingleton(typeof(IPersistanceService<>), typeof(MongoPersistanceService<>));
            services.AddSingleton<IPersistanceReaderService<Event>>(sp => sp.GetService<IPersistanceService<Event>>());
            services.AddSingleton<IPersistanceReaderService<Activity>>(sp => sp.GetService<IPersistanceService<Activity>>());
            services.AddScoped(typeof(IValidate<>), typeof(Validation<>));
            services.AddScoped(typeof(IDomainDependencies<>), typeof(DomainDependencies<>));
            services.AddScoped(typeof(IDomainService<>), typeof(DomainServiceBase<>));
            services.AddScoped(typeof(IParticipationDomainService<>), typeof(ParticipationDomainService<>));
            
            services.AddTransient<IValidator<Activity>, ActivityValidation>();
            services.AddTransient<IValidator<Event>, EventValidation>();
            services.AddTransient<IValidator<ActivityParticipation>, ActivityParticipationValidation>();
            services.AddTransient<IValidator<EventParticipation>, EventParticipationValidation>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: CorsPolicyName,
                                  builder =>
                                  {
                                      builder
                                      .WithOrigins("https://localhost:44353")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();                                      
                                  });
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

            app.UseAuthorization();
            app.UseCors(CorsPolicyName);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();                
            });
        }
    }
}