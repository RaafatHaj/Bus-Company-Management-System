namespace Travel_Company_MVC.ViewModels
{
    public class RouteStationsViewModel
    {

        public int RouteId { get; set; }
        public string RouteName { get; set; } = null!;
        public int EstimatedTime { get; set; }
        public int EstimatedDistance { get; set; }
        public IList<StationDTO> Stations { get; set; } = new List<StationDTO>();

    }
}
