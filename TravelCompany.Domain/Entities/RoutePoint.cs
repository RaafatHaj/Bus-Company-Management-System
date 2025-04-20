using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Domain.Entities
{
    public class RoutePoint
    {

        public int RouteId { get; set; }
        public route? Route { get; set; }

        public int PointId { get; set; }
        public Point? Point { get; set; }

        public int PointOrder {  get; set; }
    }
}
