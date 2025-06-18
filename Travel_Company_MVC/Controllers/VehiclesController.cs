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
        public async Task<IActionResult> GetAvailableVehicles(DateTime tripTime, int departureStationId, int tripSpanInMinits , int tripId)
        {

            TempData["TripDateAndTime"] = tripTime;
            TempData["TripSpanInMinits"] = tripSpanInMinits;
            TempData["TripId"] = tripId;

            var vehicles = await _vehicleService.GetAvailableVehicles(tripTime, departureStationId, tripSpanInMinits);

            return PartialView("_AvailableVehiclesForTrip", vehicles);
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





     
            model.MainTripStartDateAndTime = model.AvalibilityStartDateAndTime is not null ? model.AvalibilityStartDateAndTime.Value : DateTime.Now;

            model.ReturnTripStartDateAndTime = model.MainTripStartDateAndTime?.AddMinutes(model.TripTimeSpanInMinits.Value);

            if (model.AvalibilityEndDateAndTime is not null)
            {
                model.MainTripEndDateAndTime = model.AvalibilityEndDateAndTime?.AddMinutes(model.TripTimeSpanInMinits.Value * -2);
                model.ReturnTripEndDateAndTime = model.MainTripEndDateAndTime?.AddMinutes(model.TripTimeSpanInMinits.Value * -1);
            }
            else
            {
                model.MainTripEndDateAndTime = null;
                model.ReturnTripEndDateAndTime = null;
            }

			//if (TempData["TripDateAndTime"] is not null )
   //         {
   //             var tripTime = Convert.ToDateTime(TempData["TripDateAndTime"]);
   //             var tripSpanInMinits = Convert.ToInt32(TempData["TripSpanInMinits"]);
   //             var tripId = Convert.ToInt32(TempData["TripId"]);

   //             trip = await _tripService.FindTripByIdAsync(tripId);


   //         }
   //         TempData["TripId"] = trip?.Id;
   //         TempData["DepartureStationName"] = trip?.Route!.FirstStation!.StationName;
   //         TempData["DistinationStationName"] = trip?.Route!.LastStation!.StationName;


			return PartialView("_VahicleAvalibility" , model);
        }

        [HttpPost]

        public async Task<IActionResult> AssignVehicleToTrip( VehicleAvalibilityViewModel model)
        {

            var dto = new AssignVehicleDTO();

            dto.TripId=model.TripId;
            dto.VehicleId = model.VehicleId;
            dto.MainTripDateTime = model.MainTripDate!.Value + model.MainTripTime!.Value;
            dto.ReturnTripDateTime = model.ReturnTripDate!.Value + model.ReturnTripTime!.Value;



            return Ok();

        }

    }
}
