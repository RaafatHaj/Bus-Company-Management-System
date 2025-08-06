using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TravelCompany.Domain.Const;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace Travel_Company_MVC.ViewModels
{
    public class PickTicketViewModel
    {

        public IEnumerable<SelectListItem>? Stations { get; set; }=new List<SelectListItem>();
        public string? JsonStations { get; set; }

		[Display(Name = "Departure Station"), Required(ErrorMessage = Errors.RequiredFiled)]
		public int StationAId { get; set; }

		[Display(Name = "Arrival Station"), Required(ErrorMessage = Errors.RequiredFiled)]
		public int StationBId { get;set; }

		[Display(Name = "Trip Date"), Required(ErrorMessage = Errors.RequiredFiled)]
		public DateTime TripDate { get; set; }


    }
}
