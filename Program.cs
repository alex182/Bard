using Bard.Destinations.Concretes;
using Bard.Destinations.Interfaces;
using Bard.Destinations.Models;
using Bard.Jobs.Concretes;
using Bard.Sources.Concretes;
using Bard.Sources.Interfaces;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using System;

namespace Bard 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateWHostBuilder(args)
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
               .UseStartup<Bard.Startup.Startup>();
    }
}