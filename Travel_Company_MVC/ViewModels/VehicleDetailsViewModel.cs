namespace Travel_Company_MVC.ViewModels
{
	public class VehicleDetailsViewModel
	{
		public int VehicleId { get; set; }
		public int TripId { get; set; }
		public string? VehicleModel { get; set; } 
		public string? VehicleNumber { get; set; }
		public int? Seats { get; set; }
		public string? HomeStation { get; set; }=null!;
		public bool IsFromDataBase { get; set; }=false;

	}
}
