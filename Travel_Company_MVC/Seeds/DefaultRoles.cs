using Microsoft.AspNetCore.Identity;
using TravelCompany.Domain.Const;

namespace Travel_Company_MVC.Seeds
{
    public static class DefaultRoles
    {

        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {

            if(!roleManager.Roles.Any())
            {

               await roleManager.CreateAsync(new IdentityRole(AppRoles.Admin));
               await roleManager.CreateAsync(new IdentityRole(AppRoles.StationManager));
               await roleManager.CreateAsync(new IdentityRole(AppRoles.TripMonitor));
               await roleManager.CreateAsync(new IdentityRole(AppRoles.TicketAgent));
               await roleManager.CreateAsync(new IdentityRole(AppRoles.Driver));
               await roleManager.CreateAsync(new IdentityRole(AppRoles.Conductor));

            }

        }


    }
}
