using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
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

		public async Task<Trip?> FindTripByIdAsync(int tripId)
		{


			//var query=  _unitOfWork.Trips.GetQueryable()
   //                      .Include(t => t.Route).ThenInclude(r => r.FirstStation)
   //                      .Include(t => t.Route).ThenInclude(r => r.LastStation)
   //                      .Include(t => t.TripAssignment).ThenInclude(a => a.Vehicle)
			//			 .AsNoTracking().SingleOrDefaultAsync();

			//if (asNoTracking)
			//	query.AsNoTracking();



			//return await query.SingleOrDefaultAsync();
			return await _unitOfWork.Trips.GetQueryable()
                         .Include(t => t.Route).ThenInclude(r => r.FirstStation)
                         .Include(t => t.Route).ThenInclude(r => r.LastStation)
                         .Include(t => t.TripAssignment).ThenInclude(a => a.Vehicle)
                         .AsNoTracking().SingleOrDefaultAsync(t=>t.Id==tripId);
        }

		public async Task<IEnumerable<StationTrackDTO>> GetStationTripSTrack(int stationId)
        {
            return await _unitOfWork.Trips.GetStationTripSTrack(stationId);
        }



		public async Task<Trip?> FindReturnTripByMainTripIdAsync(int mainTripId)
        {
            return await _unitOfWork.Trips.GetQueryable().SingleOrDefaultAsync(t => t.MainTripId==mainTripId);

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

		public async Task<(IEnumerable<TripPattern>? TripsPatterns , IEnumerable<Trip>? Trips)> SearchForTrips(ScheduledTripsSearchDTO dto)
		{
			if(dto.SearchType == TripSearchType.TripPatterns)
			{
			    var tripsPatterns= await GetTripsPatterns(dto.RouteId);
				return (tripsPatterns, null);
			}

			return (null, null);
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

	
	}
}
