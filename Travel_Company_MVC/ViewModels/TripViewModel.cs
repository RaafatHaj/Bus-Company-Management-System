namespace Travel_Company_MVC.ViewModels
{
	public class TripViewModel
	{

		public int Id { get; set; }
		public DateTime Date { get; set; }
		public TimeSpan Time { get; set; }
		public TripStatus status { get; set; }

		public int RouteId { get; set; }
		public route? Route { get; set; }
		public int Seats { get; set; }
		public bool HasBookedSeat { get; set; }
		public long StatusCode { get; set; }

		public int? MainTripId { get; set; }



	}
}
