using AdvertTrackerAPI.DbContexts;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertTrackerAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(corsOption => corsOption.AddPolicy("policy", 
                cors => cors.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
           
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AdvertTrackerAPI", Version = "v1" });
            });


                 services.AddDbContext<ApplicationDbContext>(options =>
                 {
                     options.UseSqlServer(Configuration.GetConnectionString("AdvertTrackerConnection"),
                         sqlOptions => sqlOptions.EnableRetryOnFailure(50));
                 });

            services.AddHangfire(configuration => configuration
      .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
      .UseSimpleAssemblyNameTypeSerializer()
      .UseRecommendedSerializerSettings()
      .UseSqlServerStorage(Configuration.GetConnectionString("AdvertTrackerHangfireConnection"), new SqlServerStorageOptions
      {
          CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
          SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
          QueuePollInterval = TimeSpan.Zero,
          UseRecommendedIsolationLevel = true,
          DisableGlobalLocks = true
      }));

            services.AddHangfireServer();

            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdvertTrackerAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("policy");

            app.UseHangfireDashboard();
            // backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));
            // backgroundJobs.Enqueue<IProduct>(x => x.HangFireTask());

            app.UseAuthorization();

           


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
