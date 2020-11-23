using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Benday.VsliveVirtual.WebUi.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Benday.VsliveVirtual.WebUi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            ConfigureSecurity(services);
        }

        private void ConfigureSecurity(IServiceCollection services)
        {
            ConfigureAuthentication(services);
            ConfigureAuthorization(services);
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = new PathString("/Security/Login");
                        options.LogoutPath = new PathString("/Security/Logout");
                    });
        }

        private void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, LoggedInUsingEasyAuthHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(SecurityConstants.Policy_LoggedInUsingEasyAuth,
                                policy => policy.Requirements.Add(
                                    new LoggedInUsingEasyAuthRequirement()));

                options.DefaultPolicy = options.GetPolicy(SecurityConstants.Policy_LoggedInUsingEasyAuth);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
