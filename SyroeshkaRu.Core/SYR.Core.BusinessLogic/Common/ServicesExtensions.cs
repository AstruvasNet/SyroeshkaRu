using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SYR.Core.BusinessLogic.Interface;
using SYR.Core.BusinessLogic.Service;
using SYR.Core.DomainModel;
using SYR.Core.DomainModel.Common;
using SYR.Core.DomainModel.System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SYR.Core.BusinessLogic.Helpers;

namespace SYR.Core.BusinessLogic.Common
{
	public static class DependencyInjection
	{
		private static readonly ModelContext _db = new ModelContext();
		public static void ServicesCollection(this IServiceCollection services)
		{
			Task.Run(() =>
			{
				foreach (var s in _db.SequrityProfiles)
				{
					services.AddAuthorization(options =>
					{
						options.AddPolicy(
							s.Name, policy => policy.Requirements.Add(new AccessRequirement(s.Name)));
					});
				}
			}).Wait();

			services.AddSingleton<IAuthorizationHandler, AccessHandler>();

			services.AddTransient<ISyroeshkaRu, SyroeshkaRuService>();

			services.AddTransient<IAdmin, AdminService>();

			services.AddDbContext<ModelContext>();

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

			services.AddTransient<UserManager<Users>>();

			services.AddTransient<FormHiddenTagHelpers>();

			services.AddSingleton<Dictionary<string, string>>();

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddTransient<IEdit, EditService>();
		}
	}
}
