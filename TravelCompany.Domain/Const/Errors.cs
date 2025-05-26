using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Domain.Const
{
	public static class Errors
	{
		public const string ScheduleDatesRequired = "You should select the dates you want to schedule the travel at";
		public const string ScheduleDaysRequired = "You should select the days you want to schedule the travel at";
		public const string RequiredFiled = "Requird field";
		public const string MaxLength = "Length con not be more then {1} charecter";
		public const string Duplicated = "Another record with the same {0} is alrady exists";
        public const string ConfirmPasswordNotMatch = "The password and confirmation password do not match.";
        public const string OnlyEnglishLetters = "Only English letters are allowed.";
        public const string InvalidUsername = "Username can only contain letters or digits.";
        public const string MaxMinLength = "The {0} must be at least {2} and at max {1} characters long.";
        public const string WeakPassword = "Passwords contain an uppercase character, lowercase character, a digit, and a non-alphanumeric character. Passwords must be at least 8 characters long";
        public const string MaxSize = "File cannot be more that 2 MB!";
        public const string NotAllowedExtension = "Only .png, .jpg, .jpeg files are allowed!";
        public const string ReturnTimeIsEquel = "Return time can not be the same of departure time.";
    }
}
