using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TravelCompany.Application.Services.Travels;
using TravelCompany.Application.Services.Vehicles;
using TravelCompany.Domain.Entities;

namespace Travel_Company_MVC.Controllers
{
    public class VehiclesController : Controller
    {

        private readonly IVehicleService _vehicleService;
        private readonly ITripService _tripService;

        public VehiclesController(IVehicleService vehicleService, ITripService tripService)
        {
            _vehicleService = vehicleService;
            _tripService = tripService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/Vehicles/GetAvailableVehicles")]
        public async Task<IActionResult> GetAvailableVehicles( int tripId)
        {

            //TempData["TripDateAndTime"] = tripDateTime;
            //TempData["ReturnTripDateTime"] = returnTripDateTime;
            TempData["TripId"] = tripId;

            var vehicles = await _vehicleService.GetAvailableVehicles(tripId);

            return PartialView("_AvailableVehiclesForTrip", vehicles);
        }


        [HttpPost]
        public async Task<IActionResult> GetVehicleDetails([FromBody] VehicleDetailsViewModel model)
        {
            if (model.VehicleModel != null)
                return PartialView("_VehicleDetails", model);


            var vehicle = await _vehicleService.FindVehicleAsync(model.VehicleId);

            if(vehicle == null)
				return PartialView("_VehicleDetails", model);

            var veiwModel = new VehicleDetailsViewModel
            {
                VehicleId=vehicle.VehicleId,
                TripId=model.TripId,
                VehicleModel=vehicle.Type,
                VehicleNumber=vehicle.VehicleNumber,
                Seats=vehicle.Seats,
                HomeStation=vehicle.Station?.StationName


            };
			return PartialView("_VehicleDetails", veiwModel);
		}


        [HttpPost]
        public async Task<IActionResult> GetVehicleAvalilbilty([FromBody] VehicleAvalibilityViewModel model)
		{

     
            var trip = await _tripService.FindTripByIdAsync(model.TripId);

            if (trip is null)
                return BadRequest();

            model.DepartureStationName= trip.Route!.FirstStation!.StationName;
            model.DistinationStationName= trip.Route!.LastStation!.StationName;
            model.TripTimeSpanInMinits = trip.Route.EstimatedTime;



			return PartialView("_VahicleAvalibility" , model);
        }

        [HttpPost]

        public async Task<IActionResult> AssignVehicleToTrip(VehicleAvalibilityViewModel model)
        {

            var trip = await _tripService.FindTripByIdAsync(model.TripId);

            if (trip == null)
                return BadRequest(new { errorTitle = "Trip Not Found!", errorMessage = "There is no trip found , refresh the page and try again." });


            var dto = new AssignVehicleDTO
            {
                TripId=model.TripId,
                VehicleId=model.VehicleId,
                MainTripDateTime=model.MainTripDateTime,
                ReturnTripDateTime=model.ReturnTripDateTime,
                MainTripNewDateTime=model.MainTripNewDate.Add(model.MainTripNewTime),
                ReturnTripNewDateTime=model.ReturnTripNewDate.Add(model.ReturnTripNewTime),
                TripTimeSpanInMInits=model.TripTimeSpanInMinits
            
           
            };

      

            var result=await _vehicleService.AssignVehicleToTripAsync(dto);

            if(!result.Success)
                return BadRequest(new { errorMessage = result.Message });


            var assignedTrip = new ScheduledTripViewModel
            {
                TripId=result.Trip!.TripId,
                Date = result.Trip!.Date,
                Time = result.Trip!.Time,
                DepartureStationId= result.Trip!.DepartureStationId,
                TripTimeSpanInMInits= result.Trip!.TripTimeSpanInMInits,
                Status= result.Trip!.Status,

                ReturnTripId=result.Trip!.ReturnTripId,
                ReturnDate=result.Trip!.ReturnDate,
                ReturnTime=result.Trip!.ReturnTime,
                ReturnStatus=result.Trip!.ReturnStatus,

                VehicleId= result.Trip!.VehicleId,
                VehicleNumber= result.Trip!.VehicleNUmber,
                VehicleModel= result.Trip!.VehicleModel

            };

            //var dto = new AssignVehicleDTO();

            //dto.TripId=model.TripId;
            //dto.VehicleId = model.VehicleId;
            //dto.MainTripDateTime = model.MainTripDate!.Value + model.MainTripTime!.Value;
            //dto.ReturnTripDateTime = model.ReturnTripDate!.Value + model.ReturnTripTime!.Value;



            return PartialView("~/Views/Trips/_ScheduledTripRow.cshtml", assignedTrip);

        }

    }
}
