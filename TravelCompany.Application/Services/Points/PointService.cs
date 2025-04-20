using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Application.Common.Interfaces;

namespace TravelCompany.Application.Services.Points
{
    public class PointService :IPointService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PointService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
