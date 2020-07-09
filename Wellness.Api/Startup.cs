using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wellness.Domain;
using Wellness.Model;
using Wellness.Model.ModelValidation;
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
            services.AddSingleton(typeof(IPersistanceService<>), typeof(MongoPersistanceService<>));
            services.AddTransient<IValidator<Activity>, ActivityValidation>();
            services.AddTransient<IValidator<Event>, EventValidation>();
            services.AddScoped(typeof(IDomainDependencies<>), typeof(DomainDependencies<>));
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
