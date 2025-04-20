using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Application.Common.Interfaces.Repositories;
using TravelCompany.Domain.DTOs;
using TravelCompany.Domain.Entities;
using TravelCompany.Domain.Settings;

namespace TravelCompany.Infrastructure.Persistence.Repositories
{
    internal class RezervationRepository :BaseRepository<Reservation> , IRezervationRepository
	{

		private readonly ApplicationDbContext _context;

		private readonly string _connectionString;

		public RezervationRepository(ApplicationDbContext context, string connectionString):base(context) 
		{
			_context = context;
			_connectionString = connectionString;
		}

		public async Task<(bool IsBooked , int? ReserbationId)> BookSeatAsync(BookingSeatDTO dto)
		{
			var isBooked = false;
			var reservationId = 0;

			using (var connection = new SqlConnection(_connectionString))
			{

				using (var command = new SqlCommand("sp_BookSeat", connection))
				{

					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@ScheduledTravelId", dto.ScheduledTravelId);
					command.Parameters.AddWithValue("@StationAId", dto.StationAId);
					command.Parameters.AddWithValue("@StationBId", dto.StationBId);
					command.Parameters.AddWithValue("@SeatNumber", dto.SeatNumber);
					command.Parameters.AddWithValue("@PersonId", dto.PersonId);
					command.Parameters.AddWithValue("@PersonName", dto.PersonName);
					command.Parameters.AddWithValue("@PersonPhone", dto.PersonPhone);
					command.Parameters.AddWithValue("@PersonEmail", dto.PersonEmail);
					command.Parameters.AddWithValue("@PersonGender", dto.PersonGendor);
					command.Parameters.AddWithValue("@StationAOrder", dto.StationAOrder);
					command.Parameters.AddWithValue("@StationBOrder", dto.StationBOrder);
					command.Parameters.AddWithValue("@StationsNumber", dto.RouteStationsNumber);



					command.Parameters.Add(new SqlParameter("@IsBooked", SqlDbType.Bit) { Direction = ParameterDirection.Output });
					command.Parameters.Add(new SqlParameter("@ReservationId", SqlDbType.Int) { Direction = ParameterDirection.Output });

					try
					{
						await connection.OpenAsync();

						await command.ExecuteNonQueryAsync();

						isBooked = (bool)command.Parameters["@IsBooked"].Value;
                        reservationId= (int)command.Parameters["@ReservationId"].Value;

                    }
					catch
					{
						return (false, null);
					}


				}


			}


			return (isBooked,reservationId);

		}
	}
}
