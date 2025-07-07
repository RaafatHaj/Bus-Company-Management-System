using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Domain.DTOs
{
    public class AvailableTripVehicleDTO
    {
        public int VehicleId { get; set; }
        public string HomeStation { get; set; } = null!;
        public string VehicleModel { get; set; } = null!;
        public string VehicleNumber { get; set; }=null!;
        //public bool IsAvailable { get; set; }
        public DateTime AvailibiltyStartTime { get; set; }
        public DateTime AvailibiltyEndTime { get; set; }

    }
}
