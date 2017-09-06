using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MatOrderingService.Exceptions;
using MatOrderingService.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MatOrderingService.Middleware;
using MatOrderingService.Services.Auth;
using MatOrderingService.Services.CodeGenerator;
using MatOrderingService.Services.CodeGenerator.Impl;
using MatOrderingService.Services.Storage;
using MatOrderingService.Services.Storage.Impl;
using MatOrderingService.Services.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace MatOrderingService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetValue<string>("Data:ConnectionString");

            services.AddDbContext<OrdersDbContext>(options => 
                options.UseSqlServer(connectionString)
                .ConfigureWarnings(warnings => warnings.Log(RelationalEventId.QueryClientEvaluationWarning)));
            
            services.Configure<MatOsAuthOptions>(Configuration.GetSection("AuthOptions"));
            services.AddAuthorization(auth =>
            {
                auth.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(Configuration.GetValue<string>("AuthOptions:AuthenticationScheme"))
                    .RequireAuthenticatedUser().Build();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Materialise Academy Orders API", Version = "v1" });
                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath,
                    "MatOrderingService.xml");
                c.IncludeXmlComments(filePath);
                c.OperationFilter<SwaggerAuthorizationHeaderParameter>(Configuration.GetValue<string>("AuthOptions:AuthenticationScheme"));
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(EntityNotFoundExceptionFilter));
                options.Filters.Add(typeof(BadRequestExceptionFilter));
            });

            services.AddSingleton<ICodeGenerator, CodeGeneratorClient>();

            services.AddSingleton<IOrdersList, OrdersListService>();
            
            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            app.UseMiddleware<MatOsAuthMiddleware>();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Materialise Academy Ordering API");
            });

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
