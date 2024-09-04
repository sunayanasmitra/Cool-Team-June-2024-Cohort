using HealthCareApp.Server.Models;
using Microsoft.AspNetCore.Identity;

namespace HealthCareApp.Server.Configuration
{
	public static class Roles
	{

		public static async Task AddRoles(IServiceProvider provider)

		{
			using (var scope = provider.CreateScope())
			{

				var RoleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>()!;

				var UserManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>()!;

				var roleExist = await RoleManager.RoleExistsAsync("Admin");

				if (!roleExist)

				{

					//create the roles and seed them to the database: Question 1

					var roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));

				}
			}

		}

		public static async Task CreateDefaultUsers(IServiceProvider provider)

		{

			using (var scope = provider.CreateScope())
			{
				var RoleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

				var UserManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

				var Admin = new ApplicationUser()

				{

					UserName = "Admin@example.com",

					Email = "Admin@example.com",

					EmailConfirmed = true

				};


				try
				{
					var res = await UserManager.CreateAsync(Admin, "Asdfasdf1!");
					if (!res.Succeeded)
					{
						var toDelete = await UserManager.FindByEmailAsync(Admin.Email);
						await UserManager.DeleteAsync(toDelete);
						var res2 = await UserManager.CreateAsync(Admin, "Asdfasdf1!");
					}
				}
				catch
				{
					await UserManager.DeleteAsync(Admin);
					var res = await UserManager.CreateAsync(Admin, "Asdfasdf1!");
				}
				await UserManager.AddToRolesAsync(Admin, new[] { "Admin" });

			}

		}

	}
}
