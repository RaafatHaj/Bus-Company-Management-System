using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using TravelCompany.Application.Services.Routes;
using TravelCompany.Application.Services.Travels;
using TravelCompany.Domain.Const;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Eums;

namespace Travel_Company_MVC.Controllers
{
    [Authorize]
    public class TravelsController : Controller
    {
        private readonly ITravelService _travelService;
        private readonly IRouteService _routeService;   
        private readonly IMapper _mapper;

		public TravelsController(ITravelService travelService, IMapper mapper, IRouteService routeService)
		{
			_travelService = travelService;
			_mapper = mapper;
			_routeService = routeService;
		}


		[HttpGet]
        public   IActionResult Create()
        {
            //var time = new TimeSpan(00, 00, 00);
            ////   var travel = await _travelService.FindTravelAsync(time, 1);
            //var travel = await _travelService.FindTravelAsync(1026);

            //if (travel == null)
            //    return BadRequest();


            //var model = new CreateTravelViewModel();
            //model.ScheduleDuration = (ScheduleDuration)travel.ScheduleDuration!;
            //model.SelectedScheduleType= (ScheduleType)travel.ScheduleType;
            //model.StartingDate = DateTime.Now;
            //model.RouteId = travel.RouteId;
            //model.Seats = 1;
            //model.ReSchedule = travel.ReSchedule;
            //var date = new DateTime(2025,2,15); 
            //var dates=new List<string>() { DateTime.Now.Date.ToString("yyyy-MM-dd"), date.ToString("yyyy-MM-dd") };
            //model.JsonDates= JsonConvert.SerializeObject(dates);

			//return View(_populateModel(model));
			return View(_populateModel());
        }

        [HttpPost]
        public async Task< IActionResult> Create(CreateTravelViewModel model)
        {


            _validateScheuleType( model);

       
            if (!ModelState.IsValid)
            {
                return View(_populateModel());
            }

            var dto = _mapScheduleTravelDTO(model);

            var result = await _travelService.ScheduleNewTravelsAsync(dto);


			if (result.IsSuccess)
                return View(_populateModel());


            return View(_populateModel());
        }

		[HttpGet]
		public IActionResult Edit()
		{

            // Editing Travels ...
            //   - RouteId or TravelTime ...
            //     = will check if there is a travel has the same new RouteId and TravelTime 
            //       if there is No Travel ==>> Edit the Travel and The scheduled travels related to it after the selected Date ...
            //       if there is alrady Travel ==>> Update the Scheduled Travels related to the new Travel Id 
            //          and delete the old Travel after asking user contexting ...
            //   - ReSchedule ...
            //     - It is ok to edit it as you want without any constraints ...
            //   - Schedule Type ...
            //     - The Most Important one ...
            //       - you will have these Choices ...
            //         1. Edit Schedule Type After Schedule Ending date ...
            //            ==>> Edit Type and Schedule new Travels with starting date has the value of Ending date 
            //         2. Edit Schedule Type form date Before Ending date ..
            //            = you will have these Choices ...
            //            1. Keep all the Scheduled Travels tell the Ending date and add the new Travels along with them 
            //              => as you schedule you will check if there is an already scheduled travel to ignore schedule new one or Not 
            //            2. Keep just Scheduled Travels that have booking and delete the Empty Travels
            //              => Delete Empty Travels - as you scheduling check if there is an already Scheduled Travel to Ignore ...
            //            3. delete all scheduled travels after selected date wather it has booking or not and sending messages to customeres to 
            //               choose an apropperate another travel and give them the proirty ... 


            // Editing Scheduled Travels 
            //    - Scheuled Travel Time ....
            //      - it is ok to edit it as you want ,,,
            //    - Cansle it 
            //      - it is ok to cansel after sending notification to customers ...


			return View(_populateModel());
		}

		//[HttpPost]
		//public async Task<IActionResult> Add(CreateTravelViewModel model)
		//{

		//}


		private void _validateScheuleType(CreateTravelViewModel model)
        {

            if (model.SelectedScheduleType == RecurringType.Weekly)
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

        private TravelScheduleDTO _mapScheduleTravelDTO(CreateTravelViewModel model)
        {
            var dto= _mapper.Map<TravelScheduleDTO>(model);

			dto.Dates = DataTables.GetDatesTable();
			dto.Days = DataTables.GetDaysTable();

			if (model.SelectedScheduleType == RecurringType.Weekly)
            {
                // dto.Days = DataTables.GetDaysTable();

               
                var days = model.WeekDays!.Where(d => d.IsSelected == true).Select(d => d.Day).ToList();

                foreach (var day in days)
                    dto.Days.Rows.Add(day);
            }
            //else if (model.SelectedScheduleType == RecurringType.Monthly)
            //{
            //    // dto.Days = DataTables.GetDaysTable();

            //    var days=model.MonthDays!.Where(d=>d.IsSelected==true).Select(d=>d.Day).ToList();

            //    foreach (var day in days)
            //        dto.Days.Rows.Add(day);
            //}
            else if (model.SelectedScheduleType == RecurringType.Irregular)
            {
                // dto.Dates = DataTables.GetDatesTable();

                var dates = JsonConvert.DeserializeObject<List<DateTime>>(model.JsonDates!);

                foreach (var date in dates)
                    dto.Dates!.Rows.Add(date);
            }


            return dto;

        }

        private CreateTravelViewModel _populateModel(CreateTravelViewModel? model=null)
        {
            if( model==null)
                model =new CreateTravelViewModel();


            model.Routes = _routeService.GetAllRoutes().Select(r => new SelectListItem()
            {
                Value = r.RouteId.ToString(),
                Text = r.RouteName
            }).ToList();


			model.ScheduleDurations = new List<SelectListItem>
            {
                         new(){  Value=(ScheduleDuration.ForOneMonth).ToString(),   Text="For one month" },
                         new(){  Value=(ScheduleDuration.ForThreeMonth).ToString(),   Text="For three month"  },
                         new(){  Value=(ScheduleDuration.ForSixMonth).ToString(),   Text="For six month"  }

            };


            model.ScheduleTypes = new List<SelectListItem>
            {
                         new(){  Value=(RecurringType.Daily).ToString(),   Text="Every Single Day" },
                         new(){  Value=(RecurringType.Irregular).ToString(),   Text="Certain Dates"  },
                         new(){  Value=(RecurringType.Weekly).ToString(),   Text="Certain Days in Week"  },
                         //new(){  Value=(RecurringType.Monthly).ToString(),   Text="Certain Days in Month"  },

            };


            for (byte d = 1; d <= 7; d++)
                model.WeekDays!.Add(new() { Day = d, IsSelected = false });
           //     model.WeekDays!.Add(new() { Day = d, IsSelected = true });



            for (byte i = 1; i <= 31; i++)
                model.MonthDays!.Add(new() { Day = i, IsSelected = false });
            //    model.MonthDays!.Add(new() { Day = i, IsSelected = true });

            return model;
        }
    }
}
