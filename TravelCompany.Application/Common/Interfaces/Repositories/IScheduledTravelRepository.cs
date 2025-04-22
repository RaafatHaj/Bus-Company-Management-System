using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;

namespace TravelCompany.Application.Common.Interfaces.Repositories
{
    public interface IScheduledTravelRepository : IBaseRepository<Trip>
    {
        Task<IEnumerable<ScheduledTravelsMainViewDTO>> GetMainScheduledTravelsAsync();
        Task<IEnumerable<TravelStation>> GetScheduledTravelDetailsAsync(int scheduledTravelId);
        Task<IEnumerable<SuitableTravelDTO>> GetSuitableTravelsAsync(int stationAId, int StationBId);
        Task<IEnumerable<int>> GetAvaliableSeatsAsync(GetAvaliableSeatsDTO dto);
        Task<bool> SetStationStatusAsLeftAsync(int stationId, int shceduledTravelId);


        }
}
