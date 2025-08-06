using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;

namespace TravelCompany.Application.Services.ScheduledTravels
{
    public interface IScheduledTravelService
    {
        void SetStartedTravels();

        Task<IEnumerable<ScheduledTravelsMainViewDTO>> RetriveMainScheduledTravelsAsync();
        Task<IEnumerable<ScheduledTravelDetailDTO>> RetriveScheduledTravelDetailAsync(int scheduledTravelId);
        Task<IEnumerable<SuitableTripDTO>> FindSuitableTravelsAsync(int stationAId, int stationBId, DateTime tipDate);

        Task<bool> SetStationStatusAsLeftAsync(int stationId, int scheduledTravelId);

    }
}
