using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.Entities
{
    public class TravelStation
	{

		public int ScheduledTravelId { get; set; }
		public Trip? ScheduledTravel { get; set; }
		public int StationOrder { get; set; }
		public int StationId { get; set; }
		public Station? Station { get; set; }
		public StationStatus Status { get; set; }
		public DateTime ArrvalDateAndTime {  get; set; }
	//	public TimeSpan ArrivalTime { get; set; }
		public int AvailableSeats { get; set; }
		public int BookedSeates { get; set; }







	}
}
