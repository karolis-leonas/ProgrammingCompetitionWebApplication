using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProgrammingCompetitionWebApplication.DataContexts;
using ProgrammingCompetitionWebApplication.Services.SubmissionsService;
using System;
using System.Net;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using ProgrammingCompetitionWebApplication.Models.Configuration;

namespace ProgrammingCompetitionWebApplication
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var mainConfiguration = Configuration.GetSection("MainConfiguration");
            services.Configure<MainConfiguration>(mainConfiguration);

            var mainConfigurationSection = mainConfiguration.Get<MainConfiguration>();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = mainConfigurationSection.RootPath;
            });
            services.AddDbContext<ProgrammingCompetitionDataContext>(options => options.UseSqlServer(mainConfigurationSection.DatabaseLocation));

            var onlineCompilerUri = new Uri(mainConfigurationSection.OnlineCompilerApi);
            var httpClient = new HttpClient()
            {
                BaseAddress = onlineCompilerUri,
            };

            ServicePointManager.FindServicePoint(onlineCompilerUri).ConnectionLeaseTimeout = mainConfigurationSection.ConnectionLeaseTimeout;
            services.AddSingleton<HttpClient>(httpClient)
                .AddScoped<ISubmissionsService, SubmissionsService>()
                .AddScoped<ISubmissionsRepository, SubmissionsRepository>()
                .BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
