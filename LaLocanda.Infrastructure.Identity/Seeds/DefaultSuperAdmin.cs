using LaLocanda.Core.Application.Enums;
using LaLocanda.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace LaLocanda.Infrastructure.Identity.Seeds
{
    public static class DefaultSuperAdmin
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            AppUser defaultUser = new();
            defaultUser.FirstName = "Super";
            defaultUser.LastName = "Admin";
            defaultUser.UserName = "SuperAdmin";
            defaultUser.Email = "superadmin@email.com";
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumberConfirmed = true;

            if (userManager.Users.All(user => user.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);

                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Mega123Pa$$!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                }
            }
        }
    }
}