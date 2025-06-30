using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Domain.DTOs
{
    public class PatternWeekDTO
    {

        public int WeekOrder { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int OccupiedWeekDaysCode { get; set; }
        public int UnassignedTrips { get; set; }
        public int AllTrips { get; set; }
    }
}
