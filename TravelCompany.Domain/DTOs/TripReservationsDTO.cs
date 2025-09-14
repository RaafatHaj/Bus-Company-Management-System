using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.DTOs
{
	public class TripReservationsDTO
	{
		public int DepartureStationId { get; set; }
		public string DepartureStationName { get; set; } = null!;
		public int ArrivalStationId { get; set; }
		public string ArrivilStationName { get; set; } = null!;

		public string PassengerName { get; set; } = null!;
		public Gender PassengerGender { get; set; }
		public string PassengerPhone { get; set; } = null!;
		public string? PassengerEmail { get; set; } 
		public int SeatNumber { get; set; }
		public DateTime BookedAt { get; set; }
		public string BookedBy { get; set; } = null!;
	}
}
