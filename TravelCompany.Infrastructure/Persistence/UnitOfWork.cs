using Microsoft.EntityFrameworkCore.Storage;
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
        private IDbContextTransaction? _currentTransaction;

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

        public ITripRepository Trips => new TripRepository(_context , _connectionStrings.DefaultConnection);

        public IBaseRepository<Recurring> Recurrings => new BaseRepository<Recurring>(_context);


        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null) return; // Already started
            _currentTransaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _context.SaveChangesAsync();

                if (_currentTransaction != null)
                {
                    await _currentTransaction.CommitAsync();
                }
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            try
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.RollbackAsync();
                }
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }


        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
    }
}
