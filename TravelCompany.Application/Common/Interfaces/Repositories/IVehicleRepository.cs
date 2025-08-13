using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;

namespace TravelCompany.Application.Common.Interfaces.Repositories
{
    public interface IVehicleRepository : IBaseRepository<Vehicle>
	{

        Task<IEnumerable<AvailableTripVehicleDTO>> GetAvailableVehicles(int tripId);
        Task<(bool Success, int ReturnTrpId, string ErrorMessage)> SetVehicleForTrip(AssignVehicleDTO dto);

    }
}
