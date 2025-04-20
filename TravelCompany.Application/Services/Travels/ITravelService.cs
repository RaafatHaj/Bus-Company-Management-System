
using TravelCompany.Application.Common.Responses;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;

namespace TravelCompany.Application.Services.Travels
{
    public interface ITravelService
    {
		//  [sp_GetTravelByTravelTimeAndRouteId]
		Task<SchedulingResult> ScheduleNewTravelsAsync(TravelScheduleDTO schedule);

        Task<Trip?> FindTravelAsync(TimeSpan travelTime, int routeId);

		Task<Trip?> FindTravelAsync(int travelId);


    }
}
