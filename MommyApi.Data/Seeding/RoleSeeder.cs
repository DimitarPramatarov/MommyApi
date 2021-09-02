using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace MommyApi.Data.Seeding
{
    public class RoleSeeder : ISeeder
    {
        public void Seed(MommyApiDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            SeedRole(roleManager, "SuperAdmin");
            SeedRole(roleManager, "Moderator");
            SeedRole(roleManager, "User");
            SeedRole(roleManager, "ProUser");

        }

        private static void SeedRole(RoleManager<IdentityRole> roleManger, string roleName)
        {
            var role = roleManger.FindByNameAsync(roleName).GetAwaiter().GetResult();

            if(role is null)
            {
                var result = roleManger.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                
                if(!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(x => x.Description)));
                }
            }

        }
    }
}
