using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Application.Common.Interfaces;
using TravelCompany.Application.Common.Interfaces.Repositories;
using TravelCompany.Domain.DTOs;

namespace TravelCompany.Application.Services.Vehicles
{
    public class VehicleService : IVehicleService
    {

        private readonly IUnitOfWork _unitOfWork;

        public VehicleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AvailableTripVehicleDTO>> GetAvailableVehicles(DateTime tripTime, int departureStationId, int tripSpanInMinits)
        {
            return await _unitOfWork.Vehicles.GetAvailableVehicles(tripTime, departureStationId, tripSpanInMinits);
        }
    }
}
