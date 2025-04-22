using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Application.Common.Interfaces.Repositories;
using TravelCompany.Domain.Entities;

namespace TravelCompany.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<Point> Points { get; }
        IBaseRepository<Station> Stations { get; }
        IBaseRepository<route> Routes { get; }
        IScheduledTravelRepository  ScheduledTravels { get; }
        IBaseRepository<RoutePoint> RoutePoints { get; }
        IBaseRepository <TravelStation> TravelStations { get; }
        IRezervationRepository  Reservations { get; }

        int Save();

    }
}
