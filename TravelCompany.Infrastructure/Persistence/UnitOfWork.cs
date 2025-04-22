using Microsoft.Extensions.Options;
using TravelCompany.Application.Common.Interfaces;
using TravelCompany.Application.Common.Interfaces.Repositories;
using TravelCompany.Domain.Entities;
using TravelCompany.Domain.Settings;
using TravelCompany.Infrastructure.Persistence.Repositories;

namespace TravelCompany.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ConnectionStrings _connectionStrings;

        public UnitOfWork(ApplicationDbContext context,IOptions<ConnectionStrings> connectionStrings)
        {
            _context = context;
            _connectionStrings = connectionStrings.Value;
        }

        public IBaseRepository<Point> Points => new BaseRepository<Point>(_context);

        public IBaseRepository<Station> Stations => new BaseRepository<Station>(_context);

        public IBaseRepository<route> Routes => new BaseRepository<route>(_context);
        public IRezervationRepository Reservations => new RezervationRepository(_context, _connectionStrings.DefaultConnection);


        public IScheduledTravelRepository ScheduledTravels => new ScheduledTravelRepository (_context, _connectionStrings.DefaultConnection);

        public IBaseRepository<RoutePoint> RoutePoints => new BaseRepository<RoutePoint>(_context);

        public IBaseRepository<TravelStation> TravelStations =>  new BaseRepository<TravelStation>(_context);

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
