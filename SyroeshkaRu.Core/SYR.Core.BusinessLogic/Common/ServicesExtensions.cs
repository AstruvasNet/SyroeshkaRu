using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SYR.Core.BusinessLogic.Helpers.TagHelpers;
using SYR.Core.BusinessLogic.Interface;
using SYR.Core.BusinessLogic.Service;
using SYR.Core.DomainModel;
using SYR.Core.DomainModel.Common;
using SYR.Core.DomainModel.System;

namespace SYR.Core.BusinessLogic.Common
{
	public static class DependencyInjection
	{
		private static readonly ModelContext _db = new ModelContext();

		public static void ServicesCollection(this IServiceCollection services)
		{
			services.AddSingleton<IAuthorizationHandler, AccessHandler>();

			services.AddTransient<ISyroeshkaRu, SyroeshkaRuService>();

			services.AddTransient<IAdmin, AdminService>();

			services.AddDbContext<ModelContext>();

			services.AddTransient<UserManager<Users>>();

			services.AddTransient<FormHiddenTagHelpers>();

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddTransient<IEdit, EditService>();

			services.AddAntiforgery(t => t.HeaderName = "X-XSRF-TOKEN");

			services.AddIdentity<Users, Roles>(option =>
				{
					option.Password.RequiredLength = 5;
					option.Password.RequireNonAlphanumeric = false;
					option.Password.RequireLowercase = false;
					option.Password.RequireDigit = false;
					option.User.AllowedUserNameCharacters = "";
				})
				.AddEntityFrameworkStores<ModelContext>();

			using (var serviceScope = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>()
				.CreateScope())
			{
				DbInitialize.InitRoot(serviceScope.ServiceProvider.GetRequiredService<UserManager<Users>>(),
					serviceScope.ServiceProvider.GetRequiredService<RoleManager<Roles>>()).Wait();
			}
		}
	}
}