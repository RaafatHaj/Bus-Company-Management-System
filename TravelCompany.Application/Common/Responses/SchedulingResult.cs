


namespace TravelCompany.Application.Common.Responses
{
	public class SchedulingResult
	{

		public bool IsSuccess { get; set; }
		public string Status { get; set; } = null!;
		public string? Message { get; set; }
		public string? Data { get; set; }
	}
}
