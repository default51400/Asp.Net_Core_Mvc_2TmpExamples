﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ConfiguringApps.Infrastructure;

namespace ConfiguringApps
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<UptimeService>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                app.UseBrowserLink();
                

                //if statusCode=403 => Edge not supp., if=404 => No content response;
                app.UseMiddleware<ErrorMiddleware>();
                //if header httpContext.Items["EdgeBrowser"]== true => 403, else Next;
                app.UseMiddleware<BrowserTypeMiddleware>();
                //if headers "User-Agent" contains "edge"=> Ok, else Next;
                app.UseMiddleware<ShortCircuitMiddleware>();
                //if route contains "/middleware"=> Ok, else Next;
                app.UseMiddleware<ContentMiddleware>();
            }
            else app.UseExceptionHandler("/Home/Error");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
