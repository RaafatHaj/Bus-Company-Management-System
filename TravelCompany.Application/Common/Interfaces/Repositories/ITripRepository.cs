using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;

namespace TravelCompany.Application.Common.Interfaces.Repositories
{
    public interface ITripRepository : IBaseRepository<Trip>
    {

		Task<IEnumerable<Trip>> GetAllTripsAsync();
		Task<IEnumerable<Trip>> ScheduleTripsForDaysInWeekAsync(ScheduleDTO dto);
		Task<IEnumerable<Trip>> ScheduleTripsForEverySingleDayAsync(ScheduleDTO dto);

		Task<IEnumerable<Trip>> ScheduleTripsForSpecificDatesAsync(ScheduleDTO dto);

	}
}
