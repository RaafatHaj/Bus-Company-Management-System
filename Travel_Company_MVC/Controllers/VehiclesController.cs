using Microsoft.AspNetCore.Mvc;
using TravelCompany.Application.Services.Vehicles;

namespace Travel_Company_MVC.Controllers
{
    public class VehiclesController : Controller
    {

        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/Vehicles/GetAvailableVehicles")]
        public async Task<IActionResult> GetAvailableVehicles(DateTime tripTime, int departureStationId, int tripSpanInMinits)
        {

            var vehicles = await _vehicleService.GetAvailableVehicles(tripTime, departureStationId, tripSpanInMinits);

            return PartialView("_AvailableVehiclesForTrip", vehicles);
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicleAvalilbilty()
        {

            return PartialView("_VahicleAvalibility");
        }

    }
}
