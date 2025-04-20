using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelCompany.Application.Services.ScheduledTravels;

namespace Travel_Company_MVC.Controllers
{
    [Authorize]
    public class ScheduledTravelsController : Controller
	{

		private readonly IScheduledTravelService _scheduledTravelService;
		private readonly IMapper _mapper;

        public ScheduledTravelsController(IScheduledTravelService scheduledTravelService, IMapper mapper)
        {
            _scheduledTravelService = scheduledTravelService;
            _mapper = mapper;
        }

		[HttpGet]
        public async Task< IActionResult> Index()
		{
			var scheduledTravels= await _scheduledTravelService.RetriveMainScheduledTravelsAsync();

			if (scheduledTravels == null)
				return BadRequest();
			// create view model corosponed with DTO

			var modle=_mapper.Map<List<ScheduledTravelVeiwModel>>(scheduledTravels);




			return View(modle);
		}

		[HttpGet]
		public async Task<IActionResult> ScheduledTravelDetials(int scheduledTravelId)
		{
			var scheduledTravelDetials = await _scheduledTravelService.RetriveScheduledTravelDetailAsync(scheduledTravelId);

			if(scheduledTravelDetials == null)
				return BadRequest();

			var model=_mapper.Map<List<ScheduledTravelDetialViewModel>>(scheduledTravelDetials);


			return PartialView("_ScheduledTravelDetials",model);
		}

		[HttpPost]
		public async Task<IActionResult> SetStationStatusAsLeft(int stationId , int scheduledTravelId)
		{
			if (await _scheduledTravelService.SetStationStatusAsLeftAsync(stationId, scheduledTravelId))
				return Ok();


			return BadRequest();
		}

	}
}
