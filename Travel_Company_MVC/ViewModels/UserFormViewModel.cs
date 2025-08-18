using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TravelCompany.Domain.Const;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace Travel_Company_MVC.ViewModels
{
    public class UserFormViewModel
    {

        public string? Id { get; set; }

		[Display(Name = "First Name"), Required(ErrorMessage = Errors.RequiredFiled)]
		[MaxLength(100, ErrorMessage = Errors.MaxLength)]
        [RegularExpression(RegexPatterns.CharactersOnly_Eng, ErrorMessage = Errors.OnlyEnglishLetters)]
        public string FirstName { get; set; } = null!;

		[Display(Name = "Last Name"), Required(ErrorMessage = Errors.RequiredFiled)]
		[MaxLength(100, ErrorMessage = Errors.MaxLength)]
        [RegularExpression(RegexPatterns.CharactersOnly_Eng, ErrorMessage = Errors.OnlyEnglishLetters)]
        public string LastName { get; set; } = null!;

		[Display(Name = "ID Number"), Required(ErrorMessage = Errors.RequiredFiled)]
		public string IdNumber { get; set; } = null!;

		[Display(Name = "Gender"), Required(ErrorMessage = Errors.RequiredFiled)]
		public Gender Gender { get; set; }

		[Display(Name = "Birth Date"), Required(ErrorMessage = Errors.RequiredFiled)]
		public DateTime BirthDate { get; set; }

		[Display(Name = "Home Station"), Required(ErrorMessage = Errors.RequiredFiled)]
		public int StationId { get; set; }
        public IEnumerable<SelectListItem>? Stations { get; set; }

		[Display(Name = "Roles"), Required(ErrorMessage = Errors.RequiredFiled)]
		public IEnumerable<string> SelectedRoles { get; set; } = new List<string>();

        public IEnumerable<SelectListItem>? Roles { get; set; }


        [EmailAddress , MaxLength(200,ErrorMessage =Errors.MaxLength)]
        [Remote("IsEmailAllowed",null!,AdditionalFields ="Id",ErrorMessage =Errors.Duplicated)]
        public string Email { get; set; } = null!;

		[Display(Name = "Phone Number"), Required(ErrorMessage = Errors.RequiredFiled)]
		public string PhoneNumber { get; set; } = null!;





    }
}
