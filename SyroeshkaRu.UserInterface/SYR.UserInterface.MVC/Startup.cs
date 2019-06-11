using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SYR.UserInterface.MVC.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SYR.Core.DomainModel;
using SYR.Core.BusinessLogic.Common;
using SYR.Core.DomainModel.Common;
using SYR.Core.DomainModel.System;
using IdentityUser = Microsoft.AspNetCore.Identity.IdentityUser;

namespace SYR.UserInterface.MVC
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

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
			app.UseStaticFiles();
			app.UseCookiePolicy();
			app.UseAuthentication();
			app.UseSession();
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

				routes.MapRoute("root", "cp/{controller}/{action}/{id?}",
					new {Controller = "root", Action = "index", AreaAttribute = "admin"});

				routes.MapRoute("partial", "cp/{controller}/{action}/{type?}/{id?}",
					new { Controller = "root", Action = "index", AreaAttribute = "admin" });
			});
		}
	}
}
