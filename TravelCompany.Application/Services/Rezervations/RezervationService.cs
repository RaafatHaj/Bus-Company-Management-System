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

		public async Task<(bool IsBooked, int? ReserbationId)> BookTicketAsync(BookingSeatDTO dto)
		{
			return await _unitOfWork.Reservations.BookSeatAsync(dto);
		}

		public async Task<IEnumerable<Reservation>> GetStationPassengersAsync(int scheduledTravelId,int stationId)
		{

			var passengers=await _unitOfWork.Reservations.GetQueryable()
				.Where(r=>r.ScheduledTravelId==scheduledTravelId && r.StationAId==stationId).ToListAsync();

			return passengers;

		}
	}
}
