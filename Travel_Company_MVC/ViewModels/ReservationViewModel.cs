namespace Travel_Company_MVC.ViewModels
{
    public class ReservationViewModel
    {


        public int Id { get; set; }

        public int TripId { get; set; }

        public int StationAId { get; set; }
        public string StationAName { get; set; } = null!;

        public int StationBId { get; set; }
        public string StationBName { get; set; } = null!;

        public int SeatNumber { get; set; }

        public string PersonId { get; set; } = null!;
        public string PersonName { get; set; } = null!;
        public string PersonPhone { get; set; } = null!;
        public string? PersonEmail { get; set; }
        public SeatStatus PersonGender { get; set; }
        public DateTime TripDepartureDateTime { get; set; }

    }
}
