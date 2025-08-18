namespace Travel_Company_MVC.ViewModels
{
	public class AvailableVehicleViewModel
	{

		public int? VehicleId { get; set; }
		public string HomeStation { get; set; } = null!;
		public string? VehicleModel { get; set; } = null!;
		public string? VehicleNumber { get; set; } = null!;
		public int Seats { get; set; }
		//public bool IsAvailable { get; set; }
		public DateTime AvailibiltyStartDateTime { get; set; }
		public DateTime AvailibiltyEndDateTime { get; set; }

	}
}
