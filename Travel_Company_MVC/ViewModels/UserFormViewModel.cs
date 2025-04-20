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

        [MaxLength(20,ErrorMessage =Errors.MaxLength)]
        [Remote("IsUserNameAllowed",null!,AdditionalFields ="Id",ErrorMessage =Errors.Duplicated)]
        [RegularExpression(RegexPatterns.Username, ErrorMessage = Errors.InvalidUsername)]
        public string UserName { get; set; } = null!;


        [EmailAddress , MaxLength(200,ErrorMessage =Errors.MaxLength)]
        [Remote("IsEmailAllowed",null!,AdditionalFields ="Id",ErrorMessage =Errors.Duplicated)]
        public string Email { get; set; } = null!;


        [MaxLength(100, ErrorMessage = Errors.MaxLength), Display(Name = "Full Name")]
        [RegularExpression(RegexPatterns.CharactersOnly_Eng,ErrorMessage =Errors.OnlyEnglishLetters)]
        public string FullName { get; set; } = null!;

        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = Errors.MaxMinLength, MinimumLength = 8)]
        [RegularExpression(RegexPatterns.Password, ErrorMessage = Errors.WeakPassword)]
        [RequiredIf("Id == null",ErrorMessage = Errors.RequiredFiled)]
        public string? Password {  get; set; } = null!;

        [DataType(DataType.Password), Display(Name = "Confirm Password")]
        [Compare("Password",ErrorMessage =Errors.ConfirmPasswordNotMatch)]
        [RequiredIf("Id == null", ErrorMessage = Errors.RequiredFiled)]
        public string? ConfirmPassword { get; set; } = null!;


        [Display(Name = "Roles")]
        public IEnumerable<string> SelectedRoles { get; set; }=new List<string>();

        public IEnumerable<SelectListItem>? Roles { get; set; }



    }
}
