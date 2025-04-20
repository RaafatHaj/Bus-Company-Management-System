using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Application.Common.Interfaces;

namespace TravelCompany.Application.Services.RoutePoints
{
    internal class RoutePointService : IRoutePointService
    {


        private readonly IUnitOfWork _unitOfWork;

        public RoutePointService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
