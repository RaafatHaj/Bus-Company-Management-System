using Microsoft.Extensions.DependencyInjection;
using TravelCompany.Application.Services.Points;
using TravelCompany.Application.Services.Rezervations;
using TravelCompany.Application.Services.RoutePoints;
using TravelCompany.Application.Services.Routes;
using TravelCompany.Application.Services.ScheduledTravels;
using TravelCompany.Application.Services.Stations;
using TravelCompany.Application.Services.Travels;

namespace TravelCompany.Application
{
    static public class ConfigureServices
    {
        public static IServiceCollection RegisterApplicationServices (this IServiceCollection services)
        {
            services.AddScoped<IStationService, StationService>();
            services.AddScoped<IPointService, PointService>();
            services.AddScoped<IRouteService, RouteService>();
            services.AddScoped<ITravelService, TravelService>();
            services.AddScoped<IScheduledTravelService, ScheduledTravelService>();
            services.AddScoped<IRoutePointService, RoutePointService>();
            services.AddScoped<IRezervationService, RezervationService>();
     

            return services;

        }

    }
}
