using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.Entities
{
    public class Week
    {


        [Key]
        public int Id { get; set; }
        public int TripPatternId { get; set; }
        public TripPattern? TripPattern { get; set; }
        public int WeekOrder { get; set; }

        public int OccupiedDaysCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TripsNumber { get; set; }
        public int UnassignedTripsNumber { get; set; }
         

    }
}
