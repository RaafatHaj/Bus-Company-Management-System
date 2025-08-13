using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;

namespace TravelCompany.Application.Services.Stations
{
    public interface IStationService
    {

        Task<IEnumerable<Station>> GetAllStationsAsync();
        Task<(bool Success, StationTrackDTO Data)> SetStationAsArrived(int tripId, int stationId, int stationOrder);
        Task<(bool Success, StationTrackDTO Data)> SetStationAsMoved(int tripId, int stationId, int stationOrder);

	}
}
