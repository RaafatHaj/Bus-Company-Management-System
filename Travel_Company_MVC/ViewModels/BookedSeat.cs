using System.ComponentModel.DataAnnotations;
using TravelCompany.Domain.Const;

namespace Travel_Company_MVC.ViewModels
{
    public class BookedSeat
    {
		
		public int SeatNumber { get; set; }

		[Display(Name = "National ID"), Required(ErrorMessage = Errors.RequiredWithMessage)]
		public string PersonId { get; set; } = null!;

		[Display(Name = "First Name"), Required(ErrorMessage = Errors.RequiredWithMessage)]
		public string PersonFirstName { get; set; } = null!;

		[Display(Name = "Last Name"), Required(ErrorMessage = Errors.RequiredWithMessage)]
		public string PersonLasttName { get; set; } = null!;


		[Display(Name = "Passanger Gender"), Required(ErrorMessage = Errors.RequiredWithMessage)]
		public SeatStatus PersonGender { get; set; }
	}
}
