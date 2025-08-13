using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;

namespace TravelCompany.Application.Common.Interfaces.Repositories
{
    public interface ITripRepository : IBaseRepository<Trip>
    {

		Task<IEnumerable<Trip>> GetAllTripsAsync();
		Task<IEnumerable<ScheduledTripBaseDTO>> ScheduleTripsForDaysInWeekAsync(ScheduleDTO dto);
		Task<IEnumerable<ScheduledTripBaseDTO>> ScheduleTripsForEverySingleDayAsync(ScheduleDTO dto);
        Task<IEnumerable<SuitableTripDTO>> GetSuitableTripsAsync(int stationAId, int StationBId, DateTime tripDate);
		Task<(long SeatCode, IList<BookedSeatsDTO> AvalibleSeats)> GetAvaliableSeatsAsync(int tripId, int stationAId, int stationBId);
		Task<IEnumerable<ScheduledTripBaseDTO>> ScheduleTripsForSpecificDatesAsync(ScheduleDTO dto);
		Task<IEnumerable<PatternWeekDTO>> GetPatternWeeks(PatternWeeksRequestDTO dto);
		Task<IEnumerable<StationTrackDTO>> GetStationTripSTrack(int stationId);
		Task<(bool Success, int ReturnTrpId, string Message)> EditTrip(EditScheduledTripDTO dto);
		Task<IEnumerable<TripTrackDTO>> GetTripTrackAsunc(int tripId);

	}
}
