using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.DTOs;

namespace TravelCompany.Application.Common.Interfaces.Repositories
{
    public interface IVehicleRepository
    {

        Task<IEnumerable<AvailableTripVehicleDTO>> GetAvailableVehicles(DateTime tripTime, int departureStationId, int tripSpanInMinits);
        Task<(bool Success, int ReturnTrpId, string ErrorMessage)> SetVehicleForTrip(ScheduledTripDTO dto);

    }
}
