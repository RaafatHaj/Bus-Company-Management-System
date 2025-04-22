using Microsoft.AspNetCore.Mvc;
using TravelCompany.Application.Services.Routes;

namespace Travel_Company_MVC.Controllers
{
    public class SchedulingController : Controller
    {


		private readonly IRouteService _routeService;

		public SchedulingController(IRouteService routeService)
		{
			_routeService = routeService;
		}

		public async Task< IActionResult> ScheduleNewTrips()
        {
			//var routes = await _routeService.GetAllRoutesAsync();

			return View();
		}


        //public IActionResult ScheduleNewTrips()
        //{
        //    return View();
        //}


    }
}
