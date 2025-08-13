using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;

namespace TravelCompany.Application.Services.Vehicles
{
    public interface IVehicleService
    {

        Task<IEnumerable<AvailableTripVehicleDTO>> GetAvailableVehicles(int tripId);

        Task<(bool Success, string? Message, ScheduledTripDTO? Trip)> AssignVehicleToTripAsync(AssignVehicleDTO dto);
        Task<Vehicle?> FindVehicleAsync(int vehicleId);


	}
}
