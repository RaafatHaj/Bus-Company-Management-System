using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Domain.Entities
{
    public class Station
    {
        public int StationId { get; set; }
        public string StationName { get; set; } = null!;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public ICollection<Driver> Drivers { get; set; } = new List<Driver>();
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    }
}
