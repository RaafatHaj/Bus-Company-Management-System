namespace Travel_Company_MVC.ViewModels
{
	public class CustomScheduledTripViewModel
	{



		public int RouteId { get; set; }
		public string RouteName { get; set; } = null!;
		public int TripId { get; set; }
		public DateTime Date { get; set; }
		public TimeSpan Time { get; set; }
		public int DepartureStationId { get; set; }
		public int TripTimeSpanInMInits { get; set; }
		public TripStatus Status { get; set; }

		public int? MainTripId { get; set; }
		public int? ReturnTripId { get; set; }

		public int? VehicleId { get; set; }
		public string? VehicleNumber { get; set; }
		public string? VehicleModel { get; set; }


	}
}
