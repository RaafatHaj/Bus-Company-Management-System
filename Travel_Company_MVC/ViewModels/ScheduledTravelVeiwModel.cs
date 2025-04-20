using TravelCompany.Domain.Eums;

namespace Travel_Company_MVC.ViewModels
{

    public class ScheduledTravelVeiwModel
    {

        public int ScheduledTravelId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan TravelTime { get; set; }
        public string RouteName { get; set; } = null!;
        public int Seats { get; set; }
        public TripStatus Status { get; set; }
        public string FirstStation { get; set; } = null!;
        public string LastStation { get; set; } = null!;


    }
}
