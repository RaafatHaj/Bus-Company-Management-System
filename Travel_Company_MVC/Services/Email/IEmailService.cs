using TravelCompany.Infrastructure.Persistence.Entities;

namespace Travel_Company_MVC.Services.Email
{
    public interface IEmailService
    {

         Task SendConfimingEmailAsync(ApplicationUser user);

    }
}
