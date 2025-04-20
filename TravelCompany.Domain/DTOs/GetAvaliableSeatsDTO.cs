using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Domain.DTOs
{
    public class GetAvaliableSeatsDTO
    {

        public int seatsNumbers {  get; set; }
        public int StationAOrder { get; set; }

        public int StationAId { get; set; }


        public int StationBOrder { get; set; }
        public int StationBId { get; set; }

        public int RouteStationsNumber {  get; set; }
        public int ScheduledTravelId {  get; set; }
                 
    }
}
