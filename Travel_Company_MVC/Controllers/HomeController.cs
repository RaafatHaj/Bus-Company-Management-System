using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Security.Claims;
using Travel_Company_MVC.Helper;
using Travel_Company_MVC.Models;
using TravelCompany.Application.Common.Interfaces;
using TravelCompany.Application.Services.Stations;
using TravelCompany.Infrastructure.Persistence;
using TravelCompany.Infrastructure.Persistence.Entities;

namespace Travel_Company_MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IStationService _stationService;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IStationService stationService, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _context = context;
            _stationService = stationService;
            _unitOfWork = unitOfWork;
        }


        public async Task<IActionResult> Index()
        {
            var stationIdClaim = User.FindFirst(CustomClaimType.StationId)?.Value;
            int stationId;
          
            //if( int.TryParse(stationIdClaim, out stationId))
            //{
            //    var controlTable =await _unitOfWork.TravelStations.GetQueryable()
            //        .Where(t => t.StationId == stationId)
            //        .Include(t=>t.Station)
            //        .ToListAsync();
            //    return View(controlTable);
            //}




            return View();
        }

        [HttpGet]
        public IActionResult Test()
        {
 


            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
