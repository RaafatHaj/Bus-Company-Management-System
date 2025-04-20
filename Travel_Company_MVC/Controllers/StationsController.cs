using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelCompany.Application.Services.Stations;

namespace Travel_Company_MVC.Controllers
{
    [Authorize]
    public class StationsController : Controller
    {

        private readonly IStationService _stationService;

        public StationsController(IStationService stationService)
        {
            _stationService = stationService;
        }

        [HttpGet]
        public async Task<IActionResult> FetchAllStations()
        {

            var stations = await _stationService.GetAllStationsAsync();

            return Json(stations);
        }
    }
}
