namespace Travel_Company_MVC.ViewModels
{
	public class ScheduledTripViewModel
	{

		public int TripId { get; set; }
		public DateTime Date { get; set; }
		public TimeSpan Time { get; set; }
		public TripStatus Status { get; set; }


		public int? ReturnTripId { get; set; }
		public DateTime? ReturnDate { get; set; }
		public TimeSpan? ReturnTime { get; set; }
		public TripStatus? ReturnStatus { get; set; }


	}
}
