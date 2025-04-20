using Microsoft.AspNetCore.Identity;
using TravelCompany.Domain.Const;
using TravelCompany.Infrastructure.Persistence.Entities;

namespace Travel_Company_MVC.Seeds
{
    public static class DefaultUsers
    {

        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager)
        {

            var admin = new ApplicationUser() 
            {
                UserName = "admin",
                Email = "admin@TravelCompany.com",
                FullName = "Admin",
                EmailConfirmed = true,
                StationId = 1,
                StationName="Damascus_Center"
            };

            var user= await userManager.FindByEmailAsync(admin.Email);

            if(user == null)
            {
                await userManager.CreateAsync(admin,"P@ssword1234");
                await userManager.AddToRoleAsync(admin, AppRoles.Admin);
            }


        }
    }
}
