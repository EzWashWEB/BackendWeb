using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzWash.API.Domain.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace EzWash.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host=CreateHostBuilder(args).Build();
            
            // pre run sentences

            using (var scope=host.Services.CreateScope())
            using (var context=scope.ServiceProvider.GetService<AppDbContext>())
            {
                //Ensure Database is created including seed data
                context.Database.EnsureCreated();
            }
            
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
