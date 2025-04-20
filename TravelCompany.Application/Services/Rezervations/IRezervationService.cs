using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;

namespace TravelCompany.Application.Services.Rezervations
{
	public interface IRezervationService
	{


		Task<(bool IsBooked, int? ReserbationId)> BookTicketAsync(BookingSeatDTO dto);
		Task<IEnumerable<Reservation>> GetStationPassengersAsync(int scheduledTravelId, int stationId);

    }
}
