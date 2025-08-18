
using TravelCompany.Application.Common.Responses;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Application.Services.Travels
{
    public interface ITripService
    {
		//  [sp_GetTravelByTravelTimeAndRouteId]
		//Task<SchedulingResult> ScheduleNewTravelsAsync(ScheduleDTO schedule);
		Task<IEnumerable<Trip>> RetriveAllTripsAsync();
        Task<IEnumerable<Trip>> RetriveAllTripsAsync(int routeId, TimeSpan time);
        Task<IEnumerable<Trip>> RetriveAllTripsAsync(IList<int?> returnTripsIds);
        Task<IEnumerable<SuitableTripDTO>> FindSuitableTripsAsync(int stationAId, int stationBId, DateTime tipDate);
		Task<(long SeatCode, IList<BookedSeatsDTO> AvalibleSeats)> GetAvaliableSeatsAsync(int tripId, int stationAId, int stationBId);
		Task<(bool Success, IEnumerable<ScheduledTripBaseDTO>? Data)> ScheduleTripsAsync(ScheduleDTO schedule);
        Task<Trip?> FindTripByIdAsync(int tripId, TripJoinedType joinedType = 0);
        Task<Trip?> FindReturnTripByMainTripIdAsync(int mainTripId);
        Task<(bool Success, Trip? Trip)> EditTripTimeAsync(TripTimingDTO dto);
        Task<IEnumerable<Trip>> GetScheduledTrips(ScheduledTripsSearchDTO dto);
        Task<IEnumerable<TripPattern>> GetTripsPatterns(int routeId);
        Task<IEnumerable<PatternWeekDTO>> GetPatternWeeksAsync(PatternWeeksRequestDTO dto);
        Task<IEnumerable<StationTrackDTO>> GetStationTripSTrack(int stationId);
        Task<(bool Success, string? Message, ScheduledTripDTO? Trip)> EditTrip(EditScheduledTripDTO dto);
        Task<IEnumerable<TripTrackDTO>> GetTripTrackAsync(int tripId);


	}
}
