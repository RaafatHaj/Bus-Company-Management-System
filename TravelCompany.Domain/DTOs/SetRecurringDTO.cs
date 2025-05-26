using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Domain.DTOs
{
    public class SetRecurringDTO
    {

        public int RouteId { get; set; }
        public TimeSpan Time {  get; set; }
        public DateTime StartDate { get; set; }


    }
}
