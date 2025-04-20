using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;

namespace TravelCompany.Application.Common.Interfaces.Repositories
{
    public interface ITravelRepository : IBaseRepository<Trip>
    {

        Task<bool> ScheculeTravelsAsync(TravelScheduleDTO schedule);
        Task<int> CreateTravelAsync(TravelScheduleDTO schedule);
        Task<Trip?> GetTravelAsync(TimeSpan travelTime, int routeId);
 

   //  Task<Travel> GetTravelAsync(TimeSpan travelTime, int routeId);



	}
}
