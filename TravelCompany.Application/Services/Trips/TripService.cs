using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Collections.Immutable;
using System.Data;
using TravelCompany.Application.Common.Interfaces;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Application.Services.Travels
{
    internal class TripService : ITripService
    {


        private readonly IUnitOfWork _unitOfWork;
		

        public TripService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<Trip?> FindTripByIdAsync(int tripId , TripJoinedType joinedType= TripJoinedType.FullJoined)
		{


			//var query=  _unitOfWork.Trips.GetQueryable()
			//                      .Include(t => t.Route).ThenInclude(r => r.FirstStation)
			//                      .Include(t => t.Route).ThenInclude(r => r.LastStation)
			//                      .Include(t => t.TripAssignment).ThenInclude(a => a.Vehicle)
			//			 .AsNoTracking().SingleOrDefaultAsync();

			//if (asNoTracking)
			//	query.AsNoTracking();



			//return await query.SingleOrDefaultAsync();


            if (joinedType == TripJoinedType.WithRoute)
            {

                return await _unitOfWork.Trips.GetQueryable()
							 .Include(t => t.Route).ThenInclude(r => r!.FirstStation)
						     .Include(t => t.Route).ThenInclude(r => r!.LastStation)
							 .AsNoTracking().FirstOrDefaultAsync(t => t.Id == tripId);
            }


            return await _unitOfWork.Trips.GetQueryable()
                         .Include(t => t.Route).ThenInclude(r => r!.FirstStation)
                         .Include(t => t.Route).ThenInclude(r => r!.LastStation)
                         .Include(t => t.TripAssignment).ThenInclude(a => a.Vehicle)
                         .AsNoTracking().FirstOrDefaultAsync(t => t.Id == tripId);

        }

		public async Task<IEnumerable<StationTrackDTO>> GetStationTripSTrack(int stationId)
        {
            return await _unitOfWork.Trips.GetStationTripSTrack(stationId);
        }



		public async Task<Trip?> FindReturnTripByMainTripIdAsync(int mainTripId)
        {
            return await _unitOfWork.Trips.GetQueryable().FirstOrDefaultAsync(t => t.MainTripId==mainTripId);

        }
        public async Task<Trip?> FindMainTripByReturnTripIdAsync(int returnTripId)
        {
            return await _unitOfWork.Trips.GetQueryable()
                         .Include(t => t.Route).ThenInclude(r => r!.FirstStation)
                         .Include(t => t.Route).ThenInclude(r => r!.LastStation)
                         .Include(t => t.TripAssignment).ThenInclude(a => a.Vehicle)
                         .AsNoTracking().FirstOrDefaultAsync(t => t.ReturnTripId==returnTripId);

            //return await _unitOfWork.Trips.GetQueryable().FirstOrDefaultAsync(t => t.ReturnTripId == returnTripId);

        }
        public async Task<(bool Success , Trip? Trip)> EditTripTimeAsync(TripTimingDTO dto)
		{

			var trip = await FindTripByIdAsync(dto.TripId);

			if(trip==null)
				return (false, null);


			trip.Date= dto.Date;
			trip.Time= dto.Time;

			await _unitOfWork.SaveAsync();

			return (true, trip);

        }


        public async Task<(bool Success, Trip? Trip)> AddNewTripAsync(TripTimingDTO dto)
        {

            var trip = await FindTripByIdAsync(dto.TripId);

            if (trip != null)
                return (false, trip);

			//var newTrip = await _unitOfWork.Trips.GetQueryable().add


            trip!.Date = dto.Date;
            trip!.Time = dto.Time;

            await _unitOfWork.SaveAsync();

            return (true, trip);

        }



        public async Task<IEnumerable<Trip>> RetriveAllTripsAsync()
		{
			return await _unitOfWork.Trips.GetQueryable()
				.Include(t => t.Route)
				.Include(t=>t.TripAssignment).ThenInclude(a=>a.Vehicle).AsNoTracking()
				.ToListAsync();
			//return await _unitOfWork.Trips.GetAllTripsAsync();
		}

        public async Task<IEnumerable<Trip>> RetriveAllTripsAsync(int routeId , TimeSpan time)
        {
            return await _unitOfWork.Trips.GetQueryable()
                .Include(t => t.Route)
                .Include(t => t.TripAssignment).ThenInclude(a => a.Vehicle).AsNoTracking()
                .Where(t=>t.RouteId==routeId & t.Time==time)
                .ToListAsync();
            //return await _unitOfWork.Trips.GetAllTripsAsync();
        }
        public async Task<IEnumerable<Trip>> RetriveAllTripsAsync(IList<int?> returnTripsIds)
        {
            if (returnTripsIds is null)
                return Enumerable.Empty<Trip>();

            var trips = await _unitOfWork.Trips.GetQueryable()
                .Where(t => returnTripsIds.Contains(t.Id))
                .Include(t => t.Route)
                .Include(t => t.TripAssignment).ThenInclude(a => a.Vehicle)
                .AsNoTracking()
                .ToListAsync();

            return trips;
        }
        public async Task<(bool Success, IEnumerable<ScheduledTripBaseDTO>? Data)> ScheduleTripsAsync(ScheduleDTO schedule)
		{

			if(schedule.RecurringPattern==PatternType.Daily)
			{

				var scheduledTrips= await _unitOfWork.Trips.ScheduleTripsForEverySingleDayAsync(schedule);

				if(scheduledTrips!=null)
					return (true, scheduledTrips);
				else
					return (false, null);

		
			}
			else if (schedule.RecurringPattern==PatternType.Weekly)
			{

				var scheduledTrips = await _unitOfWork.Trips.ScheduleTripsForDaysInWeekAsync(schedule);

				if (scheduledTrips != null)
					return (true, scheduledTrips);
				else
					return (false, null);
			}
			else
			{

				var scheduledTrips = await _unitOfWork.Trips.ScheduleTripsForSpecificDatesAsync(schedule);

				if (scheduledTrips != null)
					return (true, scheduledTrips);
				else
					return (false, null);

			}

		}

		public async Task<IEnumerable<TripPattern>> GetTripsPatterns(int routeId)
		{

			return await _unitOfWork.TripPatterns.GetQueryable().Where(p=>p.RouteId == routeId).ToListAsync();

		}

		public async Task<IEnumerable<ScheduledTripBaseDTO>> GetScheduledTrips(ScheduledTripsSearchDTO dto)
		{
			if(dto.DateType == TripSearchDateType.SpecificDate)
			{
				//var trips= await _unitOfWork.Trips.GetQueryable().Where(t=>t.RouteId==dto.RouteId & t.Date==dto.TripDate)
				//                        .Include(t => t.Route)
				//                        .Include(t => t.TripAssignment).ThenInclude(a => a.Vehicle).AsNoTracking().ToListAsync();

				//            var returnTripsIds = trips.Select(t => t.ReturnTripId).ToList();

				//var reutrnTrips = await RetriveAllTripsAsync(returnTripsIds);

				//            trips.AddRange(reutrnTrips);
				var trips = await _unitOfWork.Trips.GetScheduledTripsAsync(dto.RouteId, dto.TripDate!.Value);

				var returnTripsIds = trips.Select(t => t.ReturnTripId).ToList();

				if(returnTripsIds is not null)
				{
					// Concat work with IEnumrable and gives you new inctance , 
					// AddRange work with IList and Edit the origanal list 

					var reutrnTrips = await _unitOfWork.Trips.GetScheduledTripsAsync(returnTripsIds);
					return trips.Concat(reutrnTrips);

				}


				return trips;


            }
			else
			{
                var lastDayOfMonth = DateTime.DaysInMonth(dto.Year!.Value, dto.Month!.Value);

                var endDate = new DateTime(dto.Year!.Value, dto.Month!.Value, lastDayOfMonth);
				var firsDate=new DateTime(dto.Year!.Value, dto.Month!.Value, 1);


				var trips = await _unitOfWork.Trips.GetScheduledTripsAsync(dto.RouteId, firsDate , endDate);

				var returnTripsIds = trips.Select(t => t.ReturnTripId).ToList();

				if (returnTripsIds is not null)
				{
					var reutrnTrips = await _unitOfWork.Trips.GetScheduledTripsAsync(returnTripsIds);
					return trips.Concat(reutrnTrips);

				}


				//var trips= await _unitOfWork.Trips.GetQueryable()
				//                    .Where(t => t.RouteId == dto.RouteId && t.Date >= firsDate && t.Date <= endDate)
				//                    .Include(t => t.Route)
				//                    .Include(t => t.TripAssignment).ThenInclude(a => a!.Vehicle).AsNoTracking()
				//                    .ToListAsync();

				//            var returnTripsIds = trips.Select(t => t.ReturnTripId).ToList();

				//            var reutrnTrips = await RetriveAllTripsAsync(returnTripsIds);

				//            trips.AddRange(reutrnTrips);

				return trips;

            }	      

		} 


		public async Task<IEnumerable<Trip>> GetPatternScheduledTrips(int routeId, TimeSpan time, DateTime startDate, DateTime endDate)
		{
			return await _unitOfWork.Trips.GetQueryable()
				.Where(t => t.RouteId == routeId &&
				t.Time == time && t.Date >= startDate && t.Date <= endDate)
                .Include(t => t.Route)
                .Include(t => t.TripAssignment).ThenInclude(a => a!.Vehicle).AsNoTracking()
                .ToListAsync();
		}
        public async Task<IEnumerable<PatternWeekDTO>> GetPatternWeeksAsync(PatternWeeksRequestDTO dto)
        {
            return await _unitOfWork.Trips.GetPatternWeeks(dto);
        }

        public async Task<IEnumerable<SuitableTripDTO>> FindSuitableTripsAsync(int stationAId, int stationBId, DateTime tipDate)
        {
            return await _unitOfWork.Trips.GetSuitableTripsAsync(stationAId, stationBId, tipDate);
        }

		public async Task<(long SeatCode, IList<BookedSeatsDTO> AvalibleSeats)> GetAvaliableSeatsAsync(int tripId, int stationAId, int stationBId)
		{
			return await _unitOfWork.Trips.GetAvaliableSeatsAsync(tripId,  stationAId,  stationBId);
		}


		public async Task<(bool Success , string? Message, ScheduledTripDTO? Trip)> EditTrip(EditScheduledTripDTO dto)
		{

			var result = await _unitOfWork.Trips.EditTrip(dto);

			if (!result.Success)
				return (false, result.Message, null);

			var mainTrip = await FindTripByIdAsync(dto.TripId);
			var returnTrip = await FindTripByIdAsync(result.ReturnTrpId);

			if (mainTrip is null || returnTrip is null)
				return (false, "Some thing went worng on data base level.", null);


			var trip = new ScheduledTripDTO
			{

				TripId = mainTrip.Id,
				Date = mainTrip.Date,
				Time = mainTrip.Time,
				DepartureStationId = mainTrip.Route!.FirstStationId,
				TripTimeSpanInMInits = mainTrip.Route.EstimatedTime,
				Status = mainTrip.status,
				MainTripId = mainTrip.MainTripId,


				VehicleId = mainTrip.TripAssignment?.VehicleId,
				VehicleNumber = mainTrip.TripAssignment?.Vehicle?.VehicleNumber,
				VehicleModel = mainTrip.TripAssignment?.Vehicle?.Type,

				RouteId = mainTrip.RouteId,
				RouteName=mainTrip.Route.RouteName,

				Seats = mainTrip.Seats,
				HasBookedSeat = mainTrip.HasBookedSeat,
				ArrivedStationOrder = mainTrip.ArrivedStationOrder,

				ReturnRouteId=returnTrip?.RouteId,
				ReturnTripId = returnTrip?.Id,
				ReturnTime = returnTrip?.Time,
				ReturnDate = returnTrip?.Date,
				ReturnStatus = returnTrip?.status,

			};

			return (true, "Vehicle assigned successfully.", trip);

		}

		public async Task<IEnumerable<TripTrackDTO>> GetTripTrackAsync(int tripId)
		{
			return await _unitOfWork.Trips.GetTripTrackAsunc(tripId);

		}

	
	}
}
