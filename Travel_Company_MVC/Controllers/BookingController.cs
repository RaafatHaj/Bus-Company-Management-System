using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;
using System.Security.Claims;
using TravelCompany.Application.Services.Rezervations;
using TravelCompany.Application.Services.ScheduledTravels;
using TravelCompany.Application.Services.Stations;
using TravelCompany.Application.Services.Travels;
using TravelCompany.Domain.Const;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;

namespace Travel_Company_MVC.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {

        private readonly IStationService _stationService;
        private readonly ITripService _tripService;
        private readonly IMapper _mapper;
        private readonly IRezervationService _rezervationService;

        public BookingController(IStationService stationService,  IMapper mapper, IRezervationService rezervationService, ITripService tripService)
        {
            _stationService = stationService;
            _mapper = mapper;
            _rezervationService = rezervationService;
            _tripService = tripService;
        }



        [HttpGet]
        public async Task<IActionResult> FindSuitableTravels()
        {

            var stations = await _stationService.GetAllStationsAsync();

            if (stations == null)
                return NotFound();

            var model = new PickTicketViewModel();

            model.Stations = stations.Select(s => new SelectListItem()
            {
                Value = s.StationId.ToString(),
                Text = s.StationName
            }).ToList();

            model.JsonStations = JsonConvert.SerializeObject(stations);

            return View(model);
     
        }



        [HttpPost]
        public async Task<IActionResult> FindSuitableTravels(PickTicketViewModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest();


            var travels = await _tripService.FindSuitableTripsAsync(model.StationAId, model.StationBId ,model.TripDate);

            if (travels == null)
                return NotFound();

            var travelModel = _mapper.Map<IEnumerable<SuitableTravelViewModel>>(travels);

            // pass model and return partial view >>>>>


            return PartialView("_SuitableTravels", travelModel);
        }



        [HttpGet]
        public async Task<IActionResult> GetAvaliableSeats(int tripId ,int stationAId ,int stationBId ,DateTime tripDepartureDateTime)
        {

            var result = await _tripService.GetAvaliableSeatsAsync(tripId, stationAId, stationBId);

  

            var model = new SelectSeatViewModel()
            {
                TripId=tripId,
                BookedSeats = result.AvalibleSeats,
                SeatCode=result.SeatCode,
                StationAId=stationAId,
                StationBId=stationBId,
                TripDepatureDateTime=tripDepartureDateTime
			};

			return PartialView("_AvaliableSeats", model);


		}



		[HttpGet]
        public  IActionResult BookSeat(SelectSeatViewModel model)
        {

			BookTicketViewModel viewModel = new()
            {
                TripId = model.TripId,
                TripDepatureDateTime=model.TripDepatureDateTime,

                BookedSeats=model.BookedSeats
                .Where(s=>s.IsSelected==true)
                .Select(s => new BookedSeat()
                {
                    SeatNumber=s.SeatNumber,

                }).ToList(),

				SeatCode = model.SeatCode,
				StationAId = model.StationAId,
				StationBId = model.StationBId
			};


			return View(viewModel);
        }


		[HttpPost]
		public async Task< IActionResult> BookSeat(BookTicketViewModel model)
		{
            if (!ModelState.IsValid)
                return BadRequest();

            var dto = _mapScheduleDTO(model);

           

			var result = await _rezervationService.BookSeatAsync(dto);

            

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






		private BookTicketDTO _mapScheduleDTO(BookTicketViewModel model)
		{
			var dto = _mapper.Map<BookTicketDTO>(model);

            dto.BookedSeatsTable = DataTables.GetBookedSeatsTable();

            dto.CreatedOn = DateTime.Now;
            dto.CreatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;



			foreach (var bookedSeat in model.BookedSeats)
                dto.BookedSeatsTable.Rows.Add(bookedSeat.SeatNumber, bookedSeat.PersonId, bookedSeat.PersonFirstName+" "+bookedSeat.PersonLasttName
                    ,(int)bookedSeat.PersonGender);


			return dto;

		}
        




	}





}




// What do i want to do 
