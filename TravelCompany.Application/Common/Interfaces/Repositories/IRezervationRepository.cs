using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;

namespace TravelCompany.Application.Common.Interfaces.Repositories
{
    public interface IRezervationRepository: IBaseRepository<Reservation>
	{
		Task<(bool IsBooked, int? ReserbationId)> BookSeatAsync(BookingSeatDTO dto);

	}
}
