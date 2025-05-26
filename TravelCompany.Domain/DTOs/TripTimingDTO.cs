using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Domain.DTOs
{
    public class TripTimingDTO
    {

        public int TripId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }


    }
}
