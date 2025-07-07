using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TravelCompany.Domain.Const;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace Travel_Company_MVC.ViewModels
{
	public class ScheduleTripsViewModel
	{


		[Display(Name = "Route"), Required(ErrorMessage = Errors.RequiredFiled)]
		public int RouteId { get; set; } 


		[Display(Name = "Departure Time"), Required(ErrorMessage = Errors.RequiredFiled)]
		public TimeSpan DepartureTime { get; set; }


		//[Display(Name = "Return Time"), Required(ErrorMessage = Errors.RequiredFiled)]
		//[AssertThat("ReturnTime != DepartureTime", ErrorMessage = Errors.ReturnTimeIsEquel) ]
		//public TimeSpan ReturnTime { get; set; }


		//[AssertThat("Seats>0 && Seats<45", ErrorMessage = "Seat number should be between 1 and 45 seat")]
        [Display(Name = "Vehicle Capacity"), Required(ErrorMessage = Errors.RequiredFiled)]
		public int Seats { get; set; }


		[Display(Name = "Recurring Pattern"), Required(ErrorMessage = Errors.RequiredFiled)]
		public PatternType RecurringPattern { get; set; }



		[Display(Name = "Start Date")]
        //[AssertThat("StartDate >= Today()")]
		[RequiredIf(" RecurringPattern!=PatternType.Custom ", ErrorMessage = Errors.ScheduleDatesRequired)]
		public DateTime? StartDate { get; set; }


		[Display(Name = "End Date")]
		//[AssertThat("EndDate > StartDate")]
		[RequiredIf(" RecurringPattern!=PatternType.Custom && ScheduleDuration==null ", ErrorMessage = Errors.ScheduleDatesRequired)]
		public DateTime? EndDate { get; set; }



		[Display(Name = "Schedule Duration")]
		[RequiredIf(" RecurringPattern!=PatternType.Custom && EndDate==null", ErrorMessage = Errors.ScheduleDatesRequired)]
		public ScheduleDuration? ScheduleDuration { get; set; }
		public IEnumerable<SelectListItem>? Durations { get; set; } = null!;



		//[RequiredIf(" RecurringPattern==RecurringType.Custom ", ErrorMessage = Errors.ScheduleDatesRequired)]
		//public string? JsonDates { get; set; }


		[RequiredIf(" RecurringPattern==PatternType.Custom ", ErrorMessage = Errors.ScheduleDatesRequired)]
		public IList<DateTime>? CustomDates { get; set; } 




		[Display(Name = "Days In Week")]
		public IList<WeekDay>? WeekDays { get; set; } = new List<WeekDay>();

		public bool IsEditStatus { get; set; }= false;

	}
}
