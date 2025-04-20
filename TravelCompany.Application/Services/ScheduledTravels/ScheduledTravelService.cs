using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Application.Common.Interfaces;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Application.Services.ScheduledTravels
{
    internal class ScheduledTravelService : IScheduledTravelService
    {

        private readonly IUnitOfWork _unitOfWork;
        

        public ScheduledTravelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ScheduledTravelsMainViewDTO>> RetriveMainScheduledTravelsAsync()
        {
           return await _unitOfWork.ScheduledTravels.GetMainScheduledTravelsAsync();
        }

        //public async Task<IEnumerable<ScheduledTravelDetail>> RetriveScheduledTravelDetailAsync(int scheduledTravelId)
        //{
        //    // return await _unitOfWork.ScheduledTravels.GetScheduledTravelDetailsAsync(scheduledTravelId);
        //    return await _unitOfWork.ScheduledTravelDetails.GetQueryable().Where(t=>t.ScheduledTravelId==scheduledTravelId)
                
        //        .ToListAsync();
           
        //}

        public void SetStartedTravels()
		{
			throw new NotImplementedException();
		}

        public async Task <IEnumerable<ScheduledTravelDetailDTO>> RetriveScheduledTravelDetailAsync(int scheduledTravelId)
        {
            return await _unitOfWork.TravelStations.GetQueryable().Where(t => t.ScheduledTravelId == scheduledTravelId)
                .Select(t=>new ScheduledTravelDetailDTO()
                {
                    ScheduledTravelId=t.ScheduledTravelId,
                    StationOrder=t.StationOrder,
                    StationId=t.StationId,
                    StationName=t.Station!.StationName,
                    Status=t.Status,
                    Date=t.ArrvalDateAndTime.Date,
                    ArrivalTime=t.ArrvalDateAndTime.TimeOfDay,
                    AvailableSeats=t.AvailableSeats,
                    BookedSeates=t.BookedSeates
                })
              .ToListAsync();
        }

        public async Task<IEnumerable<SuitableTravelDTO>> FindSuitableTravelsAsync(int stationAId, int StationBId)
        {
            return await _unitOfWork.ScheduledTravels.GetSuitableTravelsAsync(stationAId, StationBId);
        }

        public async Task<IEnumerable<int>> GetAvaliableSeats(GetAvaliableSeatsDTO dto)
        {
            return await _unitOfWork.ScheduledTravels.GetAvaliableSeatsAsync(dto);
        }


        //public async Task<bool> SetStationStatusAsLeftAsync(int stationId,int scheduledTravelId)
        //{
        //    return await _unitOfWork.ScheduledTravels.SetStationStatusAsLeftAsync(stationId, scheduledTravelId);
        //}

        public async Task<bool> SetStationStatusAsLeftAsync(int stationId, int scheduledTravelId)
        {

            var travel=await _unitOfWork.TravelStations.GetQueryable()
                .SingleOrDefaultAsync(t => t.StationId == stationId && t.ScheduledTravelId == scheduledTravelId);

            if(travel==null)
                return false;

            travel.Status =travel.Status == StationStatus.Pending ? StationStatus.Left : StationStatus.Pending;

            _unitOfWork.Save();

            return true;


            
            
        }
    }
}
