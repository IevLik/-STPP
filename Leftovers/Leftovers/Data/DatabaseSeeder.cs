using Leftovers.Auth.Model;
using Leftovers.Data.Dtos.Auth;
using Microsoft.AspNetCore.Identity;

namespace Leftovers.Data
{
    public class DatabaseSeeder
    {
        private readonly UserManager<LeftoversUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DatabaseSeeder(UserManager<LeftoversUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task SeedAsync()
        {
            foreach (var role in LeftoversUserRoles.All)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var newAdminUser = new LeftoversUser
            {
                UserName = "admin",
                Email = "admin@admin.com"
            };
            var existingAdminUser = await _userManager.FindByNameAsync(newAdminUser.UserName);
            if (existingAdminUser == null)
            {
                var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, "VerySafePassword1!");
                if (createAdminUserResult.Succeeded)
                {
                    await _userManager.AddToRolesAsync(newAdminUser, LeftoversUserRoles.All);
                }
            }
        }
    }
}
