using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Entities;

namespace TravelCompany.Application.Services.Recurrings
{
    public interface IRecurringServcie
    {

        Task<IEnumerable<Recurring>> GetRouteTripPattern(int routeId);
    }
}
