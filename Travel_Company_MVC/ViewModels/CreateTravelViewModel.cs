using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TravelCompany.Domain.Const;
using TravelCompany.Domain.Eums;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace Travel_Company_MVC.ViewModels
{
    public class CreateTravelViewModel
	{
		public int TravelId { get; set; }


		[Display(Name = "Route"), Required(ErrorMessage = Errors.RequiredFiled)]
		public int RouteId { get; set; }
		public IEnumerable<SelectListItem>? Routes { get; set; } = null!;




		[Display(Name = "Travel Time"), Required(ErrorMessage = Errors.RequiredFiled)]
		public TimeSpan TravelTime { get; set; }




		[AssertThat("Seats>0 && Seats<45", ErrorMessage = "Seat number should be between 1 and 45 seat"),
			Display(Name = "Seats Number of vehicle"), Required(ErrorMessage = Errors.RequiredFiled)]
		public int Seats { get; set; }


		[Display(Name = "Starting Date"),
			AssertThat("StartingDate >= Today()")]
        [RequiredIf(" SelectedScheduleType!=ScheduleType.CertainDates",
        ErrorMessage = Errors.ScheduleDatesRequired)]
        public DateTime? StartingDate { get; set; }


		[Display(Name = "Schedule Duration")]
		[RequiredIf(" SelectedScheduleType!=ScheduleType.CertainDates",
		ErrorMessage = Errors.ScheduleDatesRequired)]
		public ScheduleDuration? ScheduleDuration { get; set; }
		public IEnumerable<SelectListItem>? ScheduleDurations { get; set; } = null!;



		[Display(Name = "Schedule Type"), Required(ErrorMessage = Errors.RequiredFiled)]
		public PatternType SelectedScheduleType { get; set; }
		public IEnumerable<SelectListItem>? ScheduleTypes { get; set; } = null!;






		[RequiredIf(" SelectedScheduleType==ScheduleType.CertainDates",
	    ErrorMessage = Errors.ScheduleDatesRequired)]
		public string? JsonDates { get; set; }
		


      

        [Display(Name = "Days In Week")]
        public IList<WeekDay>? WeekDays { get; set; }=new List<WeekDay>();
        

        [Display(Name = "Days In Month")]
        public IList<MonthDay>? MonthDays { get; set; }=new List<MonthDay>();

        public bool ReSchedule { get; set; }
    }

  
}
