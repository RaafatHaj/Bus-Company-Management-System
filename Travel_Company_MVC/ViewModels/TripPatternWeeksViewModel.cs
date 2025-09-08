namespace Travel_Company_MVC.ViewModels
{
	public class TripPatternWeeksViewModel
	{
		public int RouteId { get; set; }
		public TimeSpan PatternTime { get; set; }
		public IList<PatternWeekDTO> Weeks { get; set; }=new List<PatternWeekDTO>();

	}
}
