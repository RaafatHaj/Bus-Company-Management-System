using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.DTOs
{
	public class StationBoardingDTO
	{

		public int DistinationStationId { get; set; }
		public string DistinationStation { get; set; } = null!;
		public string PassengerName { get; set; } = null!;
		public Gender PassendgerGender { get; set; } 
		public string PassendgerPhone { get; set; } = null!;
		public int SeatNumber { get; set; }

	}
}
