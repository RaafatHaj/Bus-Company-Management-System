using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Domain.DTOs
{
	public class BookingSeatDTO
	{


		public int ScheduledTravelId { get; set; }


		public int StationAOrder { get; set; }
		public int StationAId { get; set; }


		public int StationBOrder { get; set; }
		public int StationBId { get; set; }



		public int SeatNumber { get; set; }

		public int RouteStationsNumber { get; set; }

		public string PersonId { get; set; } = null!;
		public string PersonName { get; set; } = null!;
		public string PersonPhone { get; set; } = null!;

		public string? PersonEmail { get; set; }
		public bool? PersonGendor { get; set; }



	}
}
