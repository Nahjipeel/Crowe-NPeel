using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Crowe.Services;
using Swashbuckle.AspNetCore.Swagger;
using Serilog.Sinks.Stackify;
using Crowe.Models;

namespace Crowe
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
            //.ReadFrom.Configuration(Configuration)
            .MinimumLevel.Verbose()
            .WriteTo.Console(Serilog.Events.LogEventLevel.Verbose)
            .WriteTo.Seq("http://localhost:5341")
            //.WriteTo.Stackify()
            .CreateLogger();

            Serilog.Debugging.SelfLog.Enable(Console.Out);

            Log.Fatal("Hello, {Name}!", Environment.UserName);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddMvc();
            services.AddScoped<MessagesService>();

            services.AddEntityFrameworkSqlite().AddDbContext<MessagesContext>();

            services.AddLogging(loggingBuilder =>
          loggingBuilder.AddSerilog(dispose: true));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //AddSerilog registers a Serilog ILogger to receive logging events.
            // could have also done this: services.AddSingleton<Serilog.ILogger>(logger);
            loggerFactory.AddSerilog();

            app.UseCors(builder =>
                builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseMvc();
        }
    }
}
