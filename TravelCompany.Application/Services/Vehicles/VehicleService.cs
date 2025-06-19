using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Application.Common.Interfaces;
using TravelCompany.Application.Common.Interfaces.Repositories;
using TravelCompany.Application.Services.Travels;
using TravelCompany.Domain.Const;
using TravelCompany.Domain.DTOs;

namespace TravelCompany.Application.Services.Vehicles
{
    public class VehicleService : IVehicleService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ITripService _tripService;

        public VehicleService(IUnitOfWork unitOfWork, ITripService tripService)
        {
            _unitOfWork = unitOfWork;
            _tripService = tripService;
        }

        public async Task<(bool Success, string? Message, ScheduledTripDTO? Trip)> AssignVehicleToTripAsync(ScheduledTripDTO dto)
        {


            var mainTripDateTime=dto.Date+dto.Time;
            var returnTripDateTime = dto.ReturnDate + dto.ReturnTime;

            if (returnTripDateTime < mainTripDateTime.AddMinutes(dto.TripTimeSpanInMInits + AppConsts.MinBreak))
                return (false, "Return trip time should be after Main trip time plus Trip duration and Break minimum default dureation.", null);


            var resule = await _unitOfWork.Vehicles.SetVehicleForTrip(dto);

            if (!resule.Success)
                return (false, "Some thing went worn on data base level.", null);



            var mianTrip= await _tripService.FindTripByIdAsync(dto.TripId);
            var returnTrip = await _tripService.FindTripByIdAsync(resule.ReturnTrpId);

            if(mianTrip is null || returnTrip is null)
                return (false, "Some thing went worn on data base level.", null);



            var trip = new ScheduledTripDTO
            {

                TripId= mianTrip.Id,
                Date= mianTrip.Date,
                Time= mianTrip.Time,
                DepartureStationId= mianTrip.Route!.FirstStationId,
                Status= mianTrip.status,

                ReturnTripId=returnTrip.Id,
                ReturnTime=returnTrip.Time,
                ReturnDate= returnTrip.Date,
                ReturnStatus= returnTrip.status,

                VehicleId=dto.VehicleId
            };

            return (true, "Vehicle assigned successfully.", trip);


        }

        public async Task<IEnumerable<AvailableTripVehicleDTO>> GetAvailableVehicles(DateTime tripTime, int departureStationId, int tripSpanInMinits)
        {
            return await _unitOfWork.Vehicles.GetAvailableVehicles(tripTime, departureStationId, tripSpanInMinits);
        }
    }
}
