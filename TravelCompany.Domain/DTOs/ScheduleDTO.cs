using System.Data;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.DTOs
{
    public class ScheduleDTO
    {

        public int RouteId { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ReturnTime { get; set; }
        public int Seats { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ScheduleDuration? ScheduleDuration { get; set; }
        public PatternType RecurringPattern { get; set; }
        public DataTable? WeekDays { get; set; }
        public DataTable? CustomDates { get; set; }

	}
}