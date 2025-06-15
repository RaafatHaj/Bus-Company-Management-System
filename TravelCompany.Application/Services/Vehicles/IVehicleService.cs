using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.DTOs;

namespace TravelCompany.Application.Services.Vehicles
{
    public interface IVehicleService
    {

        Task<IEnumerable<AvailableTripVehicleDTO>> GetAvailableVehicles(DateTime tripTime, int departureStationId, int tripSpanInMinits);

    }
}
