using TravelCompany.Domain.Entities;

namespace Travel_Company_MVC.ViewModels
{
	public class RoutesViewModel
	{

		public IEnumerable<route> Routes = new List<route>();
		public bool ISFromScheduling { get; set; }


	}
}
