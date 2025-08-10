using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.DTOs
{
	public class ScheduledTripsSearchDTO
	{
		public int RouteId { get; set; }
		public TripSearchType SearchType { get; set; }
		public TripSearchDateType? DateType { get; set; }
		public DateTime? TripDate { get; set; }
		public int? Month { get; set; }
		public int? Year { get; set; }

	}
}
