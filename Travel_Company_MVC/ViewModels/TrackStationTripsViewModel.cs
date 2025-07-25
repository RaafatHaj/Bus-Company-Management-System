using Microsoft.AspNetCore.Mvc.Rendering;

namespace Travel_Company_MVC.ViewModels
{
	public class TrackStationTripsViewModel
	{
		public IEnumerable<SelectListItem>? Stations {  get; set; }
		public int StationId { get; set; } = 0;

	}
}
