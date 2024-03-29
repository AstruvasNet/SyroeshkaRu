﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using SYR.Core.BusinessLogic.Common;
using System.IO;

namespace SYR.UserInterface.MVC
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddRouting(options => options.LowercaseUrls = true);
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddMemoryCache();
			services.AddSession();

			services.ServicesCollection();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseDatabaseErrorPage();
			app.UseHttpsRedirection();
			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.UseCookiePolicy();
			app.UseAuthentication();
			app.UseSession();

			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules")),
				RequestPath = "/node_modules"
			});

			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Client")),
				RequestPath = "/dev"
			});

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");

				routes.MapRoute("system", "{action}/{id?}",
					new {Controller = "home", Action = "index", AreaAttribute = ""});

				routes.MapRoute("account", "{area:exists}/{action}",
					new {Controller = "account", Action = "index"});

				routes.MapRoute("manage", "{area:exists}/{action}",
					new {Controller = "manage", Action = "index"});

				routes.MapRoute("admin", "cp/{controller}/{action}/{id?}",
					new {Controller = "site", Action = "index", AreaAttribute = "admin"});

				routes.MapRoute("partial", "{controller}/{action}/{id?}",
					new {Controller = "partial", Action = "index", AreaAttribute = "admin"});
			});
		}
	}
}