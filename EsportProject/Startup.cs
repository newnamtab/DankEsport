﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using Microsoft.AspNetCore.Http;
using EsportProject.Models.DBmodels;
using Microsoft.EntityFrameworkCore;
using EsportProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EsportProject.Classes;
using Microsoft.AspNetCore.Identity;

namespace EsportProject
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            env.ConfigureNLog("nlog.config");
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddMvc();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddIdentity<ApplicationUser, IdentityRole>(
                options => {
                    options.Cookies.ApplicationCookie.LoginPath = "/Account/Login";
                    options.Cookies.ApplicationCookie.AccessDeniedPath = "/Home/Index";
                })
                .AddEntityFrameworkStores<UserContext>()
                .AddDefaultTokenProviders();
                
            //DBconnection
            var connection = @"Server=mysql34.unoeuro.com;User Id=cronen_dk;Password=testyv92;Database=cronen_dk_db";
            services.AddDbContext<NewsContext>(options => options.UseMySql(connection));
            services.AddDbContext<TurnamentContext>(options => options.UseMySql(connection));
            services.AddDbContext<UserContext>(options => options.UseMySql(connection));
            services.AddDbContext<ContactContext>(options => options.UseMySql(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // IMPORTANT: This session call MUST go before UseMvc()
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();
            //Overstående fjernes grundet NLog
            loggerFactory.AddNLog();
            app.AddNLogWeb();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStatusCodePagesWithRedirects("/Home/NotFound");
            app.UseIdentity(); 
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            new UserRoleSeed(app.ApplicationServices.GetService<RoleManager<IdentityRole>>()).Seed();
        }
    }
}
