namespace Travel_Company_MVC.ViewModels
{
	public class TripTrackViewModel
	{

		public int TripId { get; set; }
		public int StationOrder { get; set; }
		public int StationId { get; set; }
		public string StationName { get; set; } = null!;
		public StationStatus Status { get; set; }
		public DateTime ArrivalDateTime { get; set; }
        public int ArrivalLateMinutes { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public int DepartureLateMinutes { get; set; }
    }
}
