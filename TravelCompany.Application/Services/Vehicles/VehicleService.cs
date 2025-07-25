using TravelCompany.Application.Common.Interfaces;
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

        public async Task<(bool Success, string? Message, ScheduledTripDTO? Trip)> AssignVehicleToTripAsync(AssignVehicleDTO dto)
        {


            //var mainTripDateTime=dto.Date+dto.Time;
            //var returnTripDateTime = dto.ReturnDate + dto.ReturnTime;

            if (dto.ReturnTripNewDateTime < dto.MainTripNewDateTime.AddMinutes(dto.TripTimeSpanInMInits + AppConsts.MinBreak))
                return (false, "Return trip time should be after Main trip time plus Trip duration and Break minimum default dureation.", null);


            var resule = await _unitOfWork.Vehicles.SetVehicleForTrip(dto);

            if (!resule.Success)
                return (false, resule.ErrorMessage, null);



            var mainTrip= await _tripService.FindTripByIdAsync(dto.TripId);
            var returnTrip = await _tripService.FindTripByIdAsync(resule.ReturnTrpId);

            if(mainTrip is null || returnTrip is null)
                return (false, "Some thing went worng on data base level.", null);



            var trip = new ScheduledTripDTO
            {

                TripId = mainTrip.Id,
                Date = mainTrip.Date,
                Time = mainTrip.Time,
                DepartureStationId = mainTrip.Route!.FirstStationId,
                TripTimeSpanInMInits = mainTrip.Route.EstimatedTime,
                Status = mainTrip.status,
                MainTripId = mainTrip.MainTripId,


                VehicleId = mainTrip.TripAssignment?.VehicleId,
                VehicleNUmber = mainTrip.TripAssignment?.Vehicle?.VehicleNumber,
                VehicleModel=mainTrip.TripAssignment?.Vehicle?.Type,

                RouteId=mainTrip.RouteId,
                Seats=mainTrip.Seats,
                HasBookedSeat=mainTrip.HasBookedSeat,
                ArrivedStationOrder=mainTrip.ArrivedStationOrder,


                ReturnTripId =returnTrip?.Id,
                ReturnTime=returnTrip?.Time,
                ReturnDate= returnTrip?.Date,
                ReturnStatus= returnTrip?.status,

            };

            return (true, "Vehicle assigned successfully.", trip);


        }

        public async Task<IEnumerable<AvailableTripVehicleDTO>> GetAvailableVehicles(int tripId)
        {
            return await _unitOfWork.Vehicles.GetAvailableVehicles(tripId);
        }
    }
}
