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
    internal class TripService : ITripService
    {


        private readonly IUnitOfWork _unitOfWork;
		

        public TripService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<Trip?> FindTripByIdAsync(int tripId)
		{


			//var query=  _unitOfWork.Trips.GetQueryable()
   //                      .Include(t => t.Route).ThenInclude(r => r.FirstStation)
   //                      .Include(t => t.Route).ThenInclude(r => r.LastStation)
   //                      .Include(t => t.TripAssignment).ThenInclude(a => a.Vehicle)
			//			 .AsNoTracking().SingleOrDefaultAsync();

			//if (asNoTracking)
			//	query.AsNoTracking();



			//return await query.SingleOrDefaultAsync();
			return await _unitOfWork.Trips.GetQueryable()
                         .Include(t => t.Route).ThenInclude(r => r.FirstStation)
                         .Include(t => t.Route).ThenInclude(r => r.LastStation)
                         .Include(t => t.TripAssignment).ThenInclude(a => a.Vehicle)
                         .AsNoTracking().SingleOrDefaultAsync(t=>t.Id==tripId);
        }



        public async Task<Trip?> FindReturnTripByMainTripIdAsync(int mainTripId)
        {
            return await _unitOfWork.Trips.GetQueryable().SingleOrDefaultAsync(t => t.MainTripId==mainTripId);

        }

		public async Task<(bool Success , Trip? Trip)> EditTripTimeAsync(TripTimingDTO dto)
		{

			var trip = await FindTripByIdAsync(dto.TripId);

			if(trip==null)
				return (false, null);


			trip.Date= dto.Date;
			trip.Time= dto.Time;

			await _unitOfWork.SaveAsync();

			return (true, trip);

        }


        public async Task<(bool Success, Trip? Trip)> AddNewTripAsync(TripTimingDTO dto)
        {

            var trip = await FindTripByIdAsync(dto.TripId);

            if (trip != null)
                return (false, trip);

			//var newTrip = await _unitOfWork.Trips.GetQueryable().add


            trip.Date = dto.Date;
            trip.Time = dto.Time;

            await _unitOfWork.SaveAsync();

            return (true, trip);

        }



        public async Task<IEnumerable<Trip>> RetriveAllTripsAsync()
		{
			return await _unitOfWork.Trips.GetQueryable()
				.Include(t => t.Route)
				.Include(t=>t.TripAssignment).ThenInclude(a=>a.Vehicle).AsNoTracking()
				.ToListAsync();
			//return await _unitOfWork.Trips.GetAllTripsAsync();
		}

		public async Task<(bool Success, IEnumerable<ScheduledTripBaseDTO>? Data)> ScheduleTripsAsync(ScheduleDTO schedule)
		{

			if(schedule.RecurringPattern==RecurringType.Daily)
			{

				var scheduledTrips= await _unitOfWork.Trips.ScheduleTripsForEverySingleDayAsync(schedule);

				if(scheduledTrips!=null)
					return (true, scheduledTrips);
				else
					return (false, null);

		
			}
			else if (schedule.RecurringPattern==RecurringType.Weekly)
			{

				//var scheduledTrips = await _unitOfWork.Trips.ScheduleTripsForDaysInWeekAsync(schedule);

				//if (scheduledTrips != null)
				//	return (true, scheduledTrips);
				//else
					return (false, null);
			}
			else
			{

				//var scheduledTrips = await _unitOfWork.Trips.ScheduleTripsForSpecificDatesAsync(schedule);

				//if (scheduledTrips != null)
				//	return (true, scheduledTrips);
				//else
					return (false, null);

			}

		}

		//public async Task<SchedulingResult> ScheduleNewTravelsAsync(ScheduleDTO schedule)
		//{
		//	var result = new SchedulingResult();

		//	//var travel=await FindTravelAsync(schedule.TravelTime,schedule.RouteId);

		//	//if(travel == null)
		//	//{
		//	//	//schedule.TravelId = await _unitOfWork.Travels.CreateTravelAsync(schedule);

		//	//	if(schedule.TravelId > 0)
		//	//	{

		//	//	    //result.IsSuccess = await _unitOfWork.Travels.ScheculeTravelsAsync(schedule);
		//	//		result.Status = "success";
		//	//		result.Message = "New travel has been creatd , and scheduling done successfully ";

		//	//		return result;
		//	//	}

		
		//	////}

		//	////if(schedule.StartingDate > travel!.ScheduleEndingDate)
		//	////{
		//	////	// edit scheduleType
		//	////	// remove Travel days from db
		//	////	// schedule new travel as previous one

		//	////	result.IsSuccess = true;

		//	////	result.Status = "success";
		//	////	result.Message = ", and scheduling done successfully ";

		//	////	return result;

		//	////}

		//	////if (schedule.StartingDate < travel!.ScheduleEndingDate)
		//	////{


		//	////	// split the ScheduledTravels into two group 

		//	////	// edit travel schedule type 
		//	////	// remove travel days and edit it if nesserry

		//	////	// Scedule travels after ScheduleEndingDate as normel as we do

		//	////	// coming to ScheduledTravels that have dates after schedule.StartingDate
		//	////	// asking user if they want to 
		//	////	//      1- remove it and schedule by new aprouche ( handle bookings )
		//	////	//      2- keep it and schedule new travels that not exist already

		//	////	result.IsSuccess = false;
		//	////	result.Status = "needMoreData";
		//	////	result.Message = ", and scheduling done successfully ";

		//	////	return result;

		//	//}






		//	return result;
		//}
	}
}
