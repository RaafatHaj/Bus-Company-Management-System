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

		public async Task<(bool IsBooked, IEnumerable<ReservationDTO>? Reservations)> BookSeatAsync(BookTicketDTO dto)
		{


			var result = await _unitOfWork.Reservations.BookSeatAsync(dto);

			if (!result.IsBooked)
				return (false, null);

			var reservations = await _unitOfWork.Reservations.GetQueryable().AsNoTracking()
								   .Where(r => result.ReservationIDs.Contains(r.Id))
								   .Select(r=>new ReservationDTO
								   {
									   Id=r.Id,
									   TripId=r.TripId,
									   StationAId=r.StationAId,
									   StationAName=r.StationA!.StationName,
									   StationBId=r.StationBId,
									   StationBName=r.StationB!.StationName,
									   SeatNumber=r.SeatNumber,
									   PersonId=r.PersonId,
									   PersonName=r.PersonName,
									   PersonEmail=r.PersonEmail,
									   PersonPhone=r.PersonPhone,
									   PersonGender=r.PersonGender,

									   TripDepartureDateTime=r.TripDepartureDateTime
								   })
								   .ToListAsync();

			return (true, reservations);
			




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

		public async Task<IEnumerable<TripReservationsDTO>> GetStationPassengersBoarding(int tripId, int stationId)
		{

			return await _unitOfWork.Reservations.GetQueryable().AsNoTracking()
				.Where(b => b.TripId == tripId && b.StationAId == stationId)
				.Select(b => new TripReservationsDTO
				{
					DepartureStationId = b.StationAId,
					DepartureStationName = b.StationA!.StationName,
					ArrivalStationId = b.StationBId,
					ArrivilStationName = b.StationB!.StationName,

					PassengerName = b.PersonName,
					PassengerGender = (Gender)b.PersonGender,
					PassengerEmail = b.PersonEmail,
					PassengerPhone = b.PersonPhone,

					SeatNumber = b.SeatNumber,
					BookedAt = b.CreatedOn,
					BookedBy = b.CreatedBy!.FullName


				}).ToListAsync();


			//return await _unitOfWork.Reservations.GetQueryable()
			//	.AsNoTracking()	
			//	.Where(r=>r.TripId==tripId && r.StationAId==stationId)
			//	.Select (r=> new StationBoardingDTO
			//	{
			//		DistinationStationId=r.StationBId,
			//		DistinationStation=r.StationB!.StationName,
			//		PassengerName=r.PersonName,
			//		PassendgerGender=(Gender)r.PersonGender,
			//		PassendgerPhone=r.PersonPhone,
			//		SeatNumber=r.SeatNumber

			//	})
			//	.ToListAsync();
				
			


			//return await _unitOfWork.Reservations.GetStationPassengersBoarding(tripId, stationId);
		}
	}
}
