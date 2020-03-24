﻿using CoreHome.Data.DatabaseContext;
using CoreHome.Infrastructure.Models;
using CoreHome.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System;

namespace CoreHome.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // 将服务添加到容器
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddControllersWithViews();

            services.AddDbContext<ArticleDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("ArticleDb"), mySqlOptions =>
                {
                    mySqlOptions.ServerVersion(new ServerVersion(new Version(8, 0, 18), ServerType.MySql));
                });
            });

            services.AddSingleton<BingWallpaperService>();
            services.AddSingleton<ThemeService>();
            services.AddSingleton(new NotifyService(Configuration.GetValue<string>("ServerChanSckey")));
            services.AddSingleton(new OssService(Configuration.GetSection("OssConfig").Get<OssConfig>()));

            services.Configure<CookieOptions>(config =>
            {
                config.SameSite = SameSiteMode.Strict;
            });
        }

        // 配置应用服务
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ArticleDbContext articleDbContext)
        {
            if (!articleDbContext.Database.CanConnect())
            {
                articleDbContext.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UsePathBase(new PathString("/Admin"));

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Add("Content-Security-Policy", "script-src 'self' 'unsafe-inline';");
                context.Response.Headers.Add("Referrer-Policy", "no-referrer-when-downgrade");
                context.Response.Headers.Add("Feature-Policy", "autoplay 'self'; notifications 'self';");
                await next.Invoke();
            });

            app.UseWebSockets();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
