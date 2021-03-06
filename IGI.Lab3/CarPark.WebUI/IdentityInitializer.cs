﻿using System.Threading.Tasks;
using CarPark.WebUI.Areas.Identity;
using Microsoft.AspNetCore.Identity;

namespace CarPark.WebUI
{
    public class IdentityInitializer
    {
        private const string _adminEmail = "admin@mail.com";
        private const string _adminPass = "Qwerty123";

        public static async Task InitializerAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync(Roles.Admin) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }

            if (await roleManager.FindByNameAsync(Roles.User) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.User));
            }

            if (await userManager.FindByNameAsync(_adminEmail) == null)
            {
                var admin = new IdentityUser() { Email = _adminEmail, UserName = _adminEmail };
                var result = await userManager.CreateAsync(admin, _adminPass);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, Roles.Admin);
                }
            }
        }
    }


}
