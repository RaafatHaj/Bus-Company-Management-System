using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Domain.DTOs
{
    public class AvailableTripVehicleDTO
    {
        public bool fleg=false;
        public bool fleg2=false;
        public int VehicleId { get; set; }
        public string HomeStation { get; set; } = null!;
        public string VehicleModel { get; set; } = null!;
        public string VehicleNumber { get; set; }=null!;
        public int Seats {  get; set; }
        //public bool IsAvailable { get; set; }
        public DateTime AvailibiltyStartDateTime { get; set; }
        public DateTime AvailibiltyEndDateTime { get; set; }

    }
}
