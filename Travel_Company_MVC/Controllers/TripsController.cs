using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
        public async Task<IActionResult> GetPatternScheduledTrips(int routeId , TimeSpan time , DateTime startDate,DateTime endDate)
        {
            var trips=await _tripService.GetPatternScheduledTrips(routeId, time, startDate, endDate);


            var model = _setCustomTripsModel(trips);

            return PartialView("_CustomScheduledTrips", model);

        }


		[HttpGet]
        public async Task<IActionResult> GetScheduledTrips(int routeId , TimeSpan time)
        {
            var tripsList = await _tripService.RetriveAllTripsAsync(routeId , time);

            //var returnTripsIds=tripsList.Select(t=>t.ReturnTripId).ToList();

            //var returnTripsList = await _tripService.RetriveAllTripsAsync(returnTripsIds);


            var tripsModel = _setCustomTripsModel(tripsList);

            return PartialView("_CustomScheduledTrips", tripsModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetTripEditingForm(int tripId , bool isReturnTrip=false)
        {

            var trip = await _tripService.FindTripByIdAsync(tripId);

            if (trip is null)
                return BadRequest();

            if(isReturnTrip)
            {
                var mainTrip = await _tripService.FindMainTripByReturnTripIdAsync(tripId);
                var returnModel = _setTripEditingViewModle(mainTrip!, trip);
                return PartialView("_TripEditing", returnModel);
            }

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
           
            if(model.IsCustomTripRow)
            {
                if(model.IsReturnTrip)
                {
                    var returnTrip = _tripService.FindTripByIdAsync(result.Trip!.ReturnTripId!.Value);

                    var customReturnTripRow = new CustomScheduledTripViewModel
                    {
                        RouteId = returnTrip.Result!.RouteId,
                        RouteName = returnTrip.Result!.Route!.RouteName,

                        TripId = returnTrip.Result!.Id,
                        Date = returnTrip.Result!.Date,
                        Time = returnTrip.Result!.Time,
                        DepartureStationId = returnTrip.Result!.Route.FirstStationId,
                        TripTimeSpanInMInits = returnTrip.Result!.Route.EstimatedTime,
                        Status = returnTrip.Result!.status,

                        MainTripId = returnTrip.Result!.MainTripId,
                        ReturnTripId = returnTrip.Result!.ReturnTripId,


                        VehicleId = returnTrip.Result!.TripAssignment?.VehicleId,
                        VehicleNumber = returnTrip.Result!.TripAssignment?.Vehicle!.VehicleNumber,
                        VehicleModel = returnTrip.Result!.TripAssignment?.Vehicle!.Type

                    };

                    return PartialView("~/Views/Trips/_CustomScheduledTripRow.cshtml", customReturnTripRow);
                }

                var customTripRow = new CustomScheduledTripViewModel
                {
                    RouteId = result.Trip!.RouteId,
                    RouteName = result.Trip!.RouteName,

                    TripId = result.Trip!.TripId,
                    Date = result.Trip!.Date,
                    Time = result.Trip!.Time,
                    DepartureStationId = result.Trip!.DepartureStationId,
                    TripTimeSpanInMInits = result.Trip!.TripTimeSpanInMInits,
                    Status = result.Trip!.Status,

                    MainTripId = result.Trip.MainTripId,
                    ReturnTripId =result.Trip.ReturnTripId,


                    VehicleId = result.Trip!.VehicleId,
                    VehicleNumber = result.Trip!.VehicleNumber,
                    VehicleModel = result.Trip!.VehicleModel

                };

                return PartialView("~/Views/Trips/_CustomScheduledTripRow.cshtml", customTripRow);

            }

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
        public async Task< IActionResult >EditTripPage(int tripId, bool isReturnTrip = false)
        {

            var trip = await _tripService.FindTripByIdAsync(tripId);

            if (trip is null)
                return BadRequest();

            if (isReturnTrip)
            {
                var mainTrip = await _tripService.FindMainTripByReturnTripIdAsync(tripId);
                var returnModel = _setTripEditingViewModle(mainTrip!, trip);

                returnModel.IsReturnTrip = true;

                return View("~/Views/Trips/TripEditing.cshtml" , returnModel);
                //return PartialView("_TripEditing", returnModel);
            }

            var returnTrip = await _tripService.FindReturnTripByMainTripIdAsync(trip.Id);


            var model = _setTripEditingViewModle(trip, returnTrip);

            model.IsReturnTrip = false;

            return View("~/Views/Trips/TripEditing.cshtml", model);

            //return PartialView("_TripEditing", model);


            //return View("~/Views/Trips/TripEditing.cshtml");
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

            var model = new TripPatternWeeksViewModel {

                Weeks= weeks.ToList() ,
                RouteId=dto.RouteId,
                PatternTime=dto.Time
            
            };

            return PartialView("_TripPatternWeeks", model);
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
        private IEnumerable<ScheduledTripViewModel> _setTripsModel(IEnumerable<ScheduledTripBaseDTO> trips)
        {
            var tripsModel = new List<ScheduledTripViewModel>();

            var mainTrips = trips.Where(t => t.MainTripId == null)
                .ToList();
          
                foreach (var trip in mainTrips)
                {
                    var returnTrip = trips.SingleOrDefault(t => t.MainTripId == trip.TripId);

                    tripsModel.Add(new()
                    {
                        RouteId=trip.RouteId,
                        RouteName=trip.RouteName,
                        TripId = trip.TripId,
                        Date = trip.Date,
                        Time = trip.Time,
                        Status = trip.Status,
                        DepartureStationId = trip.DepartureStationId,
                        TripTimeSpanInMInits = trip.TripTimeSpanInMInits,
                        EstimatedDistance=trip.EstimatedDistance,

                        VehicleId = trip.VehicleId ,
                        VehicleNumber = trip.VehicleNumber ,
                        VehicleModel = trip.VehicleModel,
                        BookingCount=trip.BookingCount,


                        ReturnRouteId=returnTrip?.RouteId,
                        ReturnTripId = returnTrip?.TripId,
                        ReturnDate =  returnTrip?.Date,
                        ReturnTime =  returnTrip?.Time,
                        ReturnStatus = returnTrip?.Status,
                        ReturnBookingCount=returnTrip?.BookingCount
                        

                    });



                }
            

            return tripsModel;
        }

		private IEnumerable<CustomScheduledTripViewModel> _setCustomTripsModel(IEnumerable<Trip> trips)
		{
			var tripsModel = new List<CustomScheduledTripViewModel>();

            tripsModel = trips.Select(t => new CustomScheduledTripViewModel()
            {
				RouteId = t.RouteId,
				RouteName = t.Route!.RouteName,
				TripId = t.Id,
				Date = t.Date,
				Time = t.Time,
				Status = t.status,
				DepartureStationId = t.Route!.FirstStationId,
				TripTimeSpanInMInits = t.Route!.EstimatedTime,

                MainTripId=t.MainTripId,
                ReturnTripId=t.ReturnTripId,

				VehicleId = t.TripAssignment?.VehicleId,
				VehicleNumber = t.TripAssignment?.Vehicle?.VehicleNumber,
				VehicleModel = t.TripAssignment?.Vehicle?.Type,

				//ReturnRouteId = returnTrip?.RouteId,
				//ReturnTripId = returnTrip?.Id,
				//ReturnDate = returnTrip?.Date,
				//ReturnTime = returnTrip?.Time,
				//ReturnStatus = returnTrip?.status

			}).ToList();

			return tripsModel;

			//var mainTrips = trips.Where(t => t.MainTripId == null)
			//	.ToList();

			//foreach (var trip in mainTrips)
			//{
			//	var returnTrip = trips.SingleOrDefault(t => t.MainTripId == trip.Id);

			//	tripsModel.Add(new()
			//	{
			//		RouteId = trip.RouteId,
			//		RouteName = trip.Route!.RouteName,
			//		TripId = trip.Id,
			//		Date = trip.Date,
			//		Time = trip.Time,
			//		Status = trip.status,
			//		DepartureStationId = trip.Route!.FirstStationId,
			//		TripTimeSpanInMInits = trip.Route!.EstimatedTime,

			//		VehicleId = trip.TripAssignment?.VehicleId,
			//		VehicleNumber = trip.TripAssignment?.Vehicle?.VehicleNumber,
			//		VehicleModel = trip.TripAssignment?.Vehicle?.Type,

			//		ReturnRouteId = returnTrip?.RouteId,
			//		ReturnTripId = returnTrip?.Id,
			//		ReturnDate = returnTrip?.Date,
			//		ReturnTime = returnTrip?.Time,
			//		ReturnStatus = returnTrip?.status

			//	});



			//}



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
