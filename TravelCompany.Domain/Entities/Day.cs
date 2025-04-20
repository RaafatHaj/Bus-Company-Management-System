using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Domain.Entities
{
    public class Day
    {
        public int Id { get; set; }
        public int RecurringId { get; set; }
        public Recurring? Recurring { get; set; }
        public int day {  get; set; }
    }
}
