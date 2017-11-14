using System;
using System.Net;
using LightInject;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Partify.Storage.WebApi.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(options => options.Listen(IPAddress.Loopback, 5000))
                .UseStartup<Startup>()
                .ConfigureServices(sc => sc.AddSingleton<Action<IServiceContainer>>(container => { }))
                .Build();
    }
}
