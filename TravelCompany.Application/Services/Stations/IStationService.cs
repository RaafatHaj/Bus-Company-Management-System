using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Entities;

namespace TravelCompany.Application.Services.Stations
{
    public interface IStationService
    {

       Task<IEnumerable<Station>> GetAllStationsAsync();
     //  Task<string> GetAllStationsJsonAsync();
    }
}
