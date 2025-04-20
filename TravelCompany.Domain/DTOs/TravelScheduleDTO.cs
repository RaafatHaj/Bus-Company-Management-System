using System.Data;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.DTOs
{
    public class TravelScheduleDTO
    {
        public int TravelId {  get; set; }
        public int RouteId { get; set; }
        public TimeSpan TravelTime { get; set; }
        public DateTime? StartingDate { get; set; }
       // public DateTime? ScheduleEngingDate { get; set; }
        public ScheduleDuration? ScheduleDuration { get; set; }
        public RecurringType SelectedScheduleType { get; set; }
        public int Seats { get; set; }

        public bool ReSchedule { get; set; }

        public DataTable? Dates { get; set; } 
        public DataTable? Days { get; set; }



    }
}