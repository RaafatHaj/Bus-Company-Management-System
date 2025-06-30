namespace Travel_Company_MVC.ViewModels
{
	public class TripsPatternViewModel
	{



		public int Id { get; set; }
		public int RouteId { get; set; }
		public TimeSpan Time { get; set; }

		public PatternType? PatternType { get; set; }
		public decimal? Percentage { get; set; }
		public int? OccupiedWeekDaysCode { get; set; }



		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }


		public int? TripsNumber { get; set; }
		public int? UnassignedTripsNumber { get; set; }



	}
}
