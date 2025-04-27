using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Application.Common.Interfaces;
using TravelCompany.Domain.Entities;

namespace TravelCompany.Application.Services.Recurrings
{
    public class RecurringServcie : IRecurringServcie
    {


        private readonly IUnitOfWork _unitOfWork;

        public RecurringServcie(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Recurring>> GetRouteTripPattern(int routeId)
        {
            return await _unitOfWork.Recurrings.GetQueryable()
                          .Include(r => r.Days)
                          .Where(r => r.RouteId == routeId)
                          .ToListAsync();
        }
    }
}
