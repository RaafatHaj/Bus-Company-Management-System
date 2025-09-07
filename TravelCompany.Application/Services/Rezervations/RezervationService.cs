using Microsoft.EntityFrameworkCore;
using TravelCompany.Application.Common.Interfaces;
using TravelCompany.Application.Common.Interfaces.Repositories;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;

namespace TravelCompany.Application.Services.Rezervations
{
	public class RezervationService:IRezervationService
	{


		private readonly IUnitOfWork _unitOfWork;

		public RezervationService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<(bool IsBooked, IEnumerable<int> ReservationIDs)> BookSeatAsync(BookTicketDTO dto)
		{
			return await _unitOfWork.Reservations.BookSeatAsync(dto);
		}

        public async Task<IEnumerable<Reservation>> GetAllTripBookingsAsync(int tripId)
        {
            return await _unitOfWork.Reservations.GetQueryable().Where(r=>r.TripId==tripId).ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetStationPassengersAsync(int scheduledTravelId,int stationId)
		{

			var passengers=await _unitOfWork.Reservations.GetQueryable()
				.Where(r=>r.TripId==scheduledTravelId && r.StationAId==stationId).ToListAsync();

			return passengers;

		}

		public async Task<IEnumerable<Reservation>> GetStationPassengersBoarding(int tripId, int stationId)
		{
			return await _unitOfWork.Reservations.GetStationPassengersBoarding(tripId, stationId);
		}
	}
}
