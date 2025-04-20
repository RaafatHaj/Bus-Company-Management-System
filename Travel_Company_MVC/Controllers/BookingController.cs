using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Data;
using TravelCompany.Application.Services.Rezervations;
using TravelCompany.Application.Services.ScheduledTravels;
using TravelCompany.Application.Services.Stations;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;

namespace Travel_Company_MVC.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {

        private readonly IStationService _stationService;
        private readonly IScheduledTravelService _scheduledTravelService;
        private readonly IMapper _mapper;
        private readonly IRezervationService _rezervationService;

		public BookingController(IStationService stationService, IScheduledTravelService scheduledTravelService, IMapper mapper, IRezervationService rezervationService)
		{
			_stationService = stationService;
			_scheduledTravelService = scheduledTravelService;
			_mapper = mapper;
			_rezervationService = rezervationService;
		}



		[HttpGet]
        public async Task<IActionResult> FindSuitableTravels()
        {

            var stations = await _stationService.GetAllStationsAsync();

            if (stations == null)
                return NotFound();

            var model = new BookingViewModel();

            model.Stations = stations.Select(s => new SelectListItem()
            {
                Value = s.StationId.ToString(),
                Text = s.StationName
            }).ToList();

            model.JsonStations = JsonConvert.SerializeObject(stations);

            return View(model);
     
        }


        [HttpPost]
        public async Task<IActionResult> FindSuitableTravels(BookingViewModel model)
        {

            // here i will implement Pagenation on server side using DataTable >>>>>

            var travels = await _scheduledTravelService.FindSuitableTravelsAsync(model.StationAId, model.StationBId);

            if (travels == null)
                return NotFound();

            var travelModel = _mapper.Map<IEnumerable<SuitableTravelViewModel>>(travels);

            // pass model and return partial view >>>>>


            return PartialView("_SuitableTravels", travelModel);
        }


        [HttpPost]

        public async Task<IActionResult> GetAvaliableSeats([FromBody]GetAvaliableSeatsDTO dto)
        {
            var result = await _scheduledTravelService.GetAvaliableSeats(dto);

            // HashSet<int> set = new HashSet<int>(result);


            var model = _mapper.Map<BookTicketViewModel>(dto);

            int[] allSeats = Enumerable.Range(1, dto.seatsNumbers).ToArray(); // Initail and fill ...


            model.BookedSeats = allSeats.Select(s => new BookedSeat()
            {
                SeatNumber = s,
                IsBooked = !result.Contains(s), // O(n) in Array and List ... -> use it for small data
         //       IsAvaliable = set.Contains(t)  // O(1) -> use it for large data
            }).ToList();


            // Steps ...
            // 1.Match dto to BookTicketViewModel in Mappin Profile ...
            // 2.Return partial view (_AvaliableSetas) and pass BookTicketViewModel to it ...
            // 3.Partial View will be form and sumbmit will move you to another page ...

            return PartialView("_AvaliableSeats", model);
        }



        [HttpGet]
        public  IActionResult BookSeat(BookTicketViewModel model)
        {
            return View(model);
        }


		[HttpPost]
		public async Task< IActionResult> BookSeat2(BookTicketViewModel model)
		{
            if (!ModelState.IsValid)
                return BadRequest();

            var dto=_mapper.Map<BookingSeatDTO>(model);

            var result = await _rezervationService.BookTicketAsync(dto);


            if ( !result.IsBooked)
                return BadRequest();


            // Here Redirect to action and pass ReservationId to it , and show the resulte ...


			return View("TicketDetails");
		}


        [HttpGet]

        public async Task<IActionResult> GetStationPassengers(int scheduledTravelId, int stationId)
        {

            var model = await _rezervationService.GetStationPassengersAsync(scheduledTravelId, stationId);

            return PartialView("_StationPassengers", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetDestinationStations()
        {
            var stations=await _stationService.GetAllStationsAsync();

            return PartialView("_DestinationStation");
        }

    }


}




// What do i want to do 
