using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using TravelCompany.Infrastructure.Persistence.Entities;

namespace Travel_Company_MVC.Helper
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {

        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity=await base.GenerateClaimsAsync(user);


            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FullName));

            if(user.StationId!=0)
                identity.AddClaim(new Claim(CustomClaimType.StationId, user.StationId.ToString()!));

            if(!string.IsNullOrEmpty(user.StationName))
                identity.AddClaim(new Claim(CustomClaimType.StationName, user.StationName!));




            return identity;

        }
    }
}
