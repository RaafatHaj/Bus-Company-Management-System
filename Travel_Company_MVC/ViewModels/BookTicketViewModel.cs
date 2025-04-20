using System.Runtime.CompilerServices;

namespace Travel_Company_MVC.ViewModels
{
    public class BookTicketViewModel
    {

		public int ScheduledTravelId { get; set; }


        public int StationAOrder { get; set; }
        public int StationAId { get; set; }


        public int StationBOrder { get; set; }
        public int StationBId { get; set; }



		public int SeatNumber { get; set; }
        public int seatsNumbers { get; set; }
        public IEnumerable<BookedSeat> BookedSeats { get; set; }=new List<BookedSeat>();

        public int RouteStationsNumber { get; set; }

        public string PersonId { get; set; } = null!;
		public string PersonName { get; set; } = null!;
		public string PersonPhone { get; set; } = null!;
        public string? PersonEmail { get; set; }
        public bool PersonGendor { get; set; } 


   

    }
}
