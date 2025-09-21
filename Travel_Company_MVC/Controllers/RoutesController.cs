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
			//await Task.Delay(3000);

			var route = await _routeService.GetRouteStationsAsync(routeID);

            if (route is null)
                return NotFound();

            //      var routeModel = _mapper.Map<RouteStationsViewModel>(route);
            var routeModel = new RouteStationsViewModel
            {
                RouteId=route.RouteId,
                RouteName=route.RouteName,
                EstimatedDistance=route.EstimatedDistance,
                EstimatedTime=route.EstimatedTime,
                Stations=route.Stations,


            };


			return PartialView("_RouteStations", routeModel);
		}



  //      [HttpGet]
  //      public async Task<IActionResult> GetRouteStationsJsonAsync(int routeId)
  //      {
  //          var route = await _routeService.GetRouteStationsAsync(routeId);



  //          if (route == null)
  //              return NotFound();

  //          var filterdStations = route.Stations.Select(s => new
  //          {
  //              Value = s.StationId.ToString(),
  //              Text = s.StationName,
  //              stationOrder = s.StationOrder

  //          }).ToList();

  //          return Json(filterdStations);



		//}

        [HttpGet]
		public async Task<IActionResult> GetRouteStationsJsonAsync1(int routeId)
		{
			var route = await _routeService.GetRouteStationsAsync(routeId);

            

			if (route == null)
				return NotFound();

            var filterdStations = route.Stations.Select(s => new 
            {
                Value = s.StationId.ToString(),
                Text = s.StationName,
                stationOrder=s.StationOrder

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
