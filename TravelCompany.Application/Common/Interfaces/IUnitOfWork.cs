using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Application.Common.Interfaces.Repositories;
using TravelCompany.Domain.Entities;
using TravelCompany.Domain.Settings;

namespace TravelCompany.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<Point> Points { get; }
        IBaseRepository<Station> Stations { get; }
        IBaseRepository<route> Routes { get; }
        ITripRepository Trips { get; }
        IBaseRepository<TripPattern> TripPatterns { get; }
        IScheduledTravelRepository  ScheduledTravels { get; }
        IBaseRepository<RoutePoint> RoutePoints { get; }
        IBaseRepository <TravelStation> TravelStations { get; }
        IRezervationRepository  Reservations { get; }

        IVehicleRepository Vehicles { get; }
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

        Task<int> SaveAsync();

    }
}
