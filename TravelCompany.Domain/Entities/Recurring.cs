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
        public int TripId { get; set; }
        public Trip? Trip { get; set; }
        public RecurringType RecurringType { get; set; }
        public DateTime? FirstTripDate { get; set; }
        public DateTime? LastTripDate { get; set; }
        public bool IsActive { get; set; }
        public bool Reschedule {  get; set; }
        public bool HasIrregularTrip { get; set; }
        public bool HasUnsignedTrip { get; set; }

        public ICollection<Day> Days { get; set; } = new List<Day>();


    }
}
