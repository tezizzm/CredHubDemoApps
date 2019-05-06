using System;
using System.Linq;
using CredHubDemoUI.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using Steeltoe.Extensions.Logging;

namespace CredHubDemoUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();
            using (var scope = webHost.Services.CreateScope())
            {
                try
                {
                    var db = scope.ServiceProvider.GetService<CityContext>();
                    db.Database.EnsureCreated();

                    if (!db.Cities.Any())
                    {
                        db.Entry(new City { Id = 1, CityName = "Chicago, IL" }).State = EntityState.Added;
                        db.Entry(new City { Id = 2, CityName = "New York, NY" }).State = EntityState.Added;
                        db.Entry(new City { Id = 3, CityName = "Los Angeles, CA" }).State = EntityState.Added;
                        db.Entry(new City { Id = 4, CityName = "San Francisco" }).State = EntityState.Added;
                        db.Entry(new City { Id = 5, CityName = "Seattle, WA" }).State = EntityState.Added;
                        db.Entry(new City { Id = 6, CityName = "Las Vegas, NV" }).State = EntityState.Added;
                        db.Entry(new City { Id = 7, CityName = "Denver, CO" }).State = EntityState.Added;
                        db.Entry(new City { Id = 8, CityName = "Dallas, TX" }).State = EntityState.Added;

                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseCloudFoundryHosting(5555)
                .AddCloudFoundry()
                .ConfigureLogging(
                    (builderContext, loggingBuilder) =>
                    {
                        loggingBuilder.ClearProviders();
                        loggingBuilder.AddConfiguration(builderContext.Configuration.GetSection("Logging"));
                        loggingBuilder.AddDynamicConsole();
                    })
                .UseStartup<Startup>();
    }
}
