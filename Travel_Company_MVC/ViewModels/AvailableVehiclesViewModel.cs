namespace Travel_Company_MVC.ViewModels
{
    public class AvailableVehiclesViewModel
    {

         
        public int TripId { get; set; }
        public DateTime TripDateTime { get; set; }
        public string FirstStation { get; set; } = null!;
        public string LastStation { get; set; }= null!;
        public IEnumerable<AvailableTripVehicleDTO> Vehicles { get; set; }=new List<AvailableTripVehicleDTO>();

    }
}
