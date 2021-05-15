using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
// using AuctionSystemCore.Models;
using Microsoft.Identity.Client;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
// using AuctionSystemCore.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http.Connections;
using Newtonsoft.Json; 
using Newtonsoft.Json.Linq;
using AuctionSystemCore.Middlewares;

namespace AuctionSystemCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Adds required services to the container for us to consume on handling each request
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder.AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowAnyOrigin()
                       .AllowCredentials();
            }));
            
            services.AddLogging();

            services.Configure<ApiBehaviorOptions>(apiBehaviorOptions =>
                apiBehaviorOptions.InvalidModelStateResponseFactory = actionContext => {
                    Console.WriteLine(JsonConvert.SerializeObject(actionContext.ModelState.Values));
                    return new BadRequestObjectResult(new {
                        success = false,
                        errors = actionContext.ModelState.Values.SelectMany(x => x.Errors)
                            .Select(x => x.ErrorMessage)
                    });
                }
            );

            services.AddApiVersioning(options =>  
            {  
                options.ReportApiVersions = true;  
                options.DefaultApiVersion = new ApiVersion(1, 0);  
                options.AssumeDefaultVersionWhenUnspecified = true;  
            });
            services.AddHttpContextAccessor();
            services.AddSingleton<IConfiguration>(Configuration);
            // services.AddDbContext<dbContext>(_ => new dbContext(Configuration["ConnectionString"].ToString()));
        }

        // Configures the HTTP pipeline by constructing the stack of middlewares to pass through for each request
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseMiddleware<ExceptionMiddleware>();

            //this might not be required as we are running it on local
            app.UseCors("CorsPolicy");
            app.UseMvc();
        }
    }
}
