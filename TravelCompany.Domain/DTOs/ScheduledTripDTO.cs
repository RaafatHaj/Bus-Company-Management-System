using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.DTOs
{
    public class ScheduledTripDTO : ScheduledTripBaseDTO
    {

      

        public int? ReturnTripId { get; set; }
        public DateTime? ReturnDate { get; set; }
        public TimeSpan? ReturnTime { get; set; }
        public TripStatus? ReturnStatus { get; set; }

  

    }
}
