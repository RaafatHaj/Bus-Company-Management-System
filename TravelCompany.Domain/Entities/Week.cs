using System.ComponentModel.DataAnnotations;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.Entities
{
    public class Week
    {
        [Key]
        public int Id { get; set; }
        public int RecurringId { get; set; }
        public Recurring? Recurring { get; set; }
        public int WeekOrder {  get; set; }

        public RecurringType RecurringType { get; set; }
        public int RecurringDays { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TripsNumber { get; set; }
        public int UnassignedTripsNumber { get; set; }




    }
}
