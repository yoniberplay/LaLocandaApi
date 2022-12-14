using LaLocanda.Core.Application.Enums;
using LaLocanda.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLocanda.Infrastucture.Identity.Seeds
{
    public class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new();
            defaultUser.FirstName = "YONIBER";
            defaultUser.LastName = "ENCARNACION";
            defaultUser.UserName = "yoniberplay";
            defaultUser.Email = "20211442@itla.edu.do";
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);

                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser,"YOniber00+");
                    await userManager.AddToRoleAsync(defaultUser,Roles.Basic.ToString());
                }

            }
        }

    }
}
