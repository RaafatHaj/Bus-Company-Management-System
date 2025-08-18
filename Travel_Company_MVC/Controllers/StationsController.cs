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


		[HttpPost]
		public async Task<IActionResult> MarkStationAsArrived(int tripId, int stationId, int stationOrder)
		{

            var result=await _stationService.SetStationAsArrived(tripId, stationId, stationOrder);

			if (!result.Success)
				return BadRequest(new { errorMessage = result.Message });



			return PartialView("_StationsTripsTrackRow", result.Data);
		}

		[HttpPost]
		public async Task<IActionResult> MarkStationAsMoved(int tripId, int stationId, int stationOrder)
		{
			var result = await _stationService.SetStationAsMoved(tripId, stationId, stationOrder);

			if (!result.Success)
				return BadRequest(new { errorMessage = result.Message });


			return PartialView("_StationsTripsTrackRow", result.Data);
		}




	}
}
