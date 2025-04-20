using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Domain.Entities
{
    public class Point
    {
        public int PointId { get; set; }

        public int StationId { get; set; }
        public Station? Station { get; set; }

        public int PreviousStationId { get; set; }
        public Station? PreviousStation { get; set; }

        public int DistanceValue { get; set; }
        public int TimeValue { get; set; }
    }
}
