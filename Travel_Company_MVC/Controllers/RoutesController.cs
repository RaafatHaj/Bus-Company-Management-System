using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelCompany.Application.Services.Routes;

namespace Travel_Company_MVC.Controllers
{
    [Authorize]
    public class RoutesController : Controller
    {


        private readonly IRouteService _routeService;
        private readonly IMapper _mapper;


        public RoutesController(IRouteService routeService, IMapper mapper)
        {
            _routeService = routeService;
            _mapper = mapper;
        }

        public async Task<IActionResult>Index(bool isFromScheduling =false)
        {



            var model = new RoutesViewModel();

			model.Routes= await _routeService.GetAllRoutesAsync();
            model.ISFromScheduling = isFromScheduling;



			return View(model);
        }

        [HttpGet]
        public IActionResult Create ()
        {
            return View();
        }

		[HttpGet]
		public async Task<IActionResult> GetRouteStations(int routeID)
        {
            var details = await _routeService.GetRouteStationsAsync(routeID);


			return PartialView("_RouteStations", details);
		}



        [HttpGet]
        public async Task<IActionResult> GetRouteStationsJsonAsync(int routeId)
        {
            var stations = await _routeService.GetRouteStationsAsync(routeId);

            if (stations == null)
                return NotFound();

            var filterdStations = stations.Select(s => new SelectListItem
            {
                Value=s.StationId.ToString(),
                Text=s.Station!.StationName

            });


            return Json(filterdStations);



		}

        [HttpGet]
		public async Task<IActionResult> GetRouteStationsJsonAsync1(int routeId)
		{
			var stations = await _routeService.GetRouteStationsAsync(routeId);

			if (stations == null)
				return NotFound();

            var filterdStations = stations.Select(s => new 
            {
                Value = s.StationId.ToString(),
                Text = s.Point.Station.StationName,
                stationOrder=s.PointOrder

            }).ToList();


			return Json(filterdStations);



		}

		[HttpGet]
        public async Task<IActionResult> GetRouteDetails(int routeId)
        {
            var route = await _routeService.GetRouteDetails(routeId);

            if (route is null)
                return BadRequest();


            return PartialView("_RouteDetails", _mapper.Map<RouteViewModel>(route));



        }

        [HttpGet]
        public async Task<IActionResult> GetRoutesAsync()
        {
            var model = new RoutesViewModel();

            model.Routes = await _routeService.GetAllRoutesAsync();
            model.ISFromScheduling = false;



            return PartialView("_RoutesList", model);
		}



        [HttpGet]
        public async Task<IActionResult> FetchAllRoutes()
        {

			//var routes = await _routeService.GetAllRoutesAsync();

			var model = new RoutesViewModel();

			model.Routes = await _routeService.GetAllRoutesAsync();
			model.ISFromScheduling = true;


			return PartialView("_RoutesList", model);

			//var routes =await _routeService.GetAllRoutesAsync();

   //         return Json(routes);
        }


        [HttpGet]
        public IActionResult RouteTrips(int routeId=1)
        {

            return View("RouteTrips");
        }


    }
}
