using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LightInject;
using LightInject.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Partify.Server;
using Partify.Server.Configuration;
using Swashbuckle.AspNetCore.Swagger;

namespace Partify.Web
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
            services.AddMvc().AddJsonOptions(
                options => { options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver(); });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });

            AddSwaggerConfiguration(services);
            var container = new ServiceContainer(new ContainerOptions() { EnablePropertyInjection = false });
            container.Register(factory => CreateConfiguration(), new PerContainerLifetime());
            container.ScopeManagerProvider = new PerLogicalCallContextScopeManagerProvider();
            container.RegisterFrom<CompositionRoot>();
            return container.CreateServiceProvider(services);
        }

        private Server.Configuration.IConfiguration CreateConfiguration()
        {
            return new Configuration(Configuration.GetSection("SpotifyClientId").Value, Configuration.GetSection("SpotifyClientSecret").Value, Configuration.GetSection("YouTubeServiceId").Value, Configuration.GetSection("RedirectUri").Value);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"); });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Content")),
                RequestPath = "/Content"
            });

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseMvc();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) => { await context.Response.WriteAsync("MVC didnt find anything"); });
        }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                .AddJsonFile("Keys.json", optional: true, reloadOnChange: true).AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        private static void AddSwaggerConfiguration(IServiceCollection services)
        {
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info { Title = "Partify API", Version = "v1" }); });
        }
    }
}