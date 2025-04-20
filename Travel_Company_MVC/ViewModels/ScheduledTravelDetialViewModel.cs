using TravelCompany.Domain.Entities;
using TravelCompany.Domain.Eums;

namespace Travel_Company_MVC.ViewModels
{
    public class ScheduledTravelDetialViewModel
    {

        public int ScheduledTravelId { get; set; }
        public int StationOrder { get; set; }
        public int StationId { get; set; }
        public string StationName { get; set; } = null!;
        public StationStatus Status { get; set; }
        public DateTime Date{ get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public int AvailableSeats { get; set; }
        public int BookedSeates { get; set; }
    }
}
