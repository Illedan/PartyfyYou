using LightInject;
using LightInject.Microsoft.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Partify.Storage.Server;
using System;
using Microsoft.Extensions.Configuration;
using Partify.Storage.Server.Configuration;

namespace Partify.Storage.WebApi
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver
                    = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            });

            AddSwaggerConfiguration(services);
            var container = new ServiceContainer(new ContainerOptions()
            {
                EnablePropertyInjection = false
            });
            container.Register(factory => CreateConfiguration(), new PerContainerLifetime());
            container.ScopeManagerProvider = new PerLogicalCallContextScopeManagerProvider();
            container.RegisterFrom<CompositionRoot>();
            return container.CreateServiceProvider(services);
        }

        private Server.Configuration.IConfiguration CreateConfiguration()
        {
            return new PartifyConfiguration(Configuration.GetSection("DBConnectionString").Value);  
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            app.UseMvc();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("MVC didnt find anything");
            });
        }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("Configuration.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
        private static void AddSwaggerConfiguration(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Partify Storage API", Version = "v1" });
            });
        }
    }
}
