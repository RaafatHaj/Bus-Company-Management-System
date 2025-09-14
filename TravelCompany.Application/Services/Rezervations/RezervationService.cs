using Microsoft.EntityFrameworkCore;
using TravelCompany.Application.Common.Interfaces;
using TravelCompany.Application.Common.Interfaces.Repositories;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;
using TravelCompany.Domain.Eums;

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

        public async Task<IEnumerable<TripReservationsDTO>> GetAllTripBookingsAsync(int tripId)
        {

			return await _unitOfWork.Reservations.GetQueryable().AsNoTracking()
				.Where(b => b.TripId == tripId)
				.Select(b => new TripReservationsDTO
				{
					DepartureStationId=b.StationAId,
					DepartureStationName=b.StationA!.StationName,
					ArrivalStationId=b.StationBId,
					ArrivilStationName=b.StationB!.StationName,

					PassengerName=b.PersonName,
					PassengerGender=(Gender)b.PersonGender,
					PassengerEmail=b.PersonEmail,
					PassengerPhone=b.PersonPhone,

					SeatNumber=b.SeatNumber,
					BookedAt=b.CreatedOn,
					BookedBy=b.CreatedBy!.FullName


				}).ToListAsync();
            //return await _unitOfWork.Reservations.GetQueryable().Where(r=>r.TripId==tripId).ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetStationPassengersAsync(int scheduledTravelId,int stationId)
		{

			var passengers=await _unitOfWork.Reservations.GetQueryable()
				.Where(r=>r.TripId==scheduledTravelId && r.StationAId==stationId).ToListAsync();

			return passengers;

		}

		public async Task<IEnumerable<StationBoardingDTO>> GetStationPassengersBoarding(int tripId, int stationId)
		{

			return await _unitOfWork.Reservations.GetQueryable()
				.AsNoTracking()	
				.Where(r=>r.TripId==tripId && r.StationAId==stationId)
				.Select (r=> new StationBoardingDTO
				{
					DistinationStationId=r.StationBId,
					DistinationStation=r.StationB!.StationName,
					PassengerName=r.PersonName,
					PassendgerGender=(Gender)r.PersonGender,
					PassendgerPhone=r.PersonPhone,
					SeatNumber=r.SeatNumber

				})
				.ToListAsync();
				
			


			//return await _unitOfWork.Reservations.GetStationPassengersBoarding(tripId, stationId);
		}
	}
}
