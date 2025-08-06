using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Common;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.Entities
{
    public class Reservation : BaseEntity
	{

        public int Id { get; set; } 

        public int TripId { get; set; }
        public Trip? Trip { get; set; }

        public int StationAId {  get; set; }
        public Station? StationA { get; set; }

        public int StationBId { get; set; }
        public Station? StationB { get; set; }

        public int SeatNumber { get; set; }
        public long SeatCode { get; set; }

        public string PersonId { get; set; } = null!;
        public string PersonName { get; set; }=null!;
        public string PersonPhone { get; set; } = null!;
        public string? PersonEmail { get; set; }
        public SeatStatus PersonGender {  get; set; }
        public DateTime TripDepartureDateTime { get; set; }
    }
}
