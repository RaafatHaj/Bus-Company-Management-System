using Microsoft.AspNetCore.Identity;
using TravelCompany.Domain.Entities;

namespace TravelCompany.Infrastructure.Persistence.Entities
{
    public class ApplicationUser : IdentityUser
    {

        public string FullName { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public string? CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }= DateTime.Now;
        public string? LastUpdatedById { get; set; }
        public DateTime? LastUpdatedOn { get; set; }

        public int StationId { get; set; }
        public Station? Station { get; set; }
        public string?StationName { get; set; } 
       
    }
}
