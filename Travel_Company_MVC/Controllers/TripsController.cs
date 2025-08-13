using AutoMapper;
using Humanizer;
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
        public async Task<IActionResult> GetTripEditingForm(int tripId)
        {

            var trip = await _tripService.FindTripByIdAsync(tripId);

            if (trip is null)
                return BadRequest();

            var returnTrip = await _tripService.FindReturnTripByMainTripIdAsync(trip.Id);


            var model = _setTripEditingViewModle(trip, returnTrip);


            return PartialView("_TripEditing", model);

        }

        [HttpPost]
        public async Task<IActionResult> EditTrip(EditScheduledTripViewModle model)
        {


			var result = await _tripService.EditTrip(_mapper.Map<EditScheduledTripDTO>(model));

			if (!result.Success)
				return BadRequest(new { errorMessage = result.Message });


			var editedTrip = new ScheduledTripViewModel
			{


				RouteId = result.Trip!.RouteId,
				RouteName = result.Trip!.RouteName,

				TripId = result.Trip!.TripId,
				Date = result.Trip!.Date,
				Time = result.Trip!.Time,
				DepartureStationId = result.Trip!.DepartureStationId,
				TripTimeSpanInMInits = result.Trip!.TripTimeSpanInMInits,
				Status = result.Trip!.Status,

                ReturnRouteId= result.Trip!.ReturnRouteId,
				ReturnTripId = result.Trip!.ReturnTripId,
				ReturnDate = result.Trip!.ReturnDate,
				ReturnTime = result.Trip!.ReturnTime,
				ReturnStatus = result.Trip!.ReturnStatus,

				VehicleId = result.Trip!.VehicleId,
				VehicleNumber = result.Trip!.VehicleNumber,
				VehicleModel = result.Trip!.VehicleModel

			};


			return PartialView("~/Views/Trips/_ScheduledTripRow.cshtml", editedTrip);

			
        }


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


        [HttpGet]
        public async Task<IActionResult> GetTripTrack(int tripId)
        {
            var track = await _tripService.GetTripTrackAsync(tripId);

            return PartialView("_TripTrack", _mapper.Map<IEnumerable<TripTrackViewModel>>(track));

        }

  

		/// Private Methods ////////////////////////////////////////////////////////////////////////////

		private EditScheduledTripViewModle _setTripEditingViewModle(Trip mainTrip , Trip? returnTrip)
        {
            // convert it to EditScheduledTripViewModle 
            return new EditScheduledTripViewModle()
            {
				RouteId = mainTrip.RouteId,
				TripId = mainTrip.Id,
                MainTripOldDateTime=mainTrip.Date.Add(mainTrip.Time),
                MainTripHasBookedSeats=mainTrip.HasBookedSeat,
                MainTripStationStopMinutes=mainTrip.StationStopMinutes,
                MainTripHasBreak=mainTrip.HasBreak,
                MainTripBreakMinutes=mainTrip.BreakMinutes,
                MainTripStationOrderNextToBreak=mainTrip.StationOrderNextToBreak,

               


				ReturnRouteId = mainTrip.Route!.ReverseRouteId,
				ReturnTripId = returnTrip?.Id,
                ReturnTripOldDateTime=returnTrip?.Date.Add(returnTrip.Time),
                ReturnTripHasBookedSeats=returnTrip==null?false :returnTrip!.HasBookedSeat,
				ReturnTripStationStopMinutes = returnTrip==null?0:returnTrip!.StationStopMinutes,
				ReturnTripHasBreak = returnTrip == null ? false : returnTrip!.HasBreak,
				ReturnTripBreakMinutes = returnTrip?.BreakMinutes,
				ReturnTripStationOrderNextToBreak = returnTrip?.StationOrderNextToBreak,


				VehicleId =mainTrip.TripAssignment?.VehicleId


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
