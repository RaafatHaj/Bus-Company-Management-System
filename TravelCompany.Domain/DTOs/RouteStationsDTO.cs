using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Entities;

namespace TravelCompany.Domain.DTOs
{
    public class RouteStationsDTO
    {

        public int RouteId { get; set; }
        public string RouteName { get; set; } = null!;
        public int EstimatedTime { get; set; }
        public int EstimatedDistance { get; set; }
        public IList<StationDTO> Stations { get; set; } = new List<StationDTO>();

    }
}
