using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Mono.TextTemplating;
using Newtonsoft.Json;
using TravelCompany.Application.Services.Routes;
using TravelCompany.Application.Services.Travels;
using TravelCompany.Domain.Const;
using TravelCompany.Domain.DTOs;

namespace Travel_Company_MVC.Controllers
{
    public class SchedulingController : Controller
    {


		private readonly IRouteService _routeService;
		private readonly ITripService _tripService;
		private readonly IMapper _mapper;

		public SchedulingController(IRouteService routeService, IMapper mapper, ITripService tripService)
		{
			_routeService = routeService;
			_mapper = mapper;
			_tripService = tripService;
		}

		[HttpGet]
		public  IActionResult ScheduleNewTrips(int routeId=0 ,TimeSpan? time =null )
        {
            var breadcrumb = new List<string>
                            {
                                "Trips",
                                "Scheduling"
                            };

            ViewData["Breadcrumb"] = breadcrumb;
			//var routes = await _routeService.GetAllRoutesAsync();

			var model = _populateModel();


			model.RouteId = routeId;
			
			if(time is not null)
			{
				model.DepartureTime=time.Value;
			}

            return View(model);
		}

		[HttpPost]
		public async Task< IActionResult> ScheduleNewTrips(ScheduleTripsViewModel model)
		{

			_validateScheuleType(model);

			if (!ModelState.IsValid)
				return BadRequest();


			var dto = _mapScheduleDTO(model);


			var result =await _tripService.ScheduleTripsAsync(dto);

			if(!result.Success)
				return BadRequest();

			var tripsModel = _setTripsModel(result.Data!);


            //TempData["Trips"] = JsonConvert.SerializeObject(tripsModel);
  //          return RedirectToAction("ScheduledTrips", "Trips",tripsModel);
            return View("~/Views/Trips/NewScheduledTrips.cshtml", tripsModel);



		}



		///////////////////////////////////////////////////////////////////////////////////// Privete Methods
	
		private void _validateScheuleType(ScheduleTripsViewModel model)
		{

			if (model.RecurringPattern == PatternType.Weekly)
			{
				if (!model.WeekDays!.Any(d => d.IsSelected == true))
					ModelState.AddModelError("WeekDays", "Wrong");
			}

			//if (model.SelectedScheduleType == RecurringType.Monthly)
			//{
			//    if (!model.MonthDays!.Any(d => d.IsSelected == true))
			//        ModelState.AddModelError("MonthDays", "Wrong");
			//}


		}

		private IEnumerable<ScheduledTripViewModel>_setTripsModel(IEnumerable<ScheduledTripBaseDTO> trips)
		{
			var tripsModel = new List<ScheduledTripViewModel>();

			var mainTrips = trips.Where(t => t.MainTripId == null).ToList();

			foreach (var trip in mainTrips)
			{
				var returnTrip = trips.SingleOrDefault(t => t.MainTripId == trip.TripId);

                tripsModel.Add(new()
                {
                    RouteId = trip.RouteId,
                    RouteName = trip.RouteName,
                    TripId = trip.TripId,
                    Date = trip.Date,
                    Time = trip.Time,
                    Status = trip.Status,
                    DepartureStationId = trip.DepartureStationId,
                    TripTimeSpanInMInits = trip.TripTimeSpanInMInits,
					EstimatedDistance=trip.EstimatedDistance,

                    VehicleId = trip.VehicleId,
                    VehicleNumber = trip.VehicleNumber,
                    VehicleModel = trip.VehicleModel,


                });



			}

			return tripsModel;
		}
		private ScheduleDTO _mapScheduleDTO(ScheduleTripsViewModel model)
		{
			var dto = _mapper.Map<ScheduleDTO>(model);

			dto.CustomDates = DataTables.GetCustomDatesTable();
			dto.WeekDays = DataTables.GetWeekDaysTable();

			if (model.RecurringPattern == PatternType.Weekly)
			{
				// dto.Days = DataTables.GetDaysTable();


				var days = model.WeekDays!.Where(d => d.IsSelected == true).Select(d => d.Day).ToList();

				foreach (var day in days)
					dto.WeekDays.Rows.Add(day);
			}
			//else if (model.SelectedScheduleType == RecurringType.Monthly)
			//{
			//    // dto.Days = DataTables.GetDaysTable();

			//    var days=model.MonthDays!.Where(d=>d.IsSelected==true).Select(d=>d.Day).ToList();

			//    foreach (var day in days)
			//        dto.Days.Rows.Add(day);
			//}
			else if (model.RecurringPattern == PatternType.Custom)
			{
				// dto.Dates = DataTables.GetDatesTable();

				//var dates = JsonConvert.DeserializeObject<List<DateTime>>(model.JsonDates!);

				foreach (var date in model.CustomDates!)
					dto.CustomDates!.Rows.Add(date);
			}


			return dto;

		}
		private ScheduleTripsViewModel _populateModel(ScheduleTripsViewModel? model = null)
		{
			if (model == null)
				model = new ScheduleTripsViewModel();


			//model.Routes = _routeService.GetAllRoutes().Select(r => new SelectListItem()
			//{
			//	Value = r.RouteId.ToString(),
			//	Text = r.RouteName
			//}).ToList();


			model.Durations = new List<SelectListItem>
			{
						 new(){  Value=(ScheduleDuration.ForOneMonth).ToString(),   Text="For one month" },
						 new(){  Value=(ScheduleDuration.ForThreeMonth).ToString(),   Text="For three month"  },
						 new(){  Value=(ScheduleDuration.ForSixMonth).ToString(),   Text="For six month"  }

			};


			//model.ScheduleTypes = new List<SelectListItem>
			//{
			//			 new(){  Value=(RecurringType.Daily).ToString(),   Text="Every Single Day" },
			//			 new(){  Value=(RecurringType.Custom).ToString(),   Text="Certain Dates"  },
			//			 new(){  Value=(RecurringType.Weekly).ToString(),   Text="Certain Days in Week"  },
   //                      //new(){  Value=(RecurringType.Monthly).ToString(),   Text="Certain Days in Month"  },

   //         };


			for (byte d = 1; d <= 7; d++)
				model.WeekDays!.Add(new() { Day = d, IsSelected = false });
			//     model.WeekDays!.Add(new() { Day = d, IsSelected = true });




			return model;
		}

	}
}
