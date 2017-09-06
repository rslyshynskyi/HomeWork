using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IdentityServer4.Models;
using IdentityServer4;

namespace IdentityServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddInMemoryClients(Clients.Get())
                .AddInMemoryIdentityResources(Resources.GetIdentityResources())
                .AddInMemoryApiResources(Resources.GetApiResources())
                .AddTestUsers(Users.Get())
                .AddTemporarySigningCredential();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseIdentityServer();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGoogleAuthentication(new GoogleOptions
            {
                AuthenticationScheme = "Google",
                DisplayName = "Google",
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,

                ClientId = "1066333007869-nlchf3a2c1hahdo6pmmvso04hta01qh8.apps.googleusercontent.com",
                ClientSecret = "tAv8x_h9n4rz9ex-qxx50CeY"
            });

            app.UseFacebookAuthentication(new FacebookOptions()
            {
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,

                AppId = "1705316566148220",
                AppSecret = "54cf17e8f8d382c7128764ca9e7e7fd1"
            });

            app.UseMicrosoftAccountAuthentication(new MicrosoftAccountOptions()
            {
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,

                ClientId = "8b43ec8c-b075-4f72-95b5-ff9f89a5155f",
                ClientSecret = "4y1nOvGecJHXEg9hpwJtCH3"
            });

            app.UseTwitterAuthentication(new TwitterOptions()
            {
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,

                ConsumerKey = "4AIxyMsVhbjRU1AgZp9cyvrRe",
                ConsumerSecret = "qMlWjbKf5nWNX896skzkw3g9i0SXD7Hq7ZLjnRsAZxAQaIcFyM"
            });

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
