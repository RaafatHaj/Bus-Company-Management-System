using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml;

namespace TravelCompany.Domain.Entities
{
    public class Driver
    {

        public int DriverId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set;} = null!;
        public string LicenseNumber { get; set; } = null!;
        public DateTime LicenseExpiry {  get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string? Email {  get; set; }

        public int StationId { get; set; }
        public Station? Station { get; set; }

        //  public ICollection<ScheduledTrip> ScheduledTrips { get; set; } = new List<ScheduledTrip>();

        public ICollection<TripAssignment> Assignments { get; set; } = new List<TripAssignment>();

    }
}
