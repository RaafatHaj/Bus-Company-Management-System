using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using TravelCompany.Domain.Const;

namespace Travel_Company_MVC.ViewModels
{
    public class BookTicketViewModel
    {

		public int TripId { get; set; }
		public DateTime TripDepatureDateTime { get; set; }
		public long SeatCode { get; set; }
		public int StationAId { get; set; }
		public int StationBId { get; set; }


		[Display(Name = "Phone Number"), Required(ErrorMessage = Errors.RequiredWithMessage)]
		[RegularExpression(RegexPatterns.TurkishMobilPhone,ErrorMessage = Errors.UnValidTurkishMobilPhone)]
		public string PersonPhone { get; set; } = null!;
		public string? PersonEmail { get; set; }


		public IList<BookedSeat> BookedSeats { get; set; } = new List<BookedSeat>();

		//public string PersonId { get; set; } = null!;
		//public string PersonName { get; set; } = null!;
  //      public bool PersonGendor { get; set; } 


   

    }
}
