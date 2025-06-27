using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using TravelCompany.Application.Services.Recurrings;
using TravelCompany.Application.Services.Travels;
using TravelCompany.Domain.DTOs;

namespace Travel_Company_MVC.Controllers
{
    [Authorize]
    public class TripsController : Controller
    {

        private readonly IRecurringServcie _recurringService;
        private readonly ITripService _tripService;
        private readonly IMapper _mapper;

        public TripsController(IRecurringServcie recurringService, ITripService tripService, IMapper mapper)
        {
            _recurringService = recurringService;
            _tripService = tripService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        //[HttpGet]
        //public async Task<IActionResult> GetRouteTripPattern(int routeId)
        //{

        //    return PartialView("_RouteTrips");
        //    //return PartialView("_RouteTrips",await _recurringService.GetRouteTripPattern(routeId));
        //}


        [HttpGet]
        public async Task<IActionResult> GetRouteTripsPatters(int routeId)
        {

            //var pattern = await _recurringService.GetRouteTripPattern(routeId);

            //return PartialView("Views/Routes/_RouteTrips.cshtml", pattern);
            return PartialView("Views/Routes/_RouteTrips.cshtml");

        }

        [HttpGet]
        public async Task< IActionResult> ScheduledTrips()
        {



            if (TempData["Trips"] is string tripsJson)
            {
                var tripsModel = JsonConvert.DeserializeObject<IEnumerable<ScheduledTripViewModel>>(tripsJson);
                return View(tripsModel);
            }


            var tripsList = await _tripService.RetriveAllTripsAsync();
            var trips = _setTripsModel(tripsList);

            return View(trips); // fallback

        }

        [HttpGet]
        public async Task<IActionResult> GetTripTimingDetails(int tripId)
        {

            var trip =await _tripService.FindTripByIdAsync(tripId);

            if (trip is null)
                return BadRequest();

            var returnTrip = await _tripService.FindReturnTripByMainTripIdAsync(trip.Id);


            var model = _setTripTimigDetailsModel(trip, returnTrip);


            return PartialView("_TripTimingDetails",model);

        }

        [HttpPost]
        public async Task<IActionResult> EditTripTiming(ScheduledTripViewModel model)
        {

            //var trip = await _tripService.FindTripByIdAsync(model.TripId);

            //if(trip==null)
            //    return BadRequest();

            var result = await _tripService.EditTripTimeAsync(_mapper.Map<TripTimingDTO>(model));

            if (!result.Success)
                return BadRequest();


            model.TripId = result.Trip!.Id;
            model.Date = result.Trip.Date;
            model.Time = result.Trip.Time;


            if (model.ReturnTripId!=null)
            {

                var result2 = await _tripService.EditTripTimeAsync(new TripTimingDTO()
                {
                    TripId = model.ReturnTripId!.Value,
                    Date = model.ReturnDate!.Value,
                    Time = model.ReturnTime!.Value


                });



                if (!result2.Success)
                    return BadRequest();


                model.ReturnTripId = result2.Trip!.Id;
                model.ReturnDate   = result2.Trip.Date;
                model.ReturnTime   = result2.Trip.Time;

            }

            return PartialView("_ScheduledTripRow", model);

        }

        private ScheduledTripViewModel _setTripTimigDetailsModel(Trip mainTrip , Trip? returnTrip)
        {
            if(returnTrip!=null)
            {
                return new ScheduledTripViewModel()
                {

                    TripId = mainTrip.Id,
                    Date = mainTrip.Date,
                    Time = mainTrip.Time,
                    Status = mainTrip.status,


                    ReturnTripId = returnTrip.Id,
                    ReturnDate = returnTrip.Date,
                    ReturnTime = returnTrip.Time,
                    ReturnStatus = returnTrip.status


                };
            }

            return new ScheduledTripViewModel()
            {

                TripId = mainTrip.Id,
                Date = mainTrip.Date,
                Time = mainTrip.Time,
                Status = mainTrip.status


            };



        }
        private IEnumerable<ScheduledTripViewModel> _setTripsModel(IEnumerable<Trip> trips)
        {
            var tripsModel = new List<ScheduledTripViewModel>();

            var mainTrips = trips.Where(t => t.MainTripId == null)
                .ToList();

            foreach (var trip in mainTrips)
            {
                var returnTrip = trips.SingleOrDefault(t => t.MainTripId == trip.Id);

                    tripsModel.Add(new()
                    {
                        TripId = trip.Id,
                        Date = trip.Date,
                        Time = trip.Time,
                        Status = trip.status,
                        DepartureStationId=trip.Route!.FirstStationId,
                        TripTimeSpanInMInits=trip.Route!.EstimatedTime,
                        VehicleId=trip.TripAssignment?.VehicleId,
                        VehicleNumber=trip.TripAssignment?.Vehicle?.VehicleNumber,
                        VehicleModel=trip.TripAssignment?.Vehicle?.Type,

                        ReturnTripId = returnTrip?.Id,
                        ReturnDate = returnTrip?.Date,
                        ReturnTime = returnTrip?.Time,
                        ReturnStatus = returnTrip?.status

                    });
           


            }

            return tripsModel;
        }


    }
}
