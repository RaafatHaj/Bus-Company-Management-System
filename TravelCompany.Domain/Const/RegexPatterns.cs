using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Domain.Const
{
    public static  class RegexPatterns
    {

        public const string CharactersOnly_Eng = "^[a-zA-Z-_ ]*$";
        public const string TurkishMobilPhone = "^(?:\\+90|0090|0)?5\\d{9}$";
        public const string Username = "^[a-zA-Z0-9-._@+]*$";
        public const string Password = "(?=(.*[0-9]))(?=.*[\\!@#$%^&*()\\\\[\\]{}\\-_+=~`|:;\"'<>,./?])(?=.*[a-z])(?=(.*[A-Z]))(?=(.*)).{8,}";
    }
}
