using Microsoft.AspNetCore.Mvc;

namespace Travel_Company_MVC.Controllers
{
    public class TripsController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }
    }
}
