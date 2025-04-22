using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.Entities
{
    public class Recurring
    {
        public int RecurringId { get; set; }
        public int RouteId { get; set; }
        public route? Route { get; set; }
        public TimeSpan Time {  get; set; }
        public RecurringType RecurringType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsRecurring {  get; set; }
        public int TripsNumber { get; set; }
        public int IrregularTripsNumber { get; set; }

        public ICollection<Day> Days { get; set; } = new List<Day>();


    }
}
