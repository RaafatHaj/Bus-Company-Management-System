using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;

namespace TravelCompany.Application.Services.Routes
{
	public interface IRouteService
    {

        Task<IEnumerable<route>> GetAllRoutesAsync();
        IEnumerable<route> GetAllRoutes();

		public Task<RouteStationsDTO?> GetRouteStationsAsync(int routeID);

        public Task<route?> GetRouteDetails(int routeId);


	}
}
