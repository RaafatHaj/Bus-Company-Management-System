using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelCompany.Application.Services.Routes;

namespace Travel_Company_MVC.Controllers
{
    [Authorize]
    public class RoutesController : Controller
    {


        private readonly IRouteService _routeService;

        public RoutesController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        public async Task< IActionResult >Index(bool isFromScheduling =false)
        {

            var model = new RoutesViewModel();

			model.Routes= await _routeService.GetAllRoutesAsync();
            model.ISFromScheduling = isFromScheduling;



			return View(model);
        }


		[HttpGet]
		public async Task<IActionResult> GetRouteDetails(int routeID)
        {
            var details = await _routeService.GetRouteDetailsAsync(routeID);


			return PartialView("_RouteDetials",details);
		}



		[HttpGet]
		public async Task<IActionResult> GetAllRoutesAsync()
		{
			var routes = await _routeService.GetAllRoutesAsync();


			return PartialView("_RoutesList", routes);
		}

		[HttpGet]
        public IActionResult Create()
        {


            return View();
        }

        [HttpGet]
        public async Task<IActionResult> FetchAllRoutes()
        {

            var routes =await _routeService.GetAllRoutesAsync();

            return Json(routes);
        }
    }
}
