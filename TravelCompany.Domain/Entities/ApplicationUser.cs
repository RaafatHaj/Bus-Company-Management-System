using Microsoft.AspNetCore.Identity;
using TravelCompany.Domain.Entities;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Infrastructure.Persistence.Entities
{
    public class ApplicationUser : IdentityUser
    {

        public string FullName { get; set; } = null!;
        public string IdNumber { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public bool IsDeleted { get; set; }=false;
        public string? CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }= DateTime.Now;
        public string? LastUpdatedById { get; set; }
        public DateTime? LastUpdatedOn { get; set; }

        public int StationId { get; set; }
        public Station? Station { get; set; }
        public string?StationName { get; set; } 
       
    }
}
