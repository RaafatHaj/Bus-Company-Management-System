using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelCompany.Application.Services.Recurrings;

namespace Travel_Company_MVC.Controllers
{
    [Authorize]
    public class TripsController : Controller
    {

        private readonly IRecurringServcie _recurringService;

        public TripsController(IRecurringServcie recurringService)
        {
            _recurringService = recurringService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetRouteTripPattern(int routeId)
        {

            return PartialView("_RouteTrips",await _recurringService.GetRouteTripPattern(routeId));
        }


        [HttpGet]
        public async Task<IActionResult> GetRouteTripsPatters(int routeId)
        {

            var pattern = await _recurringService.GetRouteTripPattern(routeId);

            return PartialView("Views/Routes/_RouteTrips.cshtml", pattern);

        }

        [HttpGet]
        public IActionResult ScheduledTrips()
        {
            return View();
        }
    }
}
