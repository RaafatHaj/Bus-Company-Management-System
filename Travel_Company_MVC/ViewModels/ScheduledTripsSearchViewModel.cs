namespace Travel_Company_MVC.ViewModels
{
	public class ScheduledTripsSearchViewModel
	{

		public int RouteId {  get; set; }
		public TripSearchType SearchType { get; set; }
		public TripSearchDateType? DateType { get; set; }
		public DateTime? TripDate { get; set; }
		public int? Month { get; set; }
		public int? Year { get; set; }

	}
}
