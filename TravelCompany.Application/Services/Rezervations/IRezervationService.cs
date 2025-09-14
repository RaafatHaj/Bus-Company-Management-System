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


		Task<(bool IsBooked, IEnumerable<int> ReservationIDs)> BookSeatAsync(BookTicketDTO dto);
		Task<IEnumerable<Reservation>> GetStationPassengersAsync(int scheduledTravelId, int stationId);
		Task<IEnumerable<StationBoardingDTO>> GetStationPassengersBoarding(int tripId, int stationId);
		Task<IEnumerable<TripReservationsDTO>> GetAllTripBookingsAsync (int tripId);
	}
}
