using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Domain.DTOs
{
    public class StationDTO
    {

        public int StationId { get; set; }
        public string StationName { get; set; } = null!;

        public int StationTimeValue { get; set; }
        public int StationDistanceValue { get; set; }
        public int StationOrder {  get; set; }


    }
}
