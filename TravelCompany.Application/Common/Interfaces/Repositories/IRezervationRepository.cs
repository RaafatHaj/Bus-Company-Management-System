using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;

namespace TravelCompany.Application.Common.Interfaces.Repositories
{
    public interface IRezervationRepository: IBaseRepository<Reservation>
	{
		Task<(bool IsBooked, IEnumerable<int> ReservationIDs)> BookSeatAsync(BookTicketDTO dto);
		Task<IEnumerable<Reservation>> GetStationPassengersBoarding(int tripId, int stationId);
	}
}
