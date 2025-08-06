using Travel_Company_MVC.Helper;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace Travel_Company_MVC.ViewModels
{
	public class SelectSeatViewModel
	{
		public int TripId { get; set; }
		public DateTime TripDepatureDateTime { get; set; }
		public IList<BookedSeatsDTO> BookedSeats { get; set; } = new List<BookedSeatsDTO>();
		public long SeatCode { get; set; }
		public int StationAId { get; set; }
		public int StationBId { get;set; }
	}
}
