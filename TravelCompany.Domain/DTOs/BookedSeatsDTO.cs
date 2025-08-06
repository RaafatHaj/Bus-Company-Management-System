using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.DTOs
{
	public class BookedSeatsDTO
	{
		public int SeatNumber { get; set; }
		public SeatStatus Status { get; set; }
		public bool IsSelected { get; set; }=false;

	}
}
