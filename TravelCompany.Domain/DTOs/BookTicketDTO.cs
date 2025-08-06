using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Domain.DTOs
{
	public class BookTicketDTO
	{

		public int TripId { get; set; }
		public DateTime TripDepatureDateTime { get; set; }

		public long SeatCode { get; set; }
		public int StationAId { get; set; }
		public int StationBId { get; set; }
		public string PersonPhone { get; set; } = null!;
		public string? PersonEmail { get; set; }

		public DataTable BookedSeatsTable { get; set; }= null!;

		public string? CreatedById { get; set; }
		public DateTime CreatedOn { get; set; } = DateTime.Now;
	}
}
