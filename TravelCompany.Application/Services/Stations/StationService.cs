using Microsoft.EntityFrameworkCore;
using TravelCompany.Application.Common.Interfaces;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;

namespace TravelCompany.Application.Services.Stations
{
    public class StationService : IStationService
    {

        private readonly IUnitOfWork _unitOfWork;

        public StationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Station>> GetAllStationsAsync()
        {
            return await _unitOfWork.Stations.GetQueryable().ToListAsync();
        }


		public async Task<(bool Success, StationTrackDTO Data)> SetStationAsArrived(int tripId, int stationId, int stationOrder)
		{
			return await _unitOfWork.Stations.SetStationAsArrived(tripId, stationId, stationOrder); 
		}

		public async Task<(bool Success, StationTrackDTO Data)> SetStationAsMoved(int tripId, int stationId, int stationOrder)
		{
			return await _unitOfWork.Stations.SetStationAsMoved(tripId, stationId, stationOrder);
		}
	}
}
