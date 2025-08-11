using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using TravelCompany.Application.Services.Recurrings;
using TravelCompany.Application.Services.Stations;
using TravelCompany.Application.Services.Travels;
using TravelCompany.Domain.Entities;

namespace Travel_Company_MVC.Controllers
{
	[Authorize]
    public class TripsController : Controller
    {

        private readonly IRecurringServcie _recurringService;
        private readonly ITripService _tripService;
        private readonly IMapper _mapper;
        private readonly IStationService _stationService;


		public TripsController(IRecurringServcie recurringService, ITripService tripService, IMapper mapper, IStationService stationService)
		{
			_recurringService = recurringService;
			_tripService = tripService;
			_mapper = mapper;
			_stationService = stationService;
		}

		[HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public  IActionResult ScheduledTrips()
        {

			

			//if (TempData["Trips"] is string tripsJson)
   //         {
   //             var tripsModel = JsonConvert.DeserializeObject<IEnumerable<ScheduledTripViewModel>>(tripsJson);
   //             return View(tripsModel);
   //         }


            //var tripsList = await _tripService.RetriveAllTripsAsync();
            //var trips = _setTripsModel(tripsList);
            var model = new ScheduledTripsSearchViewModel();

			return View(model); // fallback

        }




        //      [HttpPost]
        //      public async Task<IActionResult> FindScheduledTripsAsync( ScheduledTripsSearchViewModel model)
        //      {
        //	if (!ModelState.IsValid)
        //		return BadRequest();

        //	return View("~/Views/Trips/ScheduledTrips.cshtml");
        //}



        [HttpPost]
        public async Task<IActionResult> GetScheduledTrips(ScheduledTripsSearchViewModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            //var result = await _tripService.SearchForTrips(_mapper.Map<ScheduledTripsSearchDTO>(model));

            if (model.SearchType == TripSearchType.TripPatterns)
            {
                var patterns = await _tripService.GetTripsPatterns(model.RouteId);

                var resultModel = _mapper.Map<IEnumerable<TripsPatternViewModel>>(patterns);

                return PartialView("_TripsPatterns", resultModel);
            }
            else
            {
                var dto = _mapper.Map<ScheduledTripsSearchDTO>(model);

                var tripsList = await _tripService.GetScheduledTrips(dto);

                var trips = _setTripsModel(tripsList);

                return PartialView("_ScheduledTrips", trips);

            }

          
		}



		[HttpGet]
        public async Task<IActionResult> GetScheduledTrips(int routeId , TimeSpan time)
        {
            var tripsList = await _tripService.RetriveAllTripsAsync(routeId , time);

            var returnTripsIds=tripsList.Select(t=>t.ReturnTripId).ToList();

            var returnTripsList = await _tripService.RetriveAllTripsAsync(returnTripsIds);


            var trips = _setTripsModel(tripsList, returnTripsList);

            return PartialView("_ScheduledTrips", trips);
        }

        [HttpGet]
        public async Task<IActionResult> GetTripTimingDetails(int tripId)
        {

            var trip = await _tripService.FindTripByIdAsync(tripId);

            if (trip is null)
                return BadRequest();

            var returnTrip = await _tripService.FindReturnTripByMainTripIdAsync(trip.Id);


            var model = _setTripTimigDetailsModel(trip, returnTrip);


            return PartialView("_TripTimingDetails", model);

        }

        //[HttpPost]
        //public async Task<IActionResult> EditTripTiming(ScheduledTripViewModel model)
        //{

        //    //var trip = await _tripService.FindTripByIdAsync(model.TripId);

        //    //if(trip==null)
        //    //    return BadRequest();

        //    var result = await _tripService.EditTripTimeAsync(_mapper.Map<TripTimingDTO>(model));

        //    if (!result.Success)
        //        return BadRequest();


        //    model.TripId = result.Trip!.Id;
        //    model.Date = result.Trip.Date;
        //    model.Time = result.Trip.Time;


        //    if (model.ReturnTripId!=null)
        //    {

        //        var result2 = await _tripService.EditTripTimeAsync(new TripTimingDTO()
        //        {
        //            TripId = model.ReturnTripId!.Value,
        //            Date = model.ReturnDate!.Value,
        //            Time = model.ReturnTime!.Value


        //        });



        //        if (!result2.Success)
        //            return BadRequest();


        //        model.ReturnTripId = result2.Trip!.Id;
        //        model.ReturnDate   = result2.Trip.Date;
        //        model.ReturnTime   = result2.Trip.Time;

        //    }

        //    return PartialView("_ScheduledTripRow", model);

        //}


        [HttpGet]
        public async Task<IActionResult> GetTripsPatterns(int routeId)
        {

			var patterns = await _tripService.GetTripsPatterns(routeId);

			var model = _mapper.Map<IEnumerable<TripsPatternViewModel>>(patterns);

			return PartialView("_TripsPatterns" , model);
        }

        [HttpPost]
        public async Task<IActionResult> GetPatternWeeks([FromBody] PatternWeeksRequestDTO dto)
        {

            var weeks = await _tripService.GetPatternWeeksAsync(dto);

            return PartialView("_TripPatternWeeks", weeks);
        }

        [HttpGet]
        public async Task< IActionResult> TrackStationTripsIndex (int stationId=0)
        {
            var model = new TrackStationTripsViewModel();
			var stations = await _stationService.GetAllStationsAsync();

			if (stations == null)
				return NotFound();

			model.Stations = stations.Select(s => new SelectListItem()
			{
				Value = s.StationId.ToString(),
				Text = s.StationName
			}).ToList();

            if (stationId != 0)
                model.StationId = stationId;


			return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> TrackStationTrips(int StationId)
        {

            var result = await _tripService.GetStationTripSTrack(StationId);

            return PartialView("_StationTripsTrack" , result);

            //var stations = await _stationService.GetAllStationsAsync();

            //if (stations == null)
            //    return NotFound();

            //var stationsList = stations.Select(s => new SelectListItem()
            //{
            //    Value = s.StationId.ToString(),
            //    Text = s.StationName
            //}).ToList();




            //return View(stationsList);
        }


        /// Private Methods ////////////////////////////////////////////////////////////////////////////

        private ScheduledTripViewModel _setTripTimigDetailsModel(Trip mainTrip , Trip? returnTrip)
        {

            return new ScheduledTripViewModel()
            {
				RouteId = mainTrip.RouteId,
				TripId = mainTrip.Id,
                Date = mainTrip.Date,
                Time = mainTrip.Time,
                Status = mainTrip.status,

				ReturnRouteId = mainTrip.Route!.ReverseRouteId,
				ReturnTripId = returnTrip?.Id,
                ReturnDate = returnTrip?.Date,
                ReturnTime = returnTrip?.Time,
                ReturnStatus = returnTrip?.status


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
                        RouteId=trip.RouteId,
                        RouteName=trip.Route!.RouteName,
                        TripId = trip.Id,
                        Date = trip.Date,
                        Time = trip.Time,
                        Status = trip.status,
                        DepartureStationId = trip.Route!.FirstStationId,
                        TripTimeSpanInMInits = trip.Route!.EstimatedTime,

                        VehicleId = trip.TripAssignment?.VehicleId ,
                        VehicleNumber = trip.TripAssignment?.Vehicle?.VehicleNumber ,
                        VehicleModel = trip.TripAssignment?.Vehicle?.Type ,

                        ReturnRouteId=returnTrip?.RouteId,
                        ReturnTripId = returnTrip?.Id,
                        ReturnDate =  returnTrip?.Date,
                        ReturnTime =  returnTrip?.Time,
                        ReturnStatus = returnTrip?.status

                    });



                }
            

            return tripsModel;
        }

        private IEnumerable<ScheduledTripViewModel> _setTripsModel(IEnumerable<Trip> trips , IEnumerable<Trip> returnTripsList)
        {
            var tripsModel = new List<ScheduledTripViewModel>();

            //var mainTrips = trips.Where(t => t.MainTripId == null)
            //    .ToList();

            foreach (var trip in trips)
            {
                var returnTrip = returnTripsList.SingleOrDefault(t => t.MainTripId == trip.Id);

                tripsModel.Add(new()
                {
                    TripId = trip.Id,
                    Date = trip.Date,
                    Time = trip.Time,
                    Status = trip.status,
                    DepartureStationId = trip.Route!.FirstStationId,
                    TripTimeSpanInMInits = trip.Route!.EstimatedTime,
                    VehicleId = trip.TripAssignment?.VehicleId,
                    VehicleNumber = trip.TripAssignment?.Vehicle?.VehicleNumber,
                    VehicleModel = trip.TripAssignment?.Vehicle?.Type,

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
