using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.Entities
{
    public class Schedule
    {

        public int Id { get; set; }
        public int DriverId { get; set; }
        public Driver? Driver { get; set; }
        public DateTime Date { get; set; }
        public DriverStatus DriverStatus { get; set; }
        public TimeSpan? ShiftMinStartTime { get; set; }
        public TimeSpan? ShiftStartTime { get; set; }
        public TimeSpan? ShiftEndTime { get; set; }
        public long ActiveTime { get; set; }
        public bool IsShiftFull { get; set; }

    }
}
