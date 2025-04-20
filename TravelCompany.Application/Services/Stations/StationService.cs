using Microsoft.EntityFrameworkCore;
using TravelCompany.Application.Common.Interfaces;
using TravelCompany.Domain.Entities;

namespace TravelCompany.Application.Services.Stations
{
    public class StationService : IStationService
    {

        private readonly IUnitOfWork _unitOfWork;

        public StationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Station>> GetAllStationsAsync()
        {
            return await _unitOfWork.Stations.GetQueryable().ToListAsync();
        }

        //public Task<string> GetAllStationsJsonAsync()
        //{

        //    return JsonConvert.
        //}
    }
}
