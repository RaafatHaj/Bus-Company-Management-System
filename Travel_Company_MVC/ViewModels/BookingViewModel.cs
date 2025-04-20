using Microsoft.AspNetCore.Mvc.Rendering;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace Travel_Company_MVC.ViewModels
{
    public class BookingViewModel
    {

        public IEnumerable<SelectListItem> Stations { get; set; }=new List<SelectListItem>();
        public string JsonStations { get; set; } = null!;

        public int StationAId { get; set; }

        public int StationBId { get;set; }


    }
}
