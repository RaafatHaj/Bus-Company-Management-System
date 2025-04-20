using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Application.Common.Interfaces;
using TravelCompany.Application.Common.Responses;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Application.Services.Travels
{
    internal class TravelService : ITravelService
    {


        private readonly IUnitOfWork _unitOfWork;

        public TravelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

	

		public async Task<Trip?> FindTravelAsync(TimeSpan travelTime, int routeId)
		{

			
			return await _unitOfWork.Travels.GetTravelAsync(travelTime, routeId);
		}

		public async Task<Trip?> FindTravelAsync(int travelId)
		{
			return await _unitOfWork.Travels.GetQueryable().FirstOrDefaultAsync(t => t.TripId == travelId);
		}

		public async Task<SchedulingResult> ScheduleNewTravelsAsync(TravelScheduleDTO schedule)
		{
			var result = new SchedulingResult();

			var travel=await FindTravelAsync(schedule.TravelTime,schedule.RouteId);

			if(travel == null)
			{
				schedule.TravelId = await _unitOfWork.Travels.CreateTravelAsync(schedule);

				if(schedule.TravelId > 0)
				{

				    result.IsSuccess = await _unitOfWork.Travels.ScheculeTravelsAsync(schedule);
					result.Status = "success";
					result.Message = "New travel has been creatd , and scheduling done successfully ";

					return result;
				}

		
			//}

			//if(schedule.StartingDate > travel!.ScheduleEndingDate)
			//{
			//	// edit scheduleType
			//	// remove Travel days from db
			//	// schedule new travel as previous one

			//	result.IsSuccess = true;

			//	result.Status = "success";
			//	result.Message = ", and scheduling done successfully ";

			//	return result;

			//}

			//if (schedule.StartingDate < travel!.ScheduleEndingDate)
			//{


			//	// split the ScheduledTravels into two group 

			//	// edit travel schedule type 
			//	// remove travel days and edit it if nesserry

			//	// Scedule travels after ScheduleEndingDate as normel as we do

			//	// coming to ScheduledTravels that have dates after schedule.StartingDate
			//	// asking user if they want to 
			//	//      1- remove it and schedule by new aprouche ( handle bookings )
			//	//      2- keep it and schedule new travels that not exist already

			//	result.IsSuccess = false;
			//	result.Status = "needMoreData";
			//	result.Message = ", and scheduling done successfully ";

			//	return result;

			}






			return result;
		}
	}
}
