using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.Entities
{
    public class TripPattern
    {


        public int Id { get; set; }
        public int RouteId { get; set; }
        public route? Route { get; set; }
        public TimeSpan Time { get; set; }

        public PatternType? PatternType { get; set; }
        public decimal? Percentage { get; set; }
        public int? OccupiedWeekDaysCode { get; set; }



        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int OccupiedWeeksNumber {  get; set; }
        public int EmptyWeeksNumber { get; set; }



		public int? TripsNumber { get; set; }
        public int? UnassignedTripsNumber { get; set; }



    }
}
