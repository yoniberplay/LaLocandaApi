using LaLocanda.Core.Application.Enums;
using LaLocanda.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace LaLocanda.Infrastructure.Identity.Seeds
{
    public static class DefaultWaiterUser
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            AppUser defaultUser = new();
            defaultUser.FirstName = "John";
            defaultUser.LastName = "Doe";
            defaultUser.UserName = "WaiterUser";
            defaultUser.Email = "defwaiter@email.com";
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumberConfirmed = true;

            if (userManager.Users.All(user => user.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);

                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Password123!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Waiter.ToString());
                }
            }
        }
    }
}