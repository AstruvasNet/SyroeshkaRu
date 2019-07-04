using Microsoft.AspNetCore.Identity;
using SYR.Core.DomainModel.System;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SYR.Core.DomainModel.Common
{
	public static class DbInitialize
	{
		public static async Task InitRoot(UserManager<Users> userManager, RoleManager<Roles> roleManager)
		{
			string email = "astruvas@gmail.com";
			string password = "Card5687vertebra~";

			if (await roleManager.FindByNameAsync("root") == null)
			{
				await roleManager.CreateAsync(new Roles
				{
					Name = "root",
					Content = "Супер администратор"
				});
			}

			if (await roleManager.FindByNameAsync("admin") == null)
			{
				await roleManager.CreateAsync(new Roles
				{
					Name = "admin",
					Content = "Администратор"
				});
			}

			if (await roleManager.FindByNameAsync("customer") == null)
			{
				await roleManager.CreateAsync(new Roles
				{
					Name = "customer",
					Content = "Клиент магазина"
				});
			}

			if (await userManager.FindByNameAsync(email) == null)
			{
				Users root = new Users
				{
					Email = email,
					UserName = email
				};
				IdentityResult result = await userManager.CreateAsync(root, password);
				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(root, "root");
				}
			}

			using (var db = new ModelContext())
			{
				if (db.SequrityProfiles.Count() == 0)
				{
					var roleId = roleManager.Roles.FirstOrDefault(i => i.Name.Contains("root"))?.Id;

					var rootProfile = new SequrityProfiles
					{
						Id = Guid.NewGuid(),
						Name = "root",
						Title = "Только супер администратор"
					};

					var rootRoles = new SequrityRoles
					{
						RoleId = roleId,
						SequrityProfile = rootProfile
					};

					db.SequrityProfiles.Add(rootProfile);
					db.SequrityRoles.Add(rootRoles);
				}

				db.SaveChanges();
			}
		}
	}
}