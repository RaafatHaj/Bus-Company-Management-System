using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Application.Common.Interfaces;
using TravelCompany.Domain.Entities;

namespace TravelCompany.Application.Services.Routes
{
    internal class RouteService : IRouteService
    {

        private readonly IUnitOfWork _unitOfWork;

        public RouteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public IEnumerable<route> GetAllRoutes()
		{
			return _unitOfWork.Routes.GetQueryable().ToList();
		}

		public async Task<IEnumerable<route>> GetAllRoutesAsync()
		{
            return await _unitOfWork.Routes.GetQueryable().AsNoTracking()
                .Include(r=>r.FirstStation)
                .Include(r=>r.LastStation)
                .ToListAsync();
		}

        public async Task<IEnumerable<RoutePoint>> GetRouteDetailsAsync( int routeID)
        {
            return await _unitOfWork.RoutePoints.GetQueryable().AsNoTracking()
                .Where(rp=>rp.RouteId==routeID)
                .OrderBy(rp=>rp.PointOrder)
                .Include(rp=>rp.Route)
                .Include(rp => rp.Point).ThenInclude(p=>p!.Station)
                .ToListAsync();
        }
    }
}
